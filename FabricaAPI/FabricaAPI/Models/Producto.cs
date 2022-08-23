using System.Text.Json.Serialization;

namespace FabricaAPI.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int ProId { get; set; }
        public string? ProDesc { get; set; }
        public decimal? ProValor { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
