namespace MiniMarket.API.Services
{
    public class SentryService
    {
        public void SetExtra(string key, string value)
        {
            SentrySdk.ConfigureScope(scope =>
            {
                scope.SetExtra(key, value);
            });
        }

        public void AddBreadcrumb(string message, string? category = null, string? type=null)
        {
            SentrySdk.AddBreadcrumb(message, category, type);
        }
    }
}
