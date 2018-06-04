using Arachnee.InnerCore.EntryProviderBases;
using Arachnee.InnerCore.LoggerBases;
using System;
using System.IO;

namespace Arachnee.TmdbProviders
{
    public abstract class TmdbDatabase : EntryProvider
    {
        protected string ResourcesFolder { get; }

        protected TmdbConverter TmdbConverter { get; }
        
        protected TmdbDatabase(string resourcesFolder, ILogger logger) : base(logger)
        {
            if (!Directory.Exists(resourcesFolder))
            {
                throw new ArgumentException($"Resources folder doesn't exist at \"{resourcesFolder}\".");
            }

            ResourcesFolder = resourcesFolder;
            Logger = logger;
            TmdbConverter = new TmdbConverter(logger?.CreateSubLoggerFor(nameof(TmdbConverter)));
        }
    }
}