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

        // GET: Estacionamentoes
        public ActionResult Index()
        {
            var estacionamento = db.Estacionamento.Include(e => e.Funcionario).Include(e => e.Gerente);
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
        public ActionResult Create()
        {
            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email");
            ViewBag.GerenteId = new SelectList(db.Gerente, "Id", "Email");
            return View();
        }

        // POST: Estacionamentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nome,estado,bairro,rua,complemento,ativo,GerenteId")] Estacionamento estacionamento)
        {
            if (ModelState.IsValid)
            {
                db.Estacionamento.Add(estacionamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email", estacionamento.FuncionarioId);
            ViewBag.GerenteId = new SelectList(db.Gerente, "Id", "Email", estacionamento.GerenteId);
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
            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email", estacionamento.FuncionarioId);
            ViewBag.GerenteId = new SelectList(db.Gerente, "Id", "Email", estacionamento.GerenteId);
            return View(estacionamento);
        }

        // POST: Estacionamentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nome,estado,bairro,rua,complemento,ativo,GerenteId,FuncionarioId")] Estacionamento estacionamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estacionamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email", estacionamento.FuncionarioId);
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
