
using System.Collections.ObjectModel;
using testagenda;

namespace Barberia2;

public partial class agenda : ContentPage
{
    ObservableCollection<disponibilidad_agenda> items_mostrar { get; set; }
    List<mostar_barbero?> mostar_barberos { get; set; }
    public TimeOnly? inicio { get; set; }
    public TimeOnly? final { get; set; }

    public agenda()
	{
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
}
