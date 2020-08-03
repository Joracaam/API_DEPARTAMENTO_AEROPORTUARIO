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
    public class DEPARTAMENTOController : ApiController
    {
        private NOMINA_AEROPORTUARIA_Entities db = new NOMINA_AEROPORTUARIA_Entities();

        // GET: api/DEPARTAMENTO
        public IQueryable<DEPARTAMENTO> GetDEPARTAMENTOes()
        {
            return db.DEPARTAMENTOes;
        }

        // GET: api/DEPARTAMENTO/5
        [ResponseType(typeof(DEPARTAMENTO))]
        public IHttpActionResult GetDEPARTAMENTO(int id)
        {
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTOes.Find(id);
            if (dEPARTAMENTO == null)
            {
                return NotFound();
            }

            return Ok(dEPARTAMENTO);
        }

        // PUT: api/DEPARTAMENTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDEPARTAMENTO(int id, DEPARTAMENTO dEPARTAMENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dEPARTAMENTO.DepartamentoId)
            {
                return BadRequest();
            }

            db.Entry(dEPARTAMENTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DEPARTAMENTOExists(id))
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

        // POST: api/DEPARTAMENTO
        [ResponseType(typeof(DEPARTAMENTO))]
        public IHttpActionResult PostDEPARTAMENTO(DEPARTAMENTO dEPARTAMENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DEPARTAMENTOes.Add(dEPARTAMENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dEPARTAMENTO.DepartamentoId }, dEPARTAMENTO);
        }

        // DELETE: api/DEPARTAMENTO/5
        [ResponseType(typeof(DEPARTAMENTO))]
        public IHttpActionResult DeleteDEPARTAMENTO(int id)
        {
            DEPARTAMENTO dEPARTAMENTO = db.DEPARTAMENTOes.Find(id);
            if (dEPARTAMENTO == null)
            {
                return NotFound();
            }

            db.DEPARTAMENTOes.Remove(dEPARTAMENTO);
            db.SaveChanges();

            return Ok(dEPARTAMENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DEPARTAMENTOExists(int id)
        {
            return db.DEPARTAMENTOes.Count(e => e.DepartamentoId == id) > 0;
        }
    }
}