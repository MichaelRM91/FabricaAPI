using FabricaFrontend.Models;
using FabricaFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FabricaFrontend.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IServicio_API_Usuario _servicioApi ;

        public UsuariosController(IServicio_API_Usuario servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Usuario> Lista = await _servicioApi.Lista();
            return View(Lista);
        }

        public async Task<IActionResult> Usuario(int idUsuario)
        {
            Usuario modelo_usuario = new Usuario();

            ViewBag.Accion = "Nuevo Usuario";
            if (idUsuario != 0)
            {
                modelo_usuario = await _servicioApi.Obtener(idUsuario);
                ViewBag.Accion = "Editar Usuario";
            }
            return View(modelo_usuario);
          
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios (Usuario usuario)
        {
            bool respuesta;

            if(usuario.usuId == 0)
            {
                respuesta = await _servicioApi.Guardar(usuario);
            }
            else
            {
                respuesta = await _servicioApi.Editar(usuario, usuario.usuId);
            }

            if(respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
            
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idUsuario)
        {
            var respuesta = await _servicioApi.Eliminar(idUsuario);

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