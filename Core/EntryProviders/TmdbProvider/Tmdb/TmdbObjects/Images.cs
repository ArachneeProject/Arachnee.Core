using System.Collections.Generic;

namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.TmdbObjects
{
    public class Images
    {
        public List<ImageDetails> Backdrops { get; set; }
        public List<ImageDetails> Posters { get; set; }
        public List<ImageDetails> Profiles { get; set; }
    }
}