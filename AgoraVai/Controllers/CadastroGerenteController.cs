using AgoraVai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgoraVai.Controllers
{
    public class CadastroGerenteController : Controller
    {
		private Contexto db = new Contexto();
		// GET: CadastroGerente
		public ActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Create(CadastroGerente usu)
		{
			Gerente gen = new Gerente();
			gen.Pessoa = new Pessoa();
			gen.Pessoa.Nome = usu.Nome;
			gen.Pessoa.cpf = usu.cpf;

			db.Pessoa.Add(gen.Pessoa);
			db.SaveChanges();
			gen.PessoaId = db.Pessoa.Where(x => x.cpf == usu.cpf).ToList().LastOrDefault().Id;
			gen.Email = usu.Email;
			gen.Senha = usu.Senha;
			db.Gerente.Add(gen);
			db.SaveChanges();
			return RedirectToAction("Create", "Estacionamentoes");
		}
	}
}