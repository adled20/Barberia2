
using System.Collections.ObjectModel;
using testagenda;

namespace Barberia2;

public partial class agenda : ContentPage
{
    private int _duracionCorte;


    ObservableCollection<disponibilidad_agenda> items_mostrar { get; set; }
    List<mostar_barbero?> mostar_barberos { get; set; }
    public TimeOnly? inicio { get; set; }
    public TimeOnly? final { get; set; }

    public agenda(int duracionCorte)
	{
        _duracionCorte = duracionCorte;

        ObservableCollection<disponibilidad_agenda> items_api = new ObservableCollection<disponibilidad_agenda>(){new disponibilidad_agenda()
            {
                idbarberos = 1,
                inicio_cita = null,
                final_cita = null,
                hora_entrada = new TimeOnly(08, 00, 00),
                hora_salida = new TimeOnly(18, 00, 00)
            }, new disponibilidad_agenda()
            {
                idbarberos = 1,
                inicio_cita = new TimeOnly(13,00,00),
                final_cita = new TimeOnly(14, 00, 00),
                hora_entrada = new TimeOnly(08, 00, 00),
                hora_salida = new TimeOnly(18, 00, 00)
            },new disponibilidad_agenda()
            {
                idbarberos = 2,
                inicio_cita = new TimeOnly(15,30,00),
                final_cita = new TimeOnly(16, 40, 00),
                hora_entrada = new TimeOnly(08, 00, 00),
                hora_salida = new TimeOnly(18, 00, 00)
            }};

        List<mostar_barbero?> lista_barberos_api = new List<mostar_barbero?>(){new mostar_barbero()
            {
                idbarberos = 1,
                nombre = "Javier doria al cuadrado",
                fotobarbero = "fachero",
                apodo = "el mascapito"

            }, new mostar_barbero()
            {
                idbarberos = 2,
                nombre = "Emmanuel Ramos",
                fotobarbero = "fotito",
                apodo = "emma"
            },new mostar_barbero()
            {
                idbarberos = 3,
                nombre = "Goku ",
                fotobarbero = "gokusito",
                apodo = "el sayayin"
            }};

        inicio = items_api.FirstOrDefault().hora_entrada;
        final = items_api.FirstOrDefault().hora_salida;
        items_mostrar = items_api;
        mostar_barberos = new List<mostar_barbero?>();
        mostar_barberos = lista_barberos_api;


        InitializeComponent();
        this.BindingContext = this;
        pickercito.ItemsSource = mostar_barberos;
    }
    private void pickercito_SelectedIndexChanged(object sender, EventArgs e)
    {
        mostar_barbero id_selecionada = (mostar_barbero)pickercito.SelectedItem;
        ContactsCollection.ItemsSource = items_mostrar.Where(x => x.inicio_cita != null && x.idbarberos == id_selecionada.idbarberos);
    }
    private void OnAgregarCitaClicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Validar barbero seleccionado
            if (pickercito.SelectedItem == null)
            {
                DisplayAlert("Error", "Debes seleccionar un barbero", "OK");
                return;
            }

            // 2. Validar y obtener hora de inicio (manejo seguro)
            if (timeInicio == null || timeInicio.Time.TotalMilliseconds == 0)
            {
                DisplayAlert("Error", "Selecciona una hora válida", "OK");
                return;
            }

            // 3. Conversión segura a TimeOnly
            var timeSpan = timeInicio.Time;
            TimeOnly inicioCita;

            try
            {
                inicioCita = new TimeOnly(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            catch (ArgumentOutOfRangeException)
            {
                DisplayAlert("Error", "La hora seleccionada no es válida", "OK");
                return;
            }

            // 4. Calcular hora final
            TimeOnly finCita = inicioCita.AddMinutes(_duracionCorte);

            // 5. Validar horario laboral
            var primerItem = items_mostrar.FirstOrDefault();
            if (primerItem == null || inicioCita < primerItem.hora_entrada || finCita > primerItem.hora_salida)
            {
                DisplayAlert("Error", $"Horario no disponible. Trabaja de {primerItem?.hora_entrada:HH:mm} a {primerItem?.hora_salida:HH:mm}", "OK");
                return;
            }

            // 6. Validar solapamiento
            var barberoSeleccionado = (mostar_barbero)pickercito.SelectedItem;
            bool conflicto = items_mostrar.Any(c =>
                c.idbarberos == barberoSeleccionado.idbarberos &&
                c.inicio_cita != null &&
                ((inicioCita >= c.inicio_cita && inicioCita < c.final_cita) ||
                 (finCita > c.inicio_cita && finCita <= c.final_cita)));

            if (conflicto)
            {
                DisplayAlert("Error", "El barbero ya tiene una cita en ese horario", "OK");
                return;
            }

            // 7. Agregar cita
            items_mostrar.Add(new disponibilidad_agenda
            {
                idbarberos = barberoSeleccionado.idbarberos,
                inicio_cita = inicioCita,
                final_cita = finCita,
                hora_entrada = primerItem.hora_entrada,
                hora_salida = primerItem.hora_salida
            });

            // 8. Actualizar UI
            ContactsCollection.ItemsSource = items_mostrar
                .Where(x => x.inicio_cita != null && x.idbarberos == barberoSeleccionado.idbarberos)
                .OrderBy(x => x.inicio_cita);

            DisplayAlert("Éxito",
                $"Cita agendada:\n" +
                $"• Barbero: {barberoSeleccionado.nombre}\n" +
                $"• Horario: {inicioCita:HH:mm} - {finCita:HH:mm}\n" +
                $"• Duración: {_duracionCorte} minutos",
                "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
#if DEBUG
            Console.WriteLine($"ERROR: {ex}");
#endif
        }
    }
}
