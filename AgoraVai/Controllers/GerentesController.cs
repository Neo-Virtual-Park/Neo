using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgoraVai.Models;

namespace AgoraVai.Controllers
{
    public class GerentesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Gerentes
        public ActionResult Index()
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);
            float? i = 0;
            DateTime data;
            foreach (var naofacoideia in db.Movimentacao.Where(x => x.FuncionarioId == sl)){
                data = Convert.ToDateTime(naofacoideia.Hora_saida);
                
                if (naofacoideia.Hora_saida != null && data.Day == DateTime.Now.Day)
                {
                    i += naofacoideia.Valor_pagar;
                }
               
            }

            ViewBag.FaturamentoDoDia = i;

            //ViewBag.Ex = db.Movimentacao.Where(x => x.Hora_saida != null).Sum(x => x.Valor_pagar == null ? 0 : x.Valor_pagar);
            return View();
        }

        // GET: Gerentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gerente gerente = db.Gerente.Find(id);
            if (gerente == null)
            {
                return HttpNotFound();
            }
            return View(gerente);
        }

        // GET: Gerentes/Create
        public ActionResult Create()
        {
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome");
            return View();
        }

        // POST: Gerentes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Senha,PessoaId")] Gerente gerente)
        {
            if (ModelState.IsValid)
            {
                db.Gerente.Add(gerente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
            return View(gerente);
        }

        // GET: Gerentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gerente gerente = db.Gerente.Find(id);
            if (gerente == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
            return View(gerente);
        }

        // POST: Gerentes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Senha,PessoaId")] Gerente gerente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gerente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
            return View(gerente);
        }

        // GET: Gerentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gerente gerente = db.Gerente.Find(id);
            if (gerente == null)
            {
                return HttpNotFound();
            }
            return View(gerente);
        }

        // POST: Gerentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gerente gerente = db.Gerente.Find(id);
            db.Gerente.Remove(gerente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
