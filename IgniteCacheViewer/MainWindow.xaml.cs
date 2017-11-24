using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using IgniteCacheViewer.ViewModel;

namespace IgniteCacheViewer
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();

            DataContext = new IgniteViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
