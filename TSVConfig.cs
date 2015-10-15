using System.Collections.Generic;
using System.IO;
using System.Linq;
using TSVConfigReader.Core;

namespace TSVConfigReader
{
    public static class TSVConfig
    {
        private const string CACHE_KEY = "textConfigs";

        private static AppEnvironment appEnv;

        private static string fileName = @"c:\temp\TextFile1.txt";

        public static string GetConfig(string key)
        {
            string output = string.Empty;

            Dictionary<string, string> textConfigs;

            textConfigs = File
                .ReadAllLines(fileName)
                .Select(x => x.Split('\t'))
                .Where(x => x.Length > 1)
                .ToDictionary(x => x[0].Trim(), x => x[(int)appEnv]);


            output = textConfigs[key];
            return output;
        }
    }
}
