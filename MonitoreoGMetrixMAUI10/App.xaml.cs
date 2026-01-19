using MonitoreoGMetrixMAUI10.Views;

namespace MonitoreoGMetrixMAUI10
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Start the app with the login page
            MainPage = new LoginPage();
        }
    }
}
