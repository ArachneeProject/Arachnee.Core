using System.Collections.Generic;

namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.TmdbObjects
{
    public class Configuration
    {
        public List<string> ChangeKeys { get; set; }
        public ImageConfiguration Images { get; set; }
    }
}