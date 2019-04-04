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
using WebApplication1;
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
namespace WebApplication1.Controllers
{
    public class EquipoController : ApiController
    {
        private ProyectoBDJordiEntities1 db = new ProyectoBDJordiEntities1();

        String mensaje = "";
        // GET: api/Equipo
        public IQueryable<Equipo> GetEquipo()
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            return db.Equipo;
        }

        // GET: api/Equipo/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult GetEquipo(int id)
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        // PUT: api/Equipo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipo(int id, Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipo.id)
            {
                return BadRequest();
            }

            db.Entry(equipo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EquipoExists(id))
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
            catch(DbUpdateException ex)
            {
                SqlException sqlEx = (SqlException)ex.InnerException.InnerException;
                   mensaje = Utilidades.Utilitats.MensajeError(sqlEx);
                return BadRequest(mensaje);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Equipo
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult PostEquipo(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipo.Add(equipo);
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


            return CreatedAtRoute("DefaultApi", new { id = equipo.id }, equipo);
        }

        // DELETE: api/Equipo/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult DeleteEquipo(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            db.Equipo.Remove(equipo);
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

            return Ok(equipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoExists(int id)
        {
            return db.Equipo.Count(e => e.id == id) > 0;
        }
    }
}