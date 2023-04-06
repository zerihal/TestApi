using System.Text.Json;

namespace TestApi.Helpers
{
    public static class ExtensionMethods
    {
        public static T? ToObject<T>(this JsonElement element)
        {
            try
            {
                var json = element.GetRawText();
                return JsonSerializer.Deserialize<T>(json);
            }
            catch 
            {
                return default(T);
            }
        }
    }
}
