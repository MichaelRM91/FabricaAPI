using System.Text.Json.Serialization;

namespace FabricaAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int UsuId { get; set; }
        public string UsuNombre { get; set; } = null!;
        public string UsuPass { get; set; } = null!;

        [JsonIgnore] 
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
