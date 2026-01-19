using MonitoreoGMetrixMAUI10.ViewModels;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class PrediccionesPage : ContentPage
{
	public PrediccionesPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PrediccionesViewModel vm)
        {
            await vm.OnAppearing();
        }
    }
}