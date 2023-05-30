using System.Text.Json;

namespace TestApi.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extension method to deserialize a JSON object to type T.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="element">JSON element</param>
        /// <returns>Object of type T from the JSON element.</returns>
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
