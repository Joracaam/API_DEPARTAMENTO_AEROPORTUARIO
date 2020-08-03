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
    public class PUESTOController : ApiController
    {
        private NOMINA_AEROPORTUARIA_Entities db = new NOMINA_AEROPORTUARIA_Entities();

        // GET: api/PUESTO
        public IQueryable<PUESTO> GetPUESTOes()
        {
            return db.PUESTOes;
        }

        // GET: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult GetPUESTO(int id)
        {
            PUESTO pUESTO = db.PUESTOes.Find(id);
            if (pUESTO == null)
            {
                return NotFound();
            }

            return Ok(pUESTO);
        }

        // GET: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult GetPUESTObyDEPARTAMENTO(int DepartamentoId)
        {
            List<PUESTO> pUESTO = db.PUESTOes.Where(puesto => puesto.DepartamentoId == DepartamentoId).ToList();
            
            if (pUESTO == null || !pUESTO.Any())
            {
                return NotFound();
            }

            return Ok(pUESTO);
        }

        // PUT: api/PUESTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPUESTO(int id, PUESTO pUESTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pUESTO.PuestoId)
            {
                return BadRequest();
            }

            db.Entry(pUESTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PUESTOExists(id))
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

        // POST: api/PUESTO
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult PostPUESTO(PUESTO pUESTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PUESTOes.Add(pUESTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pUESTO.PuestoId }, pUESTO);
        }

        // DELETE: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult DeletePUESTO(int id)
        {
            PUESTO pUESTO = db.PUESTOes.Find(id);
            if (pUESTO == null)
            {
                return NotFound();
            }

            db.PUESTOes.Remove(pUESTO);
            db.SaveChanges();

            return Ok(pUESTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PUESTOExists(int id)
        {
            return db.PUESTOes.Count(e => e.PuestoId == id) > 0;
        }
    }
}