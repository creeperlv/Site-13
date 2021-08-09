using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CommonTools;
using System.Diagnostics;

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
        void Init()
        {
            AboutButton.Click += (_, _) => {
                Dialog dialog=new Dialog();
                dialog.DialogTitle = "About";
                dialog.DialogContent = new TextBlock {Text="Language Tool\nSite-13 Toolset" };
                dialog.ShowDialog(this);
            };
            Load();
        }
        void Load()
        {
            CentralEditor.Children.Add(new Field());
            CentralEditor.Children.Add(new Field());
            CentralEditor.Children.Add(new Field());
        }
        StackPanel CentralEditor;
        Button AboutButton;
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.PreferSystemChrome;
            this.ExtendClientAreaToDecorationsHint = true;
            this.TransparencyLevelHint = WindowTransparencyLevel.Blur;
            Trace.WriteLine(this.ActualTransparencyLevel);
            this.Background = new SolidColorBrush(Colors.Transparent);
            CentralEditor=this.FindControl<StackPanel>("CentralEditor");
            AboutButton = this.FindControl<Button>("AboutButton");
            Init();
            //this.SystemDecorations = SystemDecorations.BorderOnly;
        }
    }
}
