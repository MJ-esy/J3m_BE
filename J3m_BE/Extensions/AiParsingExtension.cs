using J3M.Shared.MealPlanModels;

namespace J3m_BE.Extensions
{
    public class AiParsingExtension
    {
        public static AiOutputs ExtractSummariesAndShoppingList(string json)
        {
            if (json is null) throw new ArgumentNullException(nameof(json));

            // Example minimal extraction. Tailor to your provider’s response.
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            // Assuming OpenAI-like response structure
            var content = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            var result = new AiOutputs
            {
                DaySummaries = new List<string>(),
                ShoppingList = new List<string>()
            };

            if (string.IsNullOrWhiteSpace(content))
                return result;

            // Now parse the content as JSON to extract day summaries and shopping list
            using var structured = System.Text.Json.JsonDocument.Parse(content);
            // Assuming the content has "daySummaries" and "shoppingList" arrays

            if (structured.RootElement.TryGetProperty("daySummaries", out var ds))
            {
                foreach (var s in ds.EnumerateArray())
                {
                    var text = s.GetString();
                    if (!string.IsNullOrEmpty(text))
                        result.DaySummaries.Add(text);
                }
            }

            if (structured.RootElement.TryGetProperty("shoppingList", out var sl))
            {
                foreach (var i in sl.EnumerateArray())
                {
                    var item = i.GetString();
                    if (!string.IsNullOrEmpty(item))
                        result.ShoppingList.Add(item);
                }
            }

            return result;
        }
    }
}
