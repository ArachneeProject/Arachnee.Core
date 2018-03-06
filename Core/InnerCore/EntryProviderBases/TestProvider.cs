using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.EntryProviderBases
{
    /// <summary>
    /// Can only provide 3 different entries. Should only be used for tests.
    /// </summary>
    public class TestProvider : EntryProvider
    {
        public const string Terminator2JudgmentDayId = "Movie-280";
        public const string ArnoldSchwarzeneggerId = "Artist-1100";
        public const string JamesCameronId = "Artist-2710";
        
        public override Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null)
        {
            return Task.Run(() =>
            {
                IList<SearchResult> results = new List<SearchResult>();
                
                if (searchQuery.ToLowerInvariant().Contains("terminator"))
                {
                    results.Add(new SearchResult
                    {
                        Date = "1984",
                        EntryId = Terminator2JudgmentDayId,
                        ImagePath = string.Empty,
                        SearchResultType = SearchResultType.Movie
                    });
                }

                if (searchQuery.ToLowerInvariant().Contains("arnold"))
                {
                    results.Add(new SearchResult
                    {
                        Date = "1947",
                        EntryId = ArnoldSchwarzeneggerId,
                        ImagePath = string.Empty,
                        SearchResultType = SearchResultType.Person
                    });
                }

                if (searchQuery.ToLowerInvariant().Contains("james"))
                {
                    results.Add(new SearchResult
                    {
                        Date = "1954",
                        EntryId = JamesCameronId,
                        ImagePath = string.Empty,
                        SearchResultType = SearchResultType.Person
                    });
                }
                
                return results;

            }, cancellationToken);
        }

        protected override Task<Entry> LoadEntryAsync(string entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Entry entry = DefaultEntry.Instance;

                switch (entryId)
                {
                    case Terminator2JudgmentDayId:
                        entry = new Movie("Movie-280")
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
                                    Type = ConnectionType.Actor,
                                    Label = "T-800"
                                }
                            }
                        };
                        break;
                        
                    case ArnoldSchwarzeneggerId:
                        entry = new Artist(ArnoldSchwarzeneggerId)
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
                        };
                        break;

                    case JamesCameronId:
                        entry = new Artist(JamesCameronId)
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
                        };
                        break;
                }

                return entry;

            }, cancellationToken);
        }
    }
}