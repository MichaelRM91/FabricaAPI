using FabricaFrontend.Models;

namespace FabricaFrontend.Servicios
{
    public interface IServicio_API_Producto
    {
        Task<IEnumerable<Producto>> Lista();
        Task<Producto> Obtener(int idProducto);
        Task<bool> Guardar(Producto producto);
        Task<bool> Editar(Producto producto, int idProducto);

        Task<bool> Eliminar(int idProducto);

        
    }
}
