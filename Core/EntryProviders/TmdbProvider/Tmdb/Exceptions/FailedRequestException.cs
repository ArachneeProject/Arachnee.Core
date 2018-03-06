using System;

namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.Exceptions
{
    public class FailedRequestException : Exception
    {
        public FailedRequestException(string message) : base(message)
        {
        }
    }
}