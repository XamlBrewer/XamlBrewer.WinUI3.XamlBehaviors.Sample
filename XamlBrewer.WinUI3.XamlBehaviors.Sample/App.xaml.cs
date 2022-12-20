using Microsoft.UI.Xaml;

namespace XamlBrewer.WinUI3.XamlBehaviors.Sample
{
    public partial class App : Application
    {
        private Shell shell;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            shell = new Shell();
            shell.Activate();
        }
    }
}
