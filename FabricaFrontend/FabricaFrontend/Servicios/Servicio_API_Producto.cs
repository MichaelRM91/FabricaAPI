using FabricaFrontend.Models;
using System.Text.Json.Serialization;

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FabricaFrontend.Servicios
{
    public class Servicio_API_Producto : IServicio_API_Producto
    {
        private static string _user;
        public static string _pass;
        public static string _baseUrl;
        public static string _token;


        public Servicio_API_Producto()
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

        public async Task<IEnumerable<Producto>> Lista()
        {
            IEnumerable<Producto> lista = new List<Producto>();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Productos");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<IEnumerable<Producto>>(json_respuesta);
                lista = resultado;
            }

            return lista;
        }

        public async Task<Producto> Obtener(int idProducto)
        {
            Producto producto = new Producto();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Productos/{idProducto}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Producto>(json_respuesta);
                producto = resultado;
            }
            return producto;

        }

        public async Task<bool> Guardar(Producto producto)
        {
            bool respuesta = false;
            
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Productos/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Producto producto, int idProducto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Productos/{idProducto}", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Productos/{idProducto}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
    }
}
