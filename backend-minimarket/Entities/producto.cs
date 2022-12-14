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
    
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.detalle_pedido = new HashSet<detalle_pedido>();
        }
    
        public int producto_id { get; set; }
        public string producto_nombre { get; set; }
        public int producto_stock { get; set; }
        public decimal producto_precio { get; set; }
        public string producto_description { get; set; }
        public bool producto_disponible { get; set; }
        public int marca_marca_id { get; set; }
        public int categoria_categoria_id { get; set; }
        public string imagen { get; set; }
    
        public virtual categoria categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_pedido> detalle_pedido { get; set; }
        public virtual marca marca { get; set; }
    }
}
