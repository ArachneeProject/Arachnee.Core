using System;
using System.IO;

namespace Arachnee.TmdbProvider.Online
{
    public static class Constants
    {
        public static string ApplicationFolder
        {
            get
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(Arachnee));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                return folder;
            }
        }

        public static string ApiKey
        {
            get
            {
                var apiKeyPath = Path.Combine(Constants.ApplicationFolder, "key");
                if (!File.Exists(apiKeyPath))
                {
                    throw new FileNotFoundException($"Api key was not found at \"{apiKeyPath}\"");
                }

                var key = File.ReadAllText(apiKeyPath);
                return key;
            }
        }
    }
}