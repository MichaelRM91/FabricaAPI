using FabricaFrontend.Models;
using System.Text.Json.Serialization;

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FabricaFrontend.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _user;
        public static string _pass;
        public static string _baseUrl;
        public static string _token;


        public Servicio_API()
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

        public async Task<IEnumerable<Pedido>> Lista()
        {
            IEnumerable<Pedido> lista = new List<Pedido>();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Pedidos");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<IEnumerable<Pedido>>(json_respuesta);
                lista = resultado;
            }

            return lista;
        }

        public async Task<Pedido> Obtener(int idPedido)
        {
            Pedido pedido = new Pedido();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Pedidos/{idPedido}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<Pedido>(json_respuesta);
                pedido=resultado;
            }
            return pedido;

        }

        public async Task<bool> Guardar(Pedido pedido)
        {
            bool respuesta = false;
            
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(pedido), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Pedidos/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(Pedido pedido, int idPedido)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonSerializer.Serialize(pedido), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Pedidos/{idPedido}", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idPedido)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Pedidos/{idPedido}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
    }
}
