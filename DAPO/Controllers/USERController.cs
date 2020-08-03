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
using DAPO.Models;

namespace DAPO.Controllers
{
    public class USERController : ApiController
    {
        private NOMINA_AEROPORTUARIA_Entities db = new NOMINA_AEROPORTUARIA_Entities();

        // GET: api/USER
        public IQueryable<USER> GetUSERS()
        {
            return db.USERS;
        }

        // GET: api/USER/5
        [ResponseType(typeof(USER))]
        public IHttpActionResult GetUSER(int id)
        {
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return NotFound();
            }

            return Ok(uSER);
        }

        // GET: api/Usuarios/
        [ResponseType(typeof(USER))]
        public IHttpActionResult GetUsuarioLogin(string Username, string Password)
        {

            USER usuario = new USER();
            try
            {
                usuario = db.USERS.Where(user =>
                    (user.UserName == Username && user.UserPassword == Password))
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/USER/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUSER(int id, USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSER.UserId)
            {
                return BadRequest();
            }

            db.Entry(uSER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERExists(id))
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

        // POST: api/USER
        [ResponseType(typeof(USER))]
        public IHttpActionResult PostUSER(USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USERS.Add(uSER);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSER.UserId }, uSER);
        }

        // DELETE: api/USER/5
        [ResponseType(typeof(USER))]
        public IHttpActionResult DeleteUSER(int id)
        {
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return NotFound();
            }

            db.USERS.Remove(uSER);
            db.SaveChanges();

            return Ok(uSER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERExists(int id)
        {
            return db.USERS.Count(e => e.UserId == id) > 0;
        }
    }
}