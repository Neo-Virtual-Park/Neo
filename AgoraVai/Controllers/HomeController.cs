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
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                    var v = db.Funcionario.Where(a => a.Email.Equals(fun.Email) && a.Senha.Equals(fun.Senha)).FirstOrDefault();
                var g = db.Gerente.Where(a => a.Email.Equals(fun.Email) && a.Senha.Equals(fun.Senha)).FirstOrDefault();

                if (fun.Email == "reginaldo313122@gmail.com" && fun.Senha == "313122Aa")
                {
                    return RedirectToAction("Index", "Estacionamentoes");
                }

                if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.Id.ToString();
                    Session["FunID"] = v.Id;
                    //   Session["nomeUsuarioLogado"] = v.Nome.ToString();
                    return RedirectToAction("Index","Movimentacaos");
                    }
                if (g != null)
                {
                    Session["usuarioLogadoID"] = g.Id.ToString();
                    Session["FunID"] = g.Id;
                    //   Session["nomeUsuarioLogado"] = v.Nome.ToString();
                    return RedirectToAction("Index", "Gerentes");
                }
            }
            return View(fun);
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