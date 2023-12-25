using Newtonsoft.Json;

namespace Restro.Helper
{
    public static class SessionHelper
    {
        public static async Task SetObjectAsJsonAsync(this ISession session, string key, object value)
        {
            await Task.Run(() => session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })));
        }
        public static async Task<T> GetObjectFromJsonAsync<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value is not null)
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(value));

            return default(T);
        }
    }
}
