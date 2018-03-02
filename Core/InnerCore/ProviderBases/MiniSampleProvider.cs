using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.ProviderBases
{
    public class MiniSampleProvider : CacheEntryProvider
    {
        public readonly List<Entry> Entries;

        public MiniSampleProvider()
        {
            Entries = new List<Entry>
            {
                new Movie("Movie-280")
                {
                    Title = "Terminator 2: Judgment Day",
                    Connections = new List<Connection>
                    {
                        // cameron
                        new Connection
                        {
                            ConnectedId = "Artist-2710",
                            Type = ConnectionType.Actor,
                            Label = "Some guy at the bar"
                        },
                        new Connection
                        {
                            ConnectedId = "Artist-2710",
                            Type = ConnectionType.Director
                        },

                        // schwarzenegger
                        new Connection
                        {
                            ConnectedId = "Artist-1100",
                            Type = ConnectionType.Actor
                        }
                    }
                },
                new Artist("Artist-1100")
                {
                    Name = "Arnold Schwarzenegger",
                    Connections = new List<Connection>
                    {
                        new Connection
                        {
                            ConnectedId = "Movie-280",
                            Type = ConnectionType.Actor,
                            Label = "T-800"
                        }
                    }
                },
                new Artist("Artist-2710")
                {
                    Name = "James Cameron",
                    Connections = new List<Connection>
                    {
                        new Connection
                        {
                            ConnectedId = "Movie-280",
                            Type = ConnectionType.Actor,
                            Label = "Some guy at the bar"
                        },
                        new Connection
                        {
                            ConnectedId = "Movie-280",
                            Type = ConnectionType.Director
                        },
                    }
                }
            };
        }
        
        public override Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, IProgress<double> progress, CancellationToken cancellationToken)
        {
            return new Task<IList<SearchResult>>(() =>
            {
                var result = new List<SearchResult>();

                foreach (var movie in Entries.OfType<Movie>())
                {
                    if (movie.Title.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(new SearchResult
                        {
                            Date = movie.ReleaseDate.ToShortTimeString(),
                            EntryId = movie.Id,
                            ImagePath = movie.MainImagePath,
                            SearchResultType = SearchResultType.Movie
                        });
                    }
                }

                foreach (var artist in Entries.OfType<Artist>())
                {
                    if (artist.Name.Equals(searchQuery, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(new SearchResult
                        {
                            Date = artist.Birthday.ToShortTimeString(),
                            EntryId = artist.Id,
                            ImagePath = artist.MainImagePath,
                            SearchResultType = SearchResultType.Person
                        });
                    }
                }

                return result;
            });
        }

        protected override Task<Entry> LoadEntryAsync(string entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            return new Task<Entry>(() =>
            {
                return Entries.FirstOrDefault(e => e.Id == entryId);
            });
        }
    }
}