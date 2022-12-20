using Microsoft.UI.Xaml;

namespace XamlBrewer.WinUI3.XamlBehaviors.Sample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Window
    {
        public Shell()
        {
            Title = "XAML Brewer WinUI 3 XAML Behaviors Sample";

            InitializeComponent();

            var appWindow = this.GetAppWindow();
            appWindow.SetIcon("Assets/Beer.ico");

            (Application.Current as App).EnsureSettings();
            ApplyTheme();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = (Application.Current as App).Settings;
            settings.IsLightTheme = !settings.IsLightTheme;
            (Application.Current as App).SaveSettings();
            Root.ActualThemeChanged += Root_ActualThemeChanged;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            var settings = (Application.Current as App).Settings;
            Root.RequestedTheme = settings.IsLightTheme ? ElementTheme.Light : ElementTheme.Dark;
        }
        private void Root_ActualThemeChanged(FrameworkElement sender, object args)
        {
            // Theme change refinements (e.g. content dialogs and title bar).
        }
    }
}
