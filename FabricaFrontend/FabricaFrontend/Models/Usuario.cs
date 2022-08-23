using System.Text.Json.Serialization;

namespace FabricaFrontend.Models
{
    public partial class Usuario
    {
      

        public int usuId { get; set; }
        public string usuNombre { get; set; } = null!;
        public string usuPass { get; set; } = null!;

       
    }
}
