using MonitoreoGMetrixMAUI10.Views;

namespace MonitoreoGMetrixMAUI10
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetallesPage), typeof(DetallesPage));
            Routing.RegisterRoute(nameof(IntentosPage), typeof(IntentosPage));
            Routing.RegisterRoute(nameof(NotificacionPage), typeof(NotificacionPage));
            Routing.RegisterRoute(nameof(PrediccionesPage), typeof(PrediccionesPage));
        }
    }
}
