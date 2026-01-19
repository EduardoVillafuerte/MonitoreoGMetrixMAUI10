using System;
using MonitoreoGMetrixMAUI10.Controllers;

namespace MonitoreoGMetrixMAUI10.Views;

public partial class ActualizarDatosPage : ContentPage
{
    private readonly ActualizarDatosService _actualizarDatosService;

    public ActualizarDatosPage(ActualizarDatosService actualizarDatosService)
    {
        InitializeComponent();
        _actualizarDatosService = actualizarDatosService;
    }

    private async void BtnSeleccionarArchivo_Clicked(object sender, EventArgs e)
    {
        // Logic to select a CSV file
        var file = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Seleccionar archivo CSV",
            FileTypes = FilePickerFileType.Pdf
        });

        if (file != null)
        {
            lblArchivo.Text = file.FileName;
        }
    }

    private async void BtnSubir_Clicked(object sender, EventArgs e)
    {
        if (lblArchivo.Text == "Archivo no seleccionado")
        {
            await DisplayAlert("Error", "Por favor seleccione un archivo primero.", "OK");
            return;
        }

        // Logic to upload the file using ActualizarDatosService
        bool result = await _actualizarDatosService.SubirArchivoCSVAsync(lblArchivo.Text);

        if (result)
        {
            await DisplayAlert("Éxito", "Archivo subido correctamente.", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al subir el archivo.", "OK");
        }
    }
}