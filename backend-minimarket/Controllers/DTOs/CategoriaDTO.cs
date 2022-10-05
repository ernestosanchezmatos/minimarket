using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_minimarket.Controllers.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        public string nCategoria{ get; set; }
        public string DescCategoria{ get; set; }
    }
}