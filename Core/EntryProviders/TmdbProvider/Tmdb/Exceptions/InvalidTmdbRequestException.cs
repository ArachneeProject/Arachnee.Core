using System;

namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.Exceptions
{
    public class InvalidTmdbRequestException : Exception
    {
        public InvalidTmdbRequestException(string message) : base(message)
        {
        }
    }
}