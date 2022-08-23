using FabricaFrontend.Models;
using FabricaFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FabricaFrontend.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IServicio_API _servicioApi ;

        public PedidosController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Pedido> Lista = await _servicioApi.Lista();
            return View(Lista);
        }

        public async Task<IActionResult> Pedido(int idPedido)
        {
            Pedido modelo_pedido = new Pedido();

            ViewBag.Accion = "Nuevo Pedido";
            if (idPedido != 0)
            {
                modelo_pedido = await _servicioApi.Obtener(idPedido);
                ViewBag.Accion = "Editar Pedido";
            }
            return View(modelo_pedido);
          
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios (Pedido pedido)
        {
            bool respuesta;

            if(pedido.pedId == 0)
            {
                respuesta = await _servicioApi.Guardar(pedido);
            }
            else
            {
                respuesta = await _servicioApi.Editar(pedido, pedido.pedId);
            }

            if(respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
            
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idPedido)
        {
            var respuesta = await _servicioApi.Eliminar(idPedido);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}