using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using TSVConfigReader.Core;

namespace TSVConfigReader
{
    public static class TSVConfig
    {
        private const string CACHE_KEY = "textConfigs";

        private static AppEnvironment appEnv = (AppEnvironment)System.Enum.Parse(typeof(AppEnvironment), ConfigurationManager.AppSettings["AppEnvironment"]);

        private static string fileName = @"c:\temp\TextFile1.txt";

        public static string GetConfig(string key)
        {
            string output = string.Empty;

            ObjectCache cache = MemoryCache.Default;
            Dictionary<string, string> textConfigs = cache[CACHE_KEY] as Dictionary<string, string>;

            if (textConfigs == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();

                List<string> filePaths = new List<string>();
                filePaths.Add(fileName);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                textConfigs = File
                .ReadAllLines(fileName)
                .Select(x => x.Split('\t'))
                .Where(x => x.Length > 1)
                .ToDictionary(x => x[0].Trim(), x => x[(int)appEnv]);

                cache.Set(CACHE_KEY, textConfigs, policy);
            }

            output = textConfigs[key];

            return output;

        }
    }
}
