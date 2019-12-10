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
    public class EstacionamentoesController : Controller
    {
        private Contexto db = new Contexto();

        static public int IdDoGerente = 0;

        // GET: Estacionamentoes
        public ActionResult Index()
        {
            var estacionamento = db.Estacionamento.Include(e => e.Gerente);
            return View(estacionamento.ToList());
        }

        // GET: Estacionamentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamento estacionamento = db.Estacionamento.Find(id);
            if (estacionamento == null)
            {
                return HttpNotFound();
            }
            return View(estacionamento);
        }

        // GET: Estacionamentoes/Create
        public ActionResult Create(int Id)
        {
            IdDoGerente = Id;
            return View();
        }

        // POST: Estacionamentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nome,estado,bairro,rua,complemento")] Estacionamento estacionamento)
        {
         
            if (ModelState.IsValid)
            {
                estacionamento.GerenteId = IdDoGerente;
                estacionamento.ativo = true;
                db.Estacionamento.Add(estacionamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estacionamento);
        }

        // GET: Estacionamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamento estacionamento = db.Estacionamento.Find(id);
            if (estacionamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomizacoesId = new SelectList(db.Customizacoes, "Id", "Id", estacionamento.CustomizacoesId);
            ViewBag.GerenteId = new SelectList(db.Gerente, "Id", "Email", estacionamento.GerenteId);
            return View(estacionamento);
        }

        // POST: Estacionamentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nome,estado,bairro,rua,complemento,ativo,GerenteId,CustomizacoesId")] Estacionamento estacionamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estacionamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomizacoesId = new SelectList(db.Customizacoes, "Id", "Id", estacionamento.CustomizacoesId);
            ViewBag.GerenteId = new SelectList(db.Gerente, "Id", "Email", estacionamento.GerenteId);
            return View(estacionamento);
        }

        // GET: Estacionamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estacionamento estacionamento = db.Estacionamento.Find(id);
            if (estacionamento == null)
            {
                return HttpNotFound();
            }
            return View(estacionamento);
        }

        // POST: Estacionamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estacionamento estacionamento = db.Estacionamento.Find(id);
            db.Estacionamento.Remove(estacionamento);
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
