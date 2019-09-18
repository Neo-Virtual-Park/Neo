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
    public class VagasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Vagas
        public ActionResult Index()
        {
            return View(db.Vaga.ToList());
        }

        // GET: Vagas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        // GET: Vagas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vagas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tipo,valor,QuantidadeHorasEspeciais,valorHorasEspeciais,Quantidade")] Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                db.Vaga.Add(vaga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaga);
        }

        // GET: Vagas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        // POST: Vagas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo,valor,Quantidade")] Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaga);
        }

        // GET: Vagas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        // POST: Vagas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaga vaga = db.Vaga.Find(id);
            db.Vaga.Remove(vaga);
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
