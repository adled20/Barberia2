using Barberia2.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Barberia2.ViewModels
{
    public class RegistroUsuarioViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient = new();
        private const string ApiUrl = "https://barberiaapi.onrender.com/api/usuarios_app";
        private const string ClientesApiUrl = "https://barberiaapi.onrender.com/api/clientes";

        private bool _isVerifying;
        public bool IsVerifying
        {
            get => _isVerifying;
            set
            {
                _isVerifying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotVerifying));
            }
        }

        public bool IsNotVerifying => !IsVerifying;

        private string _clienteVerificadoMessage;
        public string ClienteVerificadoMessage
        {
            get => _clienteVerificadoMessage;
            set
            {
                _clienteVerificadoMessage = value;
                OnPropertyChanged();
            }
        }

        private Color _clienteVerificadoColor;
        public Color ClienteVerificadoColor
        {
            get => _clienteVerificadoColor;
            set
            {
                _clienteVerificadoColor = value;
                OnPropertyChanged();
            }
        }

        private bool _clienteVerificadoVisible;
        public bool ClienteVerificadoVisible
        {
            get => _clienteVerificadoVisible;
            set
            {
                _clienteVerificadoVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _formularioValido;
        public bool FormularioValido
        {
            get => _formularioValido;
            set
            {
                _formularioValido = value;
                OnPropertyChanged();
            }
        }

        // Propiedades del usuario
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaNacimiento { get; set; } = DateTime.Now.AddYears(-18);
        public string Direccion { get; set; }
        public int ClienteId { get; set; }
        public string Estado { get; set; } = "Activo";

        public ICommand VerificarClienteCommand { get; }
        public ICommand RegistrarCommand { get; }

        public RegistroUsuarioViewModel()
        {
            VerificarClienteCommand = new Command(async () => await VerificarCliente());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
        }

        private async Task VerificarCliente()
        {
            try
            {
                IsVerifying = true;
                ClienteVerificadoVisible = false;

                var response = await _httpClient.GetAsync($"{ClientesApiUrl}/{ClienteId}");

                if (response.IsSuccessStatusCode)
                {
                    ClienteVerificadoMessage = "ID de cliente válido";
                    ClienteVerificadoColor = Colors.Green;
                }
                else
                {
                    ClienteVerificadoMessage = "ID de cliente no existe";
                    ClienteVerificadoColor = Colors.Red;
                }
            }
            catch (Exception ex)
            {
                ClienteVerificadoMessage = $"Error al verificar: {ex.Message}";
                ClienteVerificadoColor = Colors.Red;
            }
            finally
            {
                ClienteVerificadoVisible = true;
                IsVerifying = false;
                ValidarFormulario();
            }
        }

        private async Task RegistrarUsuario()
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var usuario = new Usuario
                {
                    Email = Email,
                    Contrasenia = Contrasenia,
                    FechaNacimiento = FechaNacimiento,
                    Direccion = Direccion,
                    ClienteId = ClienteId,
                    Estado = Estado,
                    FechaRegistro = DateTime.Now
                };

                // Verificar si el cliente ya tiene usuario
                var usuariosResponse = await _httpClient.GetAsync($"{ApiUrl}?clienteId={ClienteId}");
                if (usuariosResponse.IsSuccessStatusCode)
                {
                    var usuarios = await usuariosResponse.Content.ReadFromJsonAsync<List<Usuario>>();
                    if (usuarios?.Any() == true)
                    {
                        await Shell.Current.DisplayAlert("Error", "Este cliente ya tiene un usuario registrado", "OK");
                        return;
                    }
                }

                // Registrar nuevo usuario
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, usuario);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await Shell.Current.DisplayAlert("Error", $"No se pudo registrar: {error}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Excepción al registrar: {ex.Message}", "OK");
            }
        }

        private bool ValidarFormulario()
        {
            var isValid = !string.IsNullOrWhiteSpace(Email) &&
                         !string.IsNullOrWhiteSpace(Contrasenia) &&
                         Contrasenia.Length >= 8 &&
                         !string.IsNullOrWhiteSpace(Direccion) &&
                         ClienteId > 0 &&
                         ClienteVerificadoColor == Colors.Green;

            FormularioValido = isValid;
            return isValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}