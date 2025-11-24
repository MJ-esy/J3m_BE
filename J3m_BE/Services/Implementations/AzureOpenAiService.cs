using J3M.Shared.MealPlanModels;
using J3m_BE.Extensions;
using J3m_BE.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace J3m_BE.Services.Implementations
{
    public class AzureOpenAiService : IAzureOpenAiService
    {
        private readonly HttpClient _client;
        private readonly AzureOpenAiOptions _options;
        public AzureOpenAiService(HttpClient client, IOptions<AzureOpenAiOptions> options)
        {
            _client = client;
            _options = options.Value;
            if (!string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                _client.DefaultRequestHeaders.Add("api-key", _options.ApiKey);
            }
        }

        // Enrich the meal plan with AI-generated summaries and shopping list
        public async Task<WeeklyMealPlanDto> EnrichAsync(List<DayMealPlanDto> plan, List<int> allergyIds, List<int> dietIds)
        {
            var prompt = BuildPrompt(plan, allergyIds, dietIds);

            // Prepare the request payload for Azure OpenAI 
            var payload = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful meal planner and nutrition assistant. Keep responses concise and structured." },
                    new { role = "user", content = prompt }
                },
                // Model parameters
                temperature = 0.7,
                max_tokens = 1000,
            };

            // Build a robust request Uri: prefer _options.Endpoint if valid absolute; otherwise rely on _client.BaseAddress.
            HttpResponseMessage response;
            try
            {
                var relativePath = $"openai/deployments/{_options.Deployment}/chat/completions?api-version={_options.ApiVersion}";

                // Validate and normalize endpoint
                var endpoint = _options.Endpoint?.Trim();
                Uri requestUri;

                if (!string.IsNullOrWhiteSpace(endpoint) && Uri.TryCreate(endpoint, UriKind.Absolute, out var baseUri))
                {
                    // Combine baseUri and relative path, ensuring no duplicate slashes
                    requestUri = new Uri(baseUri, relativePath);
                }
                else if (_client.BaseAddress != null)
                {
                    requestUri = new Uri(_client.BaseAddress, relativePath);
                }
                else
                {
                    // Neither endpoint nor BaseAddress available — provide a clear diagnostic message
                    throw new InvalidOperationException("Azure OpenAI endpoint is not configured (AzureOpenAI:Endpoint) and HttpClient.BaseAddress is not set. Configure one of them.");
                }

                response = await _client.PostAsync(
                    requestUri,
                    new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json")
                );
            }
            catch (HttpRequestException ex)
            {
                // Network-level failure (DNS, connection refused, TLS, etc.)
                throw new InvalidOperationException("Failed to send request to Azure OpenAI service.", ex);
            }
            catch (TaskCanceledException ex)
            {
                // Timeout or cancelled
                throw new InvalidOperationException("Request to Azure OpenAI service timed out or was cancelled.", ex);
            }

            // Read body for both success and error cases (useful for diagnostics)
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Include status and body to help debugging; decide whether to throw or return a fallback
                throw new HttpRequestException(
                    $"Azure OpenAI returned {(int)response.StatusCode} ({response.ReasonPhrase}). Response body: {json}"
                );
            }

            // Parse model output (guard parsing errors to provide context)
            AiOutputs parsed;
            try
            {
                parsed = AiParsingExtension.ExtractSummariesAndShoppingList(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to parse Azure OpenAI response JSON.", ex);
            }

            // Map AiOutputs to WeeklyMealPlanDto
            var weeklyMealPlan = new WeeklyMealPlanDto
            {
                // Map summaries to corresponding days
                DaySummaries = parsed.DaySummaries
                    .Select((summary, idx) => new DayMealPlanDto
                    {
                        Day = plan.ElementAtOrDefault(idx)?.Day ?? $"Day {idx + 1}",
                        Meals = plan.ElementAtOrDefault(idx)?.Meals ?? new List<MealSlotDto>(),
                        Summary = summary ?? string.Empty
                    })
                    .ToList(),
                ShoppingList = parsed.ShoppingList
            };

            return weeklyMealPlan;
        }

        // Build the prompt for the AI model
        private static string BuildPrompt(List<DayMealPlanDto> plan, List<int> allergyIds, List<int> dietIds)
        {
            // Serialize the plan and preferences to JSON for clarity
            var inputJson = System.Text.Json.JsonSerializer.Serialize(new
            {
                plan,
                allergyIds = allergyIds ?? new List<int>(),
                dietIds = dietIds ?? new List<int>()
            });

            // Construct the prompt
            return $@"
                    Given this weekly meal plan JSON, do two things:
                    1) Write a one-sentence summary for each day focusing on balance, nutrition, and variety.
                    2) Produce a consolidated shopping list (group common items; avoid duplicates).

                    Respond as JSON with:
                    {{ ""daySummaries"": [""..."", ""...""], ""shoppingList"": [""item1"", ""item2""] }}

                    Plan:
                    {inputJson}";
        }
    }
}
