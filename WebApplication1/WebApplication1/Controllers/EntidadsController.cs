using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class EntidadsController : ApiController
    {
        private ProyectoBDJordiEntities1 db = new ProyectoBDJordiEntities1();

        String mensaje = "";
        // GET: api/Entidads
        public IQueryable<Entidad> GetEntidad()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Entidad;
        }

        // GET: api/Entidads/5
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult GetEntidad(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Entidad entidad = db.Entidad.Find(id);
            if (entidad == null)
            {
                return NotFound();
            }

            return Ok(entidad);
        }

        // PUT: api/Entidads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntidad(int id, Entidad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entidad.id)
            {
                return BadRequest();
            }

            db.Entry(entidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                    mensaje = Utilidades.Utilitats.MensajeError(sqlEx);
                    return BadRequest(mensaje);
                }
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidades.Utilitats.MensajeError(sqlEx);
                return BadRequest(mensaje);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Entidads
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult PostEntidad(Entidad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entidad.Add(entidad);
            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidades.Utilitats.MensajeError(sqlEx);
                return BadRequest(mensaje);
            }
            return CreatedAtRoute("DefaultApi", new { id = entidad.id }, entidad);
        }

        // DELETE: api/Entidads/5
        [ResponseType(typeof(Entidad))]
        public IHttpActionResult DeleteEntidad(int id)
        {
            Entidad entidad = db.Entidad.Find(id);
            if (entidad == null)
            {
                return NotFound();
            }

            db.Entidad.Remove(entidad);
            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                mensaje = Utilidades.Utilitats.MensajeError(sqlEx);
                return BadRequest(mensaje);
            }
            return Ok(entidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntidadExists(int id)
        {
            return db.Entidad.Count(e => e.id == id) > 0;
        }
    }
}