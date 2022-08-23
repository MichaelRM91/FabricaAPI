using FabricaFrontend.Models;
using System.Text.Json.Serialization;

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FabricaFrontend.Servicios
{
    public class Servicio_API_Usuario : IServicio_API_Usuario
    {
        private static string _user;
        public static string _pass;
        public static string _baseUrl;
        public static string _token;


        public Servicio_API_Usuario()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _user = builder.GetSection("ApiSettings:usuNombre").Value;
            _pass = builder.GetSection("ApiSettings:usuPass").Value;
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task Autenticar()
        {
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseUrl);

            var credenciales = new Credencial() { usuNombre = _user, usuPass = _pass };

            var content = new StringContent(JsonSerializer.Serialize(credenciales), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);
            var json_respuesta=await response.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<ResultadoCredencial>(json_respuesta);

            _token = resultado.token;
        }

        public async Task<IEnumerable<Usuario>> Lista()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Usuarios");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<IEnumerable<Usuario>>(json_respuesta);
                lista = resultado;
            }

            return lista;
        }

        public async Task<Usuario> Obtener(int idUsuario)
        {
            Usuario usuario = new Usuario();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Usuarios/{idUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Usuario>(json_respuesta);
                usuario = resultado;
            }
            return usuario;

        }

        public async Task<bool> Guardar(Usuario usuario)
        {
            bool respuesta = false;
            
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Usuarios/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Usuario usuario, int idUsuario)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Usuarios/{idUsuario}", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idUsuario)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Usuarios/{idUsuario}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
    }
}
