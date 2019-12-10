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
    public class SangriasController : Controller
    {
        private Contexto db = new Contexto();
        public static double dinheironocaixa = 0;

        // GET: Sangrias
        public ActionResult Index()
        {
            var sangria = db.sangria.Include(s => s.Funcionario);
            return View(sangria.ToList());
        }

        // GET: Sangrias/Create
        public ActionResult Create()
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);
            Funcionario fun = db.Funcionario.Where(f => f.Id == sl).FirstOrDefault();
            Estacionamento est = db.Estacionamento.Where(e => e.Id == fun.EstacionamentoId).FirstOrDefault();
            ViewBag.Data = DateTime.Now;

            var lista = db.Movimentacao.Where(x => x.Valor_pagar > 0 && x.Funcionario.EstacionamentoId == est.Id).ToList();
            ViewBag.ValorNoCaixa = est.Customizacoes.ValorInicialCaixa;

            foreach(var item in lista)
            {
                ViewBag.ValorNoCaixa += item.Valor_pagar;
            }
            dinheironocaixa = Convert.ToDouble(ViewBag.ValorNoCaixa);
            return View();
        }

        //// POST: Sangrias/Create
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descricao,valor")] Sangria sangria)
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);
            Funcionario fun = db.Funcionario.Where(f => f.Id == sl).FirstOrDefault();
            Estacionamento est = db.Estacionamento.Where(e => e.Id == fun.EstacionamentoId).FirstOrDefault();
            sangria.confirmar = 0;
            sangria.FuncionarioId = fun.Id;
            sangria.horadasangria = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (sangria.valor <= dinheironocaixa)
                {
                    db.sangria.Add(sangria);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Movimentacaos");
                }
                else
                {
                    TempData["MSG"] = "Valor muito alto";
                }
              
            }
            ViewBag.Data = DateTime.Now;
            var lista = db.Movimentacao.Where(x => x.Valor_pagar > 0 && x.Funcionario.EstacionamentoId == est.Id).ToList();
            ViewBag.ValorNoCaixa = est.Customizacoes.ValorInicialCaixa;

            foreach (var item in lista)
            {
                ViewBag.ValorNoCaixa += item.Valor_pagar;
            }
            dinheironocaixa = Convert.ToDouble(ViewBag.ValorNoCaixa);
            return View(sangria);
        }

        // GET: Sangrias/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sangria sangria = db.sangria.Find(id);
        //    if (sangria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email", sangria.FuncionarioId);
        //    return View(sangria);
        //}

        //// POST: Sangrias/Edit/5
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,descricao,valor,FuncionarioId")] Sangria sangria)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sangria).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email", sangria.FuncionarioId);
        //    return View(sangria);
        //}

        //// GET: Sangrias/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Sangria sangria = db.sangria.Find(id);
        //    if (sangria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sangria);
        //}

        //// POST: Sangrias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Sangria sangria = db.sangria.Find(id);
        //    db.sangria.Remove(sangria);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
