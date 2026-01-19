using MonitoreoGMetrixMAUI10.Models;
using MonitoreoGMetrixMAUI10.ViewModels;
using MonitoreoGMetrixMAUI10.Controllers;
using MonitoreoGMetrixMAUI10.Converters;

namespace MonitoreoGMetrixMAUI10
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Registra ViewModels si quieres inyectar, opcional
            builder.Services.AddSingleton<ReportesViewModel>();
            builder.Services.AddSingleton<DetallesModel>();
            builder.Services.AddSingleton<IntentosViewModel>();
            builder.Services.AddSingleton<AptosViewModel>();
            builder.Services.AddSingleton<NotificacionViewModel>();
            builder.Services.AddSingleton<ActualizarDatosService>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<ColorPromedioConverter>();

            return builder.Build();
        }
    }
}
