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
        public static Id Terminator2JudgmentDayId = Id.FromMovieNumber(280);
        public static Id ArnoldSchwarzeneggerId = Id.FromArtistNumber(1100);
        public static Id JamesCameronId = Id.FromArtistNumber(2710);
        
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
                        ImagePath = string.Empty
                    });
                }

                if (searchQuery.ToLowerInvariant().Contains("arnold"))
                {
                    results.Add(new SearchResult
                    {
                        Date = "1947",
                        EntryId = ArnoldSchwarzeneggerId,
                        ImagePath = string.Empty
                    });
                }

                if (searchQuery.ToLowerInvariant().Contains("james"))
                {
                    results.Add(new SearchResult
                    {
                        Date = "1954",
                        EntryId = JamesCameronId,
                        ImagePath = string.Empty
                    });
                }
                
                return results;

            }, cancellationToken);
        }

        protected override Task<Entry> LoadEntryAsync(Id entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Entry entry = DefaultEntry.Instance;

                if (Id.IsNullOrDefault(entryId))
                {
                    return entry;
                }

                if (entryId == Terminator2JudgmentDayId)
                    entry = new Movie(Terminator2JudgmentDayId)
                    {
                        Title = "Terminator 2: Judgment Day",
                        Connections = new List<Connection>
                        {
                            // cameron
                            new Connection
                            {
                                ConnectedId = JamesCameronId,
                                Type = ConnectionType.Actor,
                                Label = "Some guy at the bar"
                            },
                            new Connection
                            {
                                ConnectedId = JamesCameronId,
                                Type = ConnectionType.Director
                            },

                            // schwarzenegger
                            new Connection
                            {
                                ConnectedId = ArnoldSchwarzeneggerId,
                                Type = ConnectionType.Actor,
                                Label = "T-800"
                            }
                        }
                    };
                else if (entryId == ArnoldSchwarzeneggerId)
                {
                    entry = new Artist(ArnoldSchwarzeneggerId)
                    {
                        Name = "Arnold Schwarzenegger",
                        Connections = new List<Connection>
                        {
                            new Connection
                            {
                                ConnectedId = Terminator2JudgmentDayId,
                                Type = ConnectionType.Actor,
                                Label = "T-800"
                            }
                        }
                    };
                }
                else if (entryId == JamesCameronId)
                {
                    entry = new Artist(JamesCameronId)
                    {
                        Name = "James Cameron",
                        Connections = new List<Connection>
                        {
                            new Connection
                            {
                                ConnectedId = Terminator2JudgmentDayId,
                                Type = ConnectionType.Actor,
                                Label = "Some guy at the bar"
                            },
                            new Connection
                            {
                                ConnectedId = Terminator2JudgmentDayId,
                                Type = ConnectionType.Director
                            },
                        }
                    };
                }

                return entry;

            }, cancellationToken);
        }
    }
}