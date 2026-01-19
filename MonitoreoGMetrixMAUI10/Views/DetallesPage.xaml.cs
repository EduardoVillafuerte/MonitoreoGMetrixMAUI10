using MonitoreoGMetrixMAUI10.ViewModels;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class DetallesPage : ContentPage
{
    public DetallesPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Recuperamos el ViewModel y ejecutamos su método de carga
        if (BindingContext is DetallesViewModel vm)
        {
            vm.OnAppearing();
        }
    }
}