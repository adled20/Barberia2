
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Barberia2.Models;
using Newtonsoft.Json;
using testagenda;

namespace Barberia2;

public partial class agenda : ContentPage
{
    private int _duracionCorte;
    private int _comboId;
    private int _corteId;
    private int _planId;
    private int _usuarioId;


    ObservableCollection<disponibilidad_agenda> items_mostrar { get; set; }
    List<mostar_barbero?> mostar_barberos { get; set; }
    public TimeOnly? inicio { get; set; }
    public TimeOnly? final { get; set; }

    public agenda(int duracionCorte, int comboId = 0, int corteId = 0, int planId = 0, int usuarioId = 0)
    {

        _duracionCorte = duracionCorte;
        _comboId = comboId;
        _corteId = corteId;
        _planId = planId;
        _usuarioId = usuarioId;

        InitializeComponent();
        this.BindingContext = this;
        pickercito.ItemsSource = mostar_barberos;
    }
    private void pickercito_SelectedIndexChanged(object sender, EventArgs e)
    {
        mostar_barbero id_seleccionada = (mostar_barbero)pickercito.SelectedItem;
        ContactsCollection.ItemsSource = items_mostrar
            .Where(x => x.inicio_cita != null && x.idbarberos == id_seleccionada.idbarberos);
    }

    private async void OnAgregarCitaClicked(object sender, EventArgs e)
    {
        try
        {
            if (pickercito.SelectedItem == null)
            {
                await DisplayAlert("Error", "Debes seleccionar un barbero", "OK");
                return;
            }

            if (timeInicio == null || timeInicio.Time.TotalMilliseconds == 0)
            {
                await DisplayAlert("Error", "Selecciona una hora válida", "OK");
                return;
            }

            var timeSpan = timeInicio.Time;
            TimeOnly inicioCita;
            try
            {
                inicioCita = new TimeOnly(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            catch
            {
                await DisplayAlert("Error", "La hora seleccionada no es válida", "OK");
                return;
            }

            TimeOnly finCita = inicioCita.AddMinutes(_duracionCorte);

            var primerItem = items_mostrar.FirstOrDefault();
            if (primerItem == null || inicioCita < primerItem.hora_entrada || finCita > primerItem.hora_salida)
            {
                await DisplayAlert("Error", $"Horario no disponible. Trabaja de {primerItem?.hora_entrada:HH:mm} a {primerItem?.hora_salida:HH:mm}", "OK");
                return;
            }

            var barberoSeleccionado = (mostar_barbero)pickercito.SelectedItem;

            bool conflicto = items_mostrar.Any(c =>
                c.idbarberos == barberoSeleccionado.idbarberos &&
                c.inicio_cita != null &&
                ((inicioCita >= c.inicio_cita && inicioCita < c.final_cita) ||
                 (finCita > c.inicio_cita && finCita <= c.final_cita)));

            if (conflicto)
            {
                await DisplayAlert("Error", "El barbero ya tiene una cita en ese horario", "OK");
                return;
            }

            items_mostrar.Add(new disponibilidad_agenda
            {
                idbarberos = barberoSeleccionado.idbarberos,
                inicio_cita = inicioCita,
                final_cita = finCita,
                hora_entrada = primerItem.hora_entrada,
                hora_salida = primerItem.hora_salida
            });

            ContactsCollection.ItemsSource = items_mostrar
                .Where(x => x.inicio_cita != null && x.idbarberos == barberoSeleccionado.idbarberos)
                .OrderBy(x => x.inicio_cita);

            // Crear cita para API
            var nuevaCita = new Agendas
            {
                usuarios_idusuarios = _usuarioId,
                combo_idcombo = _comboId,
                corte_idcorte = _corteId,
                Plan_idPlan = _planId,
                tiempo_inicio = inicioCita,
                tiempo_final = finCita,
                dia = DateTime.Today
            };

            bool resultado = await EnviarCitaAPI(nuevaCita);

            if (resultado)
            {
                await DisplayAlert("Cita registrada", $"Tu cita fue registrada correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar la cita en la base de datos", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            Console.WriteLine($"ERROR: {ex}");
        }
    }

    private async Task<bool> EnviarCitaAPI(Agendas nuevaCita)
    {
        try
        {
            using HttpClient client = new();
            var json = JsonConvert.SerializeObject(nuevaCita);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://barberiaapi.onrender.com/api/agenda", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar la cita: {ex.Message}");
            return false;
        }
    }

}
