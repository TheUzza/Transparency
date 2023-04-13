using Xamarin.Forms;

namespace Transparency
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void DarkModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // Enable dark mode
                App.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                // Disable dark mode (use light mode)
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
        }
    }
}
