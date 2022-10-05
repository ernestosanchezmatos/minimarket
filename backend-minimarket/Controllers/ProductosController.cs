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
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductosController : ApiController
    {
        private Context db = new Context();

        // GET: api/Productos
        [Route("api/Productos/listProducts")]
        public List<ProductoDTO> Getproducto(string limit, string page)
        {
            int _limit = Convert.ToInt32(limit);
            int _page = Convert.ToInt32(page);
            var arrProductos = db.producto
                .Include(e => e.categoria)
                .Select(elem => new ProductoDTO
                {
                    id = elem.producto_id,
                    name = elem.producto_nombre,
                    category = elem.categoria.categoria_nombre,
                    description = elem.producto_description,
                    image = elem.imagen,
                    price = elem.producto_precio,
                    quantity = elem.producto_stock

                }).ToArray();

            var arrProductosFiltro = new List<ProductoDTO>();
            int sizeArr = arrProductos.Length;

            int IndiceMax = _page * _limit > sizeArr ? sizeArr : _page * _limit;
            for (int i = (_page - 1) * _limit; i < IndiceMax; i++)
            {
                arrProductosFiltro.Add(arrProductos[i]);
            }

            return arrProductosFiltro;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(ProductoDTO))]
        public IHttpActionResult Getproducto(int id)
        {
            var objProducto = db.producto
                        .Include(e => e.categoria)
                        .Include(e => e.marca)
                        .FirstOrDefault(e => e.producto_id == id);
            ProductoDTO objRetorno = null;
            if (objProducto != null)
            {
                objRetorno = new ProductoDTO
                {
                    id = objProducto.producto_id,
                    name = objProducto.producto_nombre,
                    category = objProducto.categoria.categoria_nombre,
                    description = objProducto.producto_description,
                    image = objProducto.imagen,
                    price = objProducto.producto_precio,
                    quantity = objProducto.producto_stock
                };
            }
            return Ok(objRetorno);
        }

        [Route("api/Productos/registrarProducto")]
        [HttpPost]
        public IHttpActionResult RegistrarCliente([FromBody] ProductoDTO obj)
        {
            producto objProducto = new producto
            {
                producto_nombre = obj.name,
                producto_stock = obj.quantity,
                producto_precio = obj.price,
                producto_description = obj.description,
                imagen = obj.image,
                categoria_categoria_id = Convert.ToInt32(obj.categoryId),
                marca_marca_id = Convert.ToInt32(obj.marcaId),
                producto_disponible = true
            };

            db.producto.Add(objProducto);
            db.SaveChanges();
            return Ok();
        }

        [Route("api/Productos/getMarcasSelect")]
        [HttpGet]
        public IHttpActionResult ListarMarcas()
        {
            return Ok(db.marca.Select(e=>new SelectDTO {id=e.marca_id.ToString(),value=e.marca_nombre }).ToList());
        }

        [Route("api/Productos/getCategoriasSelect")]
        [HttpGet]
        public IHttpActionResult ListarCategorias()
        {
            return Ok(db.categoria.Select(e => new SelectDTO { id = e.categoria_id.ToString(), value = e.categoria_nombre }).ToList());
        }


        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproducto(int id, producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.producto_id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Productos
   
        [ResponseType(typeof(producto))]
        public IHttpActionResult Postproducto(producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.producto.Add(producto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (productoExists(producto.producto_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = producto.producto_id }, producto);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(producto))]
        public IHttpActionResult Deleteproducto(int id)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.producto.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productoExists(int id)
        {
            return db.producto.Count(e => e.producto_id == id) > 0;
        }
    }
}