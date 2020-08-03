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
    public class VW_NOMINAController : ApiController
    {
        private NOMINA_AEROPORTUARIA_Entities db = new NOMINA_AEROPORTUARIA_Entities();

        // GET: api/VW_NOMINA
        public IQueryable<VW_Nomina> GetVW_Nomina()
        {
            return db.VW_Nomina;
        }

        // GET: api/VW_NOMINA/5
        [ResponseType(typeof(VW_Nomina))]
        public IHttpActionResult GetVW_Nomina(int id)
        {
            VW_Nomina vW_Nomina = db.VW_Nomina.Find(id);
            if (vW_Nomina == null)
            {
                return NotFound();
            }

            return Ok(vW_Nomina);
        }

        // PUT: api/VW_NOMINA/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVW_Nomina(int id, VW_Nomina vW_Nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vW_Nomina.NominaId)
            {
                return BadRequest();
            }

            db.Entry(vW_Nomina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VW_NominaExists(id))
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

        // POST: api/VW_NOMINA
        [ResponseType(typeof(VW_Nomina))]
        public IHttpActionResult PostVW_Nomina(VW_Nomina vW_Nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VW_Nomina.Add(vW_Nomina);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VW_NominaExists(vW_Nomina.NominaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vW_Nomina.NominaId }, vW_Nomina);
        }

        // DELETE: api/VW_NOMINA/5
        [ResponseType(typeof(VW_Nomina))]
        public IHttpActionResult DeleteVW_Nomina(int id)
        {
            VW_Nomina vW_Nomina = db.VW_Nomina.Find(id);
            if (vW_Nomina == null)
            {
                return NotFound();
            }

            db.VW_Nomina.Remove(vW_Nomina);
            db.SaveChanges();

            return Ok(vW_Nomina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VW_NominaExists(int id)
        {
            return db.VW_Nomina.Count(e => e.NominaId == id) > 0;
        }
    }
}