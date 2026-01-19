using MonitoreoGMetrixMAUI10.ViewModels;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class IntentosPage : ContentPage
{
    // 1. Constructor VACÍO (Obligatorio para Shell)
    public IntentosPage()
    {
        InitializeComponent();
        // Asignamos el ViewModel vacío. 
        // Shell llenará las propiedades NRC, PeriodoID y UserName automáticamente.
        BindingContext = new IntentosViewModel();
    }

    // 2. Cargar datos cuando la página aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Ejecutamos la carga de datos aquí, cuando ya tenemos los parámetros
        if (BindingContext is IntentosViewModel vm)
        {
            vm.OnAppearing();
        }
    }
}