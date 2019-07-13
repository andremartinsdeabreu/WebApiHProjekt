using ProjetoWebApiH.Data;
using ProjetoWebApiH.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjetoWebApiH.Controllers
{
    public class TarefasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tarefas
        [HttpGet]
        public IQueryable<Tarefa> ObterTarefas(bool? isDone)
        {
            if (isDone != null)
                return db.Tarefas.Where(x => x.IsDone == isDone);

            return db.Tarefas;
        }

        // PUT: api/Tarefas/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult AtualizaTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var update = db.Tarefas.Where(x => x.Id == tarefa.Id).SingleOrDefault();
                if (update != null)
                {
                    update.Title = tarefa.Title;
                    update.IsDone = tarefa.IsDone;
                    update.Editing = false;

                    db.Entry(update).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(tarefa.Id))
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

        // POST: api/Tarefas
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult CadastrarTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarefas.Add(tarefa);
            db.SaveChanges();

            return Ok(tarefa);
        }

        // DELETE: api/Tarefas/5
        [ResponseType(typeof(Tarefa))]
        [HttpDelete]
        public IHttpActionResult DeleteTarefa(int id)
        {
            if (id == 0)
            {
                var tarefas = db.Tarefas.Where(x => x.IsDone == true).ToList();
                if (tarefas == null)
                {
                    return NotFound();
                }
                tarefas.ForEach(tarefa => db.Tarefas.Remove(tarefa));
                db.SaveChanges();
            }
            else
            {
                Tarefa tarefa = db.Tarefas.Find(id);
                if (tarefa == null)
                {
                    return NotFound();
                }

                db.Tarefas.Remove(tarefa);
                db.SaveChanges();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarefaExists(int id)
        {
            return db.Tarefas.Count(e => e.Id == id) > 0;
        }
    }
}