using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.Ignite.Core.Cache.Query;
using Apache.Ignite.Core.Client;
using Apache.Ignite.Core.Client.Cache;

namespace IgniteCacheViewer.ViewModel
{
    public class CacheViewModel : ViewModelBase
    {
        private readonly ICacheClient<object, object> _cache;

        private readonly string _cacheName;
        private ICollection<object> _cacheEntries;

        public CacheViewModel(IIgniteClient ignite, string cacheName)
        {
            // TODO: Binary mode.
            _cache = ignite.GetCache<object, object>(_cacheName);
            _cacheName = cacheName;

            Task.Run(() =>
            {
                CacheEntries = _cache.Query(new ScanQuery<object, object>()).Take(10).ToArray();
            });
        }

        public ICollection<object> CacheEntries
        {
            get => _cacheEntries;
            private set
            {
                _cacheEntries = value; 
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return _cacheName;
        }
    }
}
