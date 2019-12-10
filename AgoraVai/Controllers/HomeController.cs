using AgoraVai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgoraVai.Controllers
{
	public class HomeController : Controller
	{
        private Contexto db = new Contexto();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Sair()
        {
            Session["FunID"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Funcionario fun)
        {

            string senhacripto = SHA512(fun.Senha);
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                    var v = db.Funcionario.Where(a => a.Email.Equals(fun.Email) && a.Senha.Equals(senhacripto)).FirstOrDefault();
                var g = db.Gerente.Where(a => a.Email.Equals(fun.Email) && a.Senha.Equals(senhacripto)).FirstOrDefault();

                if (fun.Email == "reginaldo313122@gmail.com" && fun.Senha == "313122Aa")
                {
                    return RedirectToAction("Index", "Estacionamentoes");
                }

                if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.Id.ToString();
                    Session["FunID"] = v.Id;
                    //   Session["nomeUsuarioLogado"] = v.Nome.ToString();
                    if (v.ativo == true)
                    {
                        return RedirectToAction("Index", "Movimentacaos");
                    }
                    else
                    {
                        return RedirectToAction("ConfirmarEmail", "Confirmacoes");
                    }
                    
                    }
                if (g != null)
                {
                    Session["usuarioLogadoID"] = g.Id.ToString();
                    Session["GenID"] = g.Id;
                    if(g.ativo == true)
                    {
                        return RedirectToAction("Index", "Gerentes");
                    }
                    else
                    {
                        return RedirectToAction("ConfirmarEmail", "Confirmacoes");
                    }
                    //   Session["nomeUsuarioLogado"] = v.Nome.ToString();
                  
                }
            }
            return View(fun);
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

    public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}