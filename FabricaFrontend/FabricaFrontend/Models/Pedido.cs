namespace FabricaFrontend.Models
{
    public partial class Pedido
    {
        public int pedId { get; set; }
        public int? pedUsu { get; set; }
        public int? pedPro { get; set; }
        public decimal? pedVrUnit { get; set; }
        public double? pedCant { get; set; }
        public decimal? pedSubtot { get; set; }
        public double? pedIva { get; set; }
        public decimal? pedTotal { get; set; }

        public virtual Producto? oProducto { get; set; }

        public virtual Usuario? oUsuario { get; set; }

    }
}
