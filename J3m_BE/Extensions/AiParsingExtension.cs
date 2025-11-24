using J3M.Shared.MealPlanModels;
using System.Text.Json;

namespace J3m_BE.Extensions
{
    public class AiParsingExtension
    {
        public static AiOutputs ExtractSummariesAndShoppingList(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            // Parse the outer AI response (from the provider)
            using var doc = JsonDocument.Parse(json);

            // Prepare empty result object
            var result = new AiOutputs
            {
                DaySummaries = new List<string>(),
                ShoppingList = new List<string>()
            };

            // ---- 1) Extract the raw "content" from the AI provider output ----
            var choices = doc.RootElement.GetProperty("choices");
            if (choices.GetArrayLength() == 0)
                return result;

            var first = choices[0];
            string content = null;

            // Support both: choices[0].message.content and choices[0].content
            if (first.TryGetProperty("message", out var msg) &&
                msg.TryGetProperty("content", out var msgContent))
            {
                content = msgContent.GetString();
            }
            else if (first.TryGetProperty("content", out var c2))
            {
                content = c2.GetString();
            }

            if (string.IsNullOrWhiteSpace(content))
                return result;

            content = content.Trim();


            // ---- 2) Remove Markdown code fences (```json ... ```) if present ----
            if (content.StartsWith("```"))
            {
                // Find the next newline after ``` or ```json
                var firstNewline = content.IndexOf('\n', 3);
                var startIdx = firstNewline >= 0 ? firstNewline + 1 : 3;

                // Find the final ```
                var lastFence = content.LastIndexOf("```", StringComparison.Ordinal);

                if (lastFence > startIdx)
                    content = content.Substring(startIdx, lastFence - startIdx).Trim();
            }


            // ---- 3) If content still has text around the JSON, extract the first { ... } block ----
            if (content.Length > 0 && content[0] != '{' && content[0] != '[')
            {
                var firstBrace = content.IndexOf('{');
                var lastBrace = content.LastIndexOf('}');

                if (firstBrace >= 0 && lastBrace > firstBrace)
                    content = content.Substring(firstBrace, lastBrace - firstBrace + 1).Trim();
            }


            // ---- 4) Parse the cleaned content as JSON ----
            try
            {
                using var structured = JsonDocument.Parse(content);
                var root = structured.RootElement;

                // Extract daySummaries[]
                if (root.TryGetProperty("daySummaries", out var ds))
                {
                    foreach (var s in ds.EnumerateArray())
                    {
                        var text = s.GetString();
                        if (!string.IsNullOrWhiteSpace(text))
                            result.DaySummaries.Add(text);
                    }
                }

                // Extract shoppingList[]
                if (root.TryGetProperty("shoppingList", out var sl))
                {
                    foreach (var item in sl.EnumerateArray())
                    {
                        var text = item.GetString();
                        if (!string.IsNullOrWhiteSpace(text))
                            result.ShoppingList.Add(text);
                    }
                }
            }
            catch (JsonException)
            {
                // If JSON parsing fails, include raw content for easier debugging
                throw new InvalidOperationException(
                    "Failed to parse model output as JSON. Raw content:\n" + content
                );
            }

            return result;
        }

    }
}
