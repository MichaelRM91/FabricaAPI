using FabricaFrontend.Models;

namespace FabricaFrontend.Servicios
{
    public interface IServicio_API
    {
        Task<IEnumerable<Pedido>> Lista();
        Task<Pedido> Obtener(int idPedido);
        Task<bool> Guardar(Pedido pedido);
        Task<bool> Editar(Pedido pedido, int idPedido);

        Task<bool> Eliminar(int idPedido);

        
    }
}
