using MonitoreoGMetrixMAUI10.ViewModels;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class AptosPage : ContentPage
{
    private readonly AptosViewModel _viewModel;

    public AptosPage()
    {
        InitializeComponent();
        _viewModel = new AptosViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }
}   