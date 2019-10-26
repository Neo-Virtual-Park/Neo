using AgoraVai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgoraVai.Controllers
{
    public class CadastrarFuncController : Controller
    {
		private Contexto db = new Contexto();
		// GET: CadastrarFunc
		public ActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Create(CadastroFuncionario usu)
		{
            int sl = 0;
            sl = Convert.ToInt32(Session["FunID"]);

            Funcionario func = new Funcionario();
			func.Pessoa = new Pessoa();
			func.Pessoa.Nome = usu.Nome;
			func.Pessoa.cpf = usu.cpf;

			db.Pessoa.Add(func.Pessoa);
			db.SaveChanges();
			func.PessoaId = db.Pessoa.Where(x => x.cpf == usu.cpf).ToList().LastOrDefault().Id;
			func.Email = usu.Email;
			func.Senha = usu.Senha;
            func.EstacionamentoId = sl;
            func.Estacionamento = db.Estacionamento.Where(x => x.Id == func.EstacionamentoId ).ToList().LastOrDefault();
            db.Funcionario.Add(func);
			db.SaveChanges();
            return RedirectToAction("Index","Gerentes");
        }
	}
}