using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using backend_minimarket.Controllers.DTOs;
using backend_minimarket.Entities;


namespace backend_minimarket.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
        private Context db = new Context();

        // POST: api/Usuarios
        [Route("api/Usuarios/login")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] UsuarioDTO obj)
        {
            UsuarioDTO objResponse = default(UsuarioDTO);
            var objUsuario = db.usuario.FirstOrDefault(e => obj.email == e.u_correo && obj.password == e.password);
            if (objUsuario == null) return Ok(objResponse);
            else
            {
                objResponse = new UsuarioDTO();
                objResponse.token = Guid.NewGuid().ToString();
                objResponse.data = new string[3];
                objResponse.data[0] = objUsuario.user;
                return Ok(objResponse);
            }
        }

        [Route("api/Usuarios/registrarCliente")]
        [HttpPost]
        public IHttpActionResult RegistrarCliente([FromBody] UsuarioDTO obj)
        {
            usuario objUsuario = new usuario
            {
                u_dni=obj.dni,
                password=obj.password,
                rol_rol_id=2,
                user=obj.user,
                u_apellidoMaterno=obj.aMaterno,
                u_apellidoPaterno=obj.aPaterno,
                u_correo=obj.email,
                u_direccion=obj.direccion,
                u_estado=true,
                u_nombres=obj.nombres,
                u_fechaNac=obj.fechaNac,
                u_telefono=obj.telefono                
            };

            db.usuario.Add(objUsuario);
            db.SaveChanges();
            return Ok();
        }

    }
}