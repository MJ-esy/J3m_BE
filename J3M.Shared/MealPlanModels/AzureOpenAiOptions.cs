namespace J3M.Shared.MealPlanModels
{
    // Configuration options for Azure OpenAI
    public class AzureOpenAiOptions
    {
        public string Endpoint { get; set; }
        public string Deployment { get; set; }
        public string ApiKey { get; set; }
        public string ApiVersion { get; set; }
    }
}
