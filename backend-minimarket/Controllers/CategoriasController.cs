using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using backend_minimarket.Controllers.DTOs;
using backend_minimarket.Entities;

namespace backend_minimarket.Controllers
{
    public class CategoriasController : ApiController
    {
        private Context db = new Context();

        // GET: api/categorias
        public IQueryable<CategoriaDTO> Getcategoria()
        {
            return db.categoria.Select(elem=>new CategoriaDTO {
                CategoriaId=elem.categoria_id,
                nCategoria = elem.categoria_nombre,
                DescCategoria = elem.categoria_description
            });
        }

        // GET: api/categorias/5
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Getcategoria(int id)
        {
            categoria categoria = db.categoria.Find(id);
            var objCatDto = new CategoriaDTO
            {
                CategoriaId = categoria.categoria_id,
                nCategoria = categoria.categoria_nombre,
                DescCategoria = categoria.categoria_description
            };
            if (objCatDto == null)
            {
                return NotFound();
            }

            return Ok(objCatDto);
        }

        // PUT: api/categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategoria(int id, categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.categoria_id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoriaExists(id))
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

        // POST: api/categorias
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Postcategoria(categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categoria.Add(categoria);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (categoriaExists(categoria.categoria_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoria.categoria_id }, categoria);
        }

        // DELETE: api/categorias/5
        [ResponseType(typeof(categoria))]
        public IHttpActionResult Deletecategoria(int id)
        {
            categoria categoria = db.categoria.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.categoria.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoriaExists(int id)
        {
            return db.categoria.Count(e => e.categoria_id == id) > 0;
        }
    }
}