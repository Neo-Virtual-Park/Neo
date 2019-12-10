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
            gen.Senha = SHA512(usu.Senha);
            gen.ativo = false;
			db.Gerente.Add(gen);
			db.SaveChanges();
			return RedirectToAction("Create", "Estacionamentoes", new { id = gen.Id });
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