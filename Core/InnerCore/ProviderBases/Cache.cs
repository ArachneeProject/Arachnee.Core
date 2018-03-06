using System.Collections.Concurrent;

namespace Arachnee.InnerCore.ProviderBases
{
    public class Cache<TKey,TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _cache = new ConcurrentDictionary<TKey, TValue>();

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return _cache.GetOrAdd(key, k => value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _cache.TryGetValue(key, out value);
        }
    }
}