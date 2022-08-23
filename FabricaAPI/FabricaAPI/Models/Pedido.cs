using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FabricaAPI.Models
{
    public partial class Pedido
    {
        public int PedId { get; set; }
        public int? PedUsu { get; set; }
        public int? PedPro { get; set; }
        public decimal? PedVrUnit { get; set; }
        public double? PedCant { get; set; }
        public decimal? PedSubtot { get; set; }
        public double? PedIva { get; set; }
        public decimal? PedTotal { get; set; }
        
        public virtual Producto? oProducto { get; set; }
   
        public virtual Usuario? oUsuario { get; set; }
    }
}
