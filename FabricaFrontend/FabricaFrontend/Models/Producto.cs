using System.Text.Json.Serialization;

namespace FabricaFrontend.Models
{
    public partial class Producto
    {
       
        public int proId { get; set; }
        public string? proDesc { get; set; }
        public decimal? proValor { get; set; }
       
    }
}
