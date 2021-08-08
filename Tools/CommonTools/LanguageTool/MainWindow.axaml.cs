using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LanguageTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.PreferSystemChrome;
            this.ExtendClientAreaToDecorationsHint = true;
            //this.SystemDecorations = SystemDecorations.BorderOnly;
        }
    }
}
