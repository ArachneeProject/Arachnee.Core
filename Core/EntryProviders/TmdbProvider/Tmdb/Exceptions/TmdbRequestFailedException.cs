namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.Exceptions
{
    public class TmdbRequestFailedException : FailedRequestException
    {
        public TmdbRequestFailedException(string message) : base(message)
        {
        }
    }
}