using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;

namespace IgniteCacheViewer.ViewModel
{
    public class IgniteViewModel : INotifyPropertyChanged
    {
        private string _status = "Connecting...";

        private volatile IIgniteClient _client;

        private ICollection<string> _cacheNames;

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

                    CacheNames = _client.GetCacheNames();
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
            set
            {
                _status = value; 
                OnPropertyChanged();
            }
        }

        public ICollection<string> CacheNames
        {
            get => _cacheNames;
            set
            {
                _cacheNames = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
