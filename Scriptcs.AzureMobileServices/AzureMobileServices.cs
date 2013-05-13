using ScriptCs.Contracts;

namespace Scriptcs.AzureMobileServices
{
    public class AzureMobileServices : IScriptPackContext
    {
        public ZumoClient<T> GetTable<T>(string url)
        {
            return new ZumoClient<T>(url);
        }

        public ZumoClient<T> GetTable<T>(string url, string key)
        {
            return new ZumoClient<T>(url, key);
        }

        public ZumoClient<T> GetTable<T>(string url, string key, string tableName)
        {
            return new ZumoClient<T>(url, key, tableName);
        }
    }
}