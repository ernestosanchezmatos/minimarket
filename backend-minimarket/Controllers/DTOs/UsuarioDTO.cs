using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_minimarket.Controllers.DTOs
{
    public class UsuarioDTO
    {
        public string dni { get; set; }
        public string nombres{ get; set; }
        public string aPaterno{ get; set; }
        public string aMaterno { get; set; }
        public DateTime fechaNac{ get; set; }
        public string direccion{ get; set; }
        public string telefono{ get; set; }
        public string email{ get; set; }
        public string user{ get; set; }
        public string password{ get; set; }              
        public string token { get; set; }
        public string[] data{ get; set; }

    }
}