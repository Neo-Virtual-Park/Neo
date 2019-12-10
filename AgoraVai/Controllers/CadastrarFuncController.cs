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
            sl = Convert.ToInt32(Session["GenID"]);

            Funcionario func = new Funcionario();
			func.Pessoa = new Pessoa();
			func.Pessoa.Nome = usu.Nome;
			func.Pessoa.cpf = usu.cpf;

			db.Pessoa.Add(func.Pessoa);
			db.SaveChanges();
			func.PessoaId = db.Pessoa.Where(x => x.cpf == usu.cpf).ToList().LastOrDefault().Id;
			func.Email = usu.Email;
			func.Senha = SHA512(usu.Senha);
            func.EstacionamentoId = sl;
            func.Estacionamento = db.Estacionamento.Where(x => x.Id == func.EstacionamentoId ).ToList().LastOrDefault();
            func.ativo = false;
            db.Funcionario.Add(func);
			db.SaveChanges();
            return RedirectToAction("Index","Gerentes");
        }
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}