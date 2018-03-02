using System.Collections.Concurrent;

namespace Arachnee.InnerCore.ProviderBases
{
    public class Cache<TKey,TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _cache = new ConcurrentDictionary<TKey, TValue>();

        public void AddOrUpdate(TKey key, TValue value)
        {
            _cache.AddOrUpdate(key, k => value, (k, oldValue) => value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _cache.TryGetValue(key, out value);
        }
    }
}