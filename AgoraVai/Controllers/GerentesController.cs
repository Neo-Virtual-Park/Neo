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

        static private int idgerente = 0;

        // GET: Gerentes
        public ActionResult Index()
        {
            DateTime teste = DateTime.Now;
            calcularcards();
            calculargraf(teste.Year.ToString());

            int sl = 0;
            sl = Convert.ToInt32(Session["GenID"]);
            idgerente = sl;
            Gerente gen = db.Gerente.Find(sl);
            ViewBag.nomedousuario = gen.Pessoa.Nome;
            //ViewBag.Ex = db.Movimentacao.Where(x => x.Hora_saida != null).Sum(x => x.Valor_pagar == null ? 0 : x.Valor_pagar);
            return View();
        }

        public ActionResult Customizar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Customizar(Customizacoes cust)
        {
            Gerente gen = db.Gerente.Find(idgerente);
            Estacionamento est = db.Estacionamento.Where(x => x.GerenteId == gen.Id).ToList().FirstOrDefault();
            if (est.Customizacoes == null)
            {
                est.Customizacoes = new Customizacoes();
                est.Customizacoes.MinutosDeTolerancia = cust.MinutosDeTolerancia;
                est.Customizacoes.ValorInicialCaixa = cust.ValorInicialCaixa;
                db.Customizacoes.Add(est.Customizacoes);
                db.Entry(est).State = EntityState.Modified;
                db.SaveChanges();
                est.CustomizacoesId = db.Customizacoes.Where(x => x.Id == est.Customizacoes.Id).ToList().LastOrDefault().Id;
                db.SaveChanges();
            }
            else
            {
                est.Customizacoes.MinutosDeTolerancia = cust.MinutosDeTolerancia;
                est.Customizacoes.ValorInicialCaixa = cust.ValorInicialCaixa;
                db.Entry(est).State = EntityState.Modified;
                db.SaveChanges();
            }
            TempData["MSG"] = "Dados Salvos";
            return View();
        }

        public ActionResult SangriasLista()
        {
            var Sangrias = db.sangria.Where(s => s.Funcionario.Estacionamento.GerenteId == idgerente).ToList();
            return View(Sangrias);
        }

        public ActionResult SangriasLista2(int Id)
        {
            Sangria sans = db.sangria.Where(s => s.Id == Id).FirstOrDefault();
            sans.confirmar = 1;
            db.Entry(sans).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("SangriasLista");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,Senha,PessoaId")] Gerente gerente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(gerente).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
        //    return View(gerente);
        //}

        public void calcularcards()
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["GenID"]);
            decimal? dia = 0;
            decimal? sem = 0;
            decimal? mes = 0;
            decimal? html = 1260;
            ViewBag.qualquercoisa = html;

            DateTime data;

            foreach (var naofacoideia in db.Movimentacao.Where(x => x.FuncionarioId == sl))
            {
                data = Convert.ToDateTime(naofacoideia.Hora_saida);

                if (naofacoideia.Hora_saida != null && data.Month == DateTime.Now.Month && data.Year == DateTime.Now.Year)
                {
                    if (data.Day == DateTime.Now.Day)
                        dia += naofacoideia.Valor_pagar;

                    if (data.Day <= DateTime.Now.Day + 7 && data.Day >= DateTime.Now.Day - 7)
                    {
                        sem += naofacoideia.Valor_pagar;
                    }

                    mes += naofacoideia.Valor_pagar;
                }

            }

            ViewBag.FaturamentoDoDia = dia;
            ViewBag.FaturamentoDaSemana = sem;
            ViewBag.FaturamentoDoMes = mes;
        }

        public void calculargraf(string data)
        {
            int datacon = Convert.ToInt32(data);
            decimal[] mes = new decimal[12];
            int sl = 0;
            sl = Convert.ToInt32(Session["GenID"]);
            DateTime data2;
            // DateTime data3 = Convert.ToDateTime(datacon);

            foreach (var naofacoideia in db.Movimentacao.Where(x => x.FuncionarioId == sl))
            {
                data2 = Convert.ToDateTime(naofacoideia.Hora_saida);
                for (int i = 0; i <= 11; i++)
                {
                    if (naofacoideia.Hora_saida != null && data2.Month == i + 1 && data2.Year == datacon)
                    {
                        mes[i] += naofacoideia.Valor_pagar;
                    }
                }

            }
            ViewBag.jan = mes[0].ToString().Replace(',', '.');
            ViewBag.fev = mes[1].ToString().Replace(',', '.');
            ViewBag.mar = mes[2].ToString().Replace(',', '.');
            ViewBag.abr = mes[3].ToString().Replace(',', '.');
            ViewBag.mai = mes[4].ToString().Replace(',', '.');
            ViewBag.jun = mes[5].ToString().Replace(',', '.');
            ViewBag.jul = mes[6].ToString().Replace(',', '.');
            ViewBag.ago = mes[7].ToString().Replace(',', '.');
            ViewBag.set = mes[8].ToString().Replace(',', '.');
            ViewBag.outu = mes[9].ToString().Replace(',', '.');
            ViewBag.nov = mes[10].ToString().Replace(',', '.');
            ViewBag.dez = mes[11].ToString().Replace(',', '.');
        }

        [HttpPost]
        public ActionResult Index(string data)
        {
            calcularcards();
            calculargraf(data);
            return View();
        }

        public ActionResult Registros()
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["GenID"]);
            ViewBag.soma = 0;
            Gerente gen = db.Gerente.Find(sl);
            var mov = db.Movimentacao.Where(x => x.Valor_pagar > 0 && x.Funcionario.Estacionamento.GerenteId == gen.Id).ToList();
            foreach (var item in mov)
            {
                ViewBag.soma += item.Valor_pagar;
            }
            return View(mov);
        }

        [HttpPost]
        public ActionResult Registros(string data3, string data4)
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["GenID"]);
            DateTime data1 = Convert.ToDateTime(data3);
            DateTime data2 = Convert.ToDateTime(data4);
            Gerente gen = db.Gerente.Find(sl);
            ViewBag.soma = 0;
            if (data1 < data2)
            {
                var mov = db.Movimentacao.Where(x => x.Valor_pagar > 0 && x.Funcionario.Estacionamento.GerenteId == gen.Id).Where(x => x.Hora_saida >= data1.Date && x.Hora_saida <= data2.Date).ToList();
                foreach (var item in mov)
                {
                    ViewBag.soma += item.Valor_pagar;
                }
                return View(mov);
            }
            else
            {
                var mov = db.Movimentacao.Where(x => x.Valor_pagar > 0 && x.Funcionario.Estacionamento.GerenteId == gen.Id && x.Hora_saida >= data2 && x.Hora_saida <= data1).ToList();
                foreach (var item in mov)
                {
                    ViewBag.soma += item.Valor_pagar;
                }
                return View(mov);
            }
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

        //// GET: Gerentes/Create


        //// GET: Gerentes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Gerente gerente = db.Gerente.Find(id);
        //    if (gerente == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
        //    return View(gerente);
        //}

        //// POST: Gerentes/Edit/5
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,Senha,PessoaId")] Gerente gerente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(gerente).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", gerente.PessoaId);
        //    return View(gerente);
        //}

        // GET: Gerentes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Gerente gerente = db.Gerente.Find(id);
        //    if (gerente == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gerente);
        //}

        //// POST: Gerentes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Gerente gerente = db.Gerente.Find(id);
        //    db.Gerente.Remove(gerente);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
