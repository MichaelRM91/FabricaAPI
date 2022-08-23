using FabricaFrontend.Models;

namespace FabricaFrontend.Servicios
{
    public interface IServicio_API_Usuario
    {
        Task<IEnumerable<Usuario>> Lista();
        Task<Usuario> Obtener(int idUsuario);
        Task<bool> Guardar(Usuario usuario);
        Task<bool> Editar(Usuario usuario, int idUsuario);

        Task<bool> Eliminar(int idUsuario);

        
    }
}
