using FabricaFrontend.Models;
using FabricaFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FabricaFrontend.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IServicio_API_Producto _servicioApi ;

        public ProductosController(IServicio_API_Producto servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> Lista = await _servicioApi.Lista();
            return View(Lista);
        }

        public async Task<IActionResult> Producto(int idProducto)
        {
            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";
            if (idProducto != 0)
            {
                modelo_producto = await _servicioApi.Obtener(idProducto);
                ViewBag.Accion = "Editar Producto";
            }
            return View(modelo_producto);
          
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios (Producto producto)
        {
            bool respuesta;

            if(producto.proId == 0)
            {
                respuesta = await _servicioApi.Guardar(producto);
            }
            else
            {
                respuesta = await _servicioApi.Editar(producto, producto.proId);
            }

            if(respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
            
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            var respuesta = await _servicioApi.Eliminar(idProducto);

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