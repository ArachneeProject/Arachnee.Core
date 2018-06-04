using Arachnee.InnerCore.EntryProviderBases;
using Arachnee.InnerCore.LoggerBases;
using System;
using System.IO;

namespace Arachnee.TmdbProviders
{
    public abstract class TmdbDatabase : EntryProvider
    {
        protected string ResourcesFolder { get; }

        protected TmdbConverter TmdbConverter { get; } = new TmdbConverter();
        
        protected TmdbDatabase(string resourcesFolder, ILogger logger) : base(logger)
        {
            if (!Directory.Exists(resourcesFolder))
            {
                throw new ArgumentException($"Resources folder doesn't exist at \"{resourcesFolder}\".");
            }

            ResourcesFolder = resourcesFolder;
            Logger = logger;
        }
    }
}