using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_minimarket.Controllers.DTOs
{
    public class ProductoDTO
    {
        public int id { get; set; }

        public string name { get; set; }
            
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public bool disponibleProducto { get; set; }
        public string marcaId { get; set; }
        public string categoryId{ get; set; }
        public string nMarca { get; set; }
        public string category { get; set; }
        public string images { get; set; }
        public string image { get; set; }


    }
}