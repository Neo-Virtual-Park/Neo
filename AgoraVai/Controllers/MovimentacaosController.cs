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
    public class MovimentacaosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Movimentacaos
        public ActionResult Index()
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);
            //ViewBag.ArrecadacaoDia = db.Movimentacao.Where(x => x.Hora_saida == DateTime.Now).Sum(x => x.Valor_pagar == 0f ? 0 : x.Valor_pagar);



            // ViewBag.Ex = db.Movimentacao.Where(x => x.Hora_saida != null).Sum(x => x.Valor_pagar == null ? 0 : x.Valor_pagar);

            // var movimentacao = db.Movimentacao.Include(m => m.Funcionario).Include(m => m.Vaga);

            //db.Movimentacao.ToList()

            Funcionario fun = db.Funcionario.Find(sl);
            ViewBag.nomedousuario = fun.Pessoa.Nome;
            return View(db.Movimentacao.Where(x => x.Valor_pagar == 0 && x.Funcionario.EstacionamentoId == fun.EstacionamentoId).ToList());
        }

        // GET: Movimentacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // GET: Movimentacaos/Create
        public ActionResult Create()
        {
            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email");
            ViewBag.VagaId = new SelectList(db.Vaga, "Id", "Tipo");
            return View();
        }

        // POST: Movimentacaos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cpf,Telefone,Placa,hora_entrada,FuncionarioId,Hora_saida,Valor_pagar,VagaId")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                db.Movimentacao.Add(movimentacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Nome", movimentacao.FuncionarioId);
            ViewBag.VagaId = new SelectList(db.Vaga, "Id", "Tipo", movimentacao.VagaId);
            return View(movimentacao);
        }

        // GET: Movimentacaos/Edit/5
        public ActionResult Edit(int? id)
        {
            Movimentacao mov = db.Movimentacao.Find(id);
            ViewBag.horaSaida = DateTime.Now;
            ViewBag.Vp = calcularpreco(mov.hora_entrada, DateTime.Now, mov.Vaga);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacaos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Cpf, Telefone, Placa, hora_entrada, FuncionarioId, Hora_saida, Valor_pagar, VagaId")] Movimentacao movimentacao, int? id)
        {
            Movimentacao mov = db.Movimentacao.Find(id);
            //movimentacao.VagaId = mov.VagaId;
            //var vag = db.Vaga.FirstOrDefault(p => p.Id == mov.VagaId);
            //movimentacao.Cpf = mov.Cpf;
            //movimentacao.Telefone = mov.Telefone;
            //movimentacao.Placa = mov.Placa;
            //movimentacao.hora_entrada = mov.hora_entrada;
            //movimentacao.FuncionarioId = mov.FuncionarioId;
            mov.Hora_saida = DateTime.Now;
            mov.Valor_pagar = calcularpreco(mov.hora_entrada, mov.Hora_saida.Value, mov.Vaga);

            if (ModelState.IsValid)
            {
                db.Entry(mov).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = mov.Id });

            }
            return View(movimentacao);
        }

        public float calcularpreco(DateTime ent, DateTime sai, Vaga vag)
        {
            float total = 0f;
            TimeSpan tmp = sai - ent;
            int conta = 0;

            if (vag.QuantidadeHorasEspeciais > 0)
                conta = vag.QuantidadeHorasEspeciais * 60;

            int tempo = (tmp.Hours) * 60 + tmp.Minutes;

			if (tempo >= 60)
			{
				for (int i = tempo; i >= 0; i -= 60)
				{
					if (tempo > conta)
					{
						total += vag.valorHorasEspeciais;
					}
					else
					{
						total += vag.valor;
					}
				}
			}
			else
			{
				total += vag.valor;
			}
            return total;
        }

        // GET: Movimentacaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            db.Movimentacao.Remove(movimentacao);
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
