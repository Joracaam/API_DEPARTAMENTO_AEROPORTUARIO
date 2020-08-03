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
    public class NOMINAController : ApiController
    {
        private NOMINA_AEROPORTUARIA_Entities db = new NOMINA_AEROPORTUARIA_Entities();

        // GET: api/NOMINA
        public IQueryable<NOMINA> GetNOMINAs()
        {
            return db.NOMINAs;
        }

        // GET: api/NOMINA/5
        [ResponseType(typeof(NOMINA))]
        public IHttpActionResult GetNOMINA(int id)
        {
            NOMINA nOMINA = db.NOMINAs.Find(id);
            if (nOMINA == null)
            {
                return NotFound();
            }

            return Ok(nOMINA);
        }

        // GET: api/NOMINA/5
        [ResponseType(typeof(NOMINA))]
        public IHttpActionResult GetNOMINAByPuesto(int Puestoid)
        {
            List<NOMINA> nOMINA = db.NOMINAs.Where(nomina => nomina.FuncionId == Puestoid).ToList();
            if (nOMINA == null)
            {
                return NotFound();
            }

            return Ok(nOMINA);
        }

        // GET: api/NOMINA/5
        [ResponseType(typeof(NOMINA))]
        public IHttpActionResult GetNOMINAByPuestoEmpryInfo(int Puestoid, string mode)
        {
            List<NOMINA> nOMINA = db.NOMINAs.Where(nomina => nomina.FuncionId == Puestoid && 
            (nomina.Cedula == "000-0000000-0" || nomina.Nombre.ToUpper() == "NONE" || nomina.Apellido.ToUpper() == "NONE" || nomina.Salario == 0)).ToList();
            if (nOMINA == null)
            {
                return NotFound();
            }

            return Ok(nOMINA);
        }

        // PUT: api/NOMINA/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNOMINA(int id, NOMINA nOMINA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nOMINA.NominaId)
            {
                return BadRequest();
            }

            db.Entry(nOMINA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NOMINAExists(id))
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

        // POST: api/NOMINA
        [ResponseType(typeof(NOMINA))]
        public IHttpActionResult PostNOMINA(NOMINA nOMINA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NOMINAs.Add(nOMINA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nOMINA.NominaId }, nOMINA);
        }

        // DELETE: api/NOMINA/5
        [ResponseType(typeof(NOMINA))]
        public IHttpActionResult DeleteNOMINA(int id)
        {
            NOMINA nOMINA = db.NOMINAs.Find(id);
            if (nOMINA == null)
            {
                return NotFound();
            }

            db.NOMINAs.Remove(nOMINA);
            db.SaveChanges();

            return Ok(nOMINA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NOMINAExists(int id)
        {
            return db.NOMINAs.Count(e => e.NominaId == id) > 0;
        }
    }
}