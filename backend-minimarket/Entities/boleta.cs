//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace backend_minimarket.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class boleta
    {
        public int bol_id { get; set; }
        public int pedido_pe_id { get; set; }
        public System.DateTime fecha { get; set; }
        public System.TimeSpan hora { get; set; }
        public string bol_serie { get; set; }
        public bool bol_flagCancelado { get; set; }
        public decimal bol_igv { get; set; }
        public decimal bol_importeTotal { get; set; }
        public decimal bol_OperacionGrabada { get; set; }
    
        public virtual pedido pedido { get; set; }
    }
}