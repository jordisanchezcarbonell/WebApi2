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

namespace WebApplication1.Controllers
{
    public class Act_concedidaController : ApiController
    {
        private ProyectoBDJordiEntities db = new ProyectoBDJordiEntities();

        // GET: api/Act_concedida
        public IQueryable<Act_concedida> GetAct_concedida()
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            return db.Act_concedida;
        }

        // GET: api/Act_concedida/5
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult GetAct_concedida(int id)
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            Act_concedida act_concedida = db.Act_concedida.Find(id);
            if (act_concedida == null)
            {
                return NotFound();
            }

            return Ok(act_concedida);
        }

        // PUT: api/Act_concedida/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAct_concedida(int id, Act_concedida act_concedida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act_concedida.id)
            {
                return BadRequest();
            }

            db.Entry(act_concedida).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Act_concedidaExists(id))
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

        // POST: api/Act_concedida
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult PostAct_concedida(Act_concedida act_concedida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Act_concedida.Add(act_concedida);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = act_concedida.id }, act_concedida);
        }

        // DELETE: api/Act_concedida/5
        [ResponseType(typeof(Act_concedida))]
        public IHttpActionResult DeleteAct_concedida(int id)
        {
            Act_concedida act_concedida = db.Act_concedida.Find(id);
            if (act_concedida == null)
            {
                return NotFound();
            }

            db.Act_concedida.Remove(act_concedida);
            db.SaveChanges();

            return Ok(act_concedida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Act_concedidaExists(int id)
        {
            return db.Act_concedida.Count(e => e.id == id) > 0;
        }
    }
}