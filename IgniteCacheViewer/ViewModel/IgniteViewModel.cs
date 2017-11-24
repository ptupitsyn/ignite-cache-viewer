using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;

namespace IgniteCacheViewer.ViewModel
{
    public class IgniteViewModel : ViewModelBase
    {
        private string _status = "Connecting...";

        private volatile IIgniteClient _client;

        private ICollection<CacheViewModel> _caches;
        private CacheViewModel _selectedCache;

        public IgniteViewModel()
        {
            Task.Run(() =>
            {
                var cfg = new IgniteClientConfiguration
                {
                    Host = "127.0.0.1"
                };

                try
                {
                    Console.WriteLine("Connecting...");
                    _client = Ignition.StartClient(cfg);

                    var cacheNames = _client.GetCacheNames();

                    Status = $"Connected to Ignite cluster. Found {cacheNames.Count} caches.";

                    _caches = cacheNames.Select(x => new CacheViewModel(_client, x)).ToArray();

                    Console.WriteLine("CONNECTED.");
                }
                catch (Exception e)
                {
                    Status = "Failed to connect: " + e;
                    Console.WriteLine(e);
                    throw;
                }
            });
        }

        public string Status
        {
            get => _status;
            private set
            {
                _status = value; 
                OnPropertyChanged();
            }
        }

        public ICollection<CacheViewModel> Caches
        {
            get => _caches;
            private set
            {
                _caches = value;
                OnPropertyChanged();
            }
        }

        public CacheViewModel SelectedCache
        {
            get => _selectedCache;
            set { _selectedCache = value; OnPropertyChanged(); }
        }
    }
}
