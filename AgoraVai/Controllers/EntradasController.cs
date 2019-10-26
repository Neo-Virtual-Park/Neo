using AgoraVai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgoraVai.Controllers
{
    public class EntradasController : Controller
    {
        private Contexto db = new Contexto();
        // GET: Entradas
        public ActionResult Create()
        {
            ViewBag.HoraEnt = DateTime.Now;
            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Email");
            ViewBag.VagaId = new SelectList(db.Vaga.Where(x => x.Quantidade > 0), "Id", "Tipo");
            ViewBag.TipoId = new SelectList(db.TipoPreco, "Id", "Tipo");
            //ViewBag.Tipo = new SelectList(Tip, "Id", "Tipo");
            return View();
        }

        [HttpPost]
          [ValidateAntiForgeryToken]
        public ActionResult Create(Entrada usu)
        {
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);
            Movimentacao mov = new Movimentacao();
            mov.Cpf = usu.Cpf;
            mov.Funcionario = usu.Funcionario;
            mov.hora_entrada = DateTime.Now;
            mov.Placa = usu.Placa;
            mov.Telefone = usu.Telefone;
           // mov.Funcionario = usu.Funcionario;
            mov.FuncionarioId = sl ;
            mov.Vaga = usu.Vaga;
            mov.VagaId = usu.VagaId;
            mov.Valor_pagar = 0;
            mov.Tipo = usu.TipoId;
            db.Movimentacao.Add(mov);
            db.SaveChanges();
            return RedirectToAction("Index", "movimentacaos");
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cpf,Telefone,Placa,hora_entrada,FuncionarioId,VagaId")] Movimentacao movimentacao)
        {

            movimentacao.hora_entrada = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Movimentacao.Add(movimentacao);
                db.SaveChanges();
                return RedirectToAction("Index","movimentacaos");
            }

            ViewBag.FuncionarioId = new SelectList(db.Funcionario, "Id", "Nome", movimentacao.FuncionarioId);
            ViewBag.VagaId = new SelectList(db.Vaga, "Id", "Tipo", movimentacao.VagaId);
            return View(movimentacao);
        }
        */
    }
}