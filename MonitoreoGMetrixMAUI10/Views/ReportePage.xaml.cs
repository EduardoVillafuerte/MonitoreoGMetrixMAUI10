using MonitoreoGMetrixMAUI10.ViewModels;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class ReportePage : ContentPage
{
    private readonly ReportesViewModel _viewModel;

    public ReportePage()
    {
        InitializeComponent();
        _viewModel = new ReportesViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // If you later add a load method on the VM, call it here:
        // _viewModel.Load();
    }
}