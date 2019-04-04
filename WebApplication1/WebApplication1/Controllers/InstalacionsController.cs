﻿using System;
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
    public class InstalacionsController : ApiController
    {
        String mensaje ="";
        private ProyectoBDJordiEntities1 db = new ProyectoBDJordiEntities1();

        // GET: api/Instalacions
        public IQueryable<Instalacion> GetInstalacion()
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            return db.Instalacion;
        }

        // GET: api/Instalacions/5
        [ResponseType(typeof(Instalacion))]
        public IHttpActionResult GetInstalacion(int id)
        {
            //Evito que em retorni tots els objectes enllaçats:
            db.Configuration.LazyLoadingEnabled = false;

            Instalacion instalacion = db.Instalacion.Find(id);
            if (instalacion == null)
            {
                return NotFound();
            }

            return Ok(instalacion);
        }

        // PUT: api/Instalacions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstalacion(int id, Instalacion instalacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instalacion.id)
            {
                return BadRequest();
            }

            db.Entry(instalacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex )
            {
                if (!InstalacionExists(id))
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

        // POST: api/Instalacions
        [ResponseType(typeof(Instalacion))]
        public IHttpActionResult PostInstalacion(Instalacion instalacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Instalacion.Add(instalacion);
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

            return CreatedAtRoute("DefaultApi", new { id = instalacion.id }, instalacion);
        }

        // DELETE: api/Instalacions/5
        [ResponseType(typeof(Instalacion))]
        public IHttpActionResult DeleteInstalacion(int id)
        {
            Instalacion instalacion = db.Instalacion.Find(id);
            if (instalacion == null)
            {
                return NotFound();
            }

            db.Instalacion.Remove(instalacion);
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

            return Ok(instalacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstalacionExists(int id)
        {
            return db.Instalacion.Count(e => e.id == id) > 0;
        }
    }
}