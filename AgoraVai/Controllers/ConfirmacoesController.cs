using AgoraVai.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgoraVai.Controllers
{
    public class ConfirmacoesController : Controller
    {
        private Contexto db = new Contexto();
        private static int idgerente = 0;
        // GET: Confirmacoes
        public ActionResult ConfirmarEmail()
        {
            if (Session["GenID"] != null)
            {
                int sl = 0;
                sl = Convert.ToInt32(Session["GenID"]);
                idgerente = sl;
                enviogen();
            }

            if (Session["FunID"] != null)
            {
                int sl = 0;
                sl = Convert.ToInt32(Session["FunID"]);
                idgerente = sl;
                enviofun();
            }


            return View();
        }

        public void enviogen()
        {
            Random random = new Random();
            string alfabeto = "abcdefghijklmnopqrstuvwxyz";
            string codigosep = "";

            for(int i = 0; i <= 5; i++)
            {
                if (i <= 2)
                {
                    int r = random.Next(22);
                    codigosep += alfabeto[r];
                }
                else
                {
                    int r = random.Next(9);
                    codigosep += Convert.ToString(r);
                }
            }
            // random.Next(min, max);
            string codfinal = codigosep;
            Gerente gen = db.Gerente.Find(idgerente);
            if (gen.CodigoDeAtivacao == null)
            {
                gen.CodigoDeAtivacao = new CodigoDeAtivacao();
                gen.CodigoDeAtivacao.Codigo = codfinal;
                db.CodigoDeAtivacao.Add(gen.CodigoDeAtivacao);
                db.Entry(gen).State = EntityState.Modified;
                db.SaveChanges();
                gen.CodigoDeAtivacaoId = db.CodigoDeAtivacao.Where(x => x.Codigo == gen.CodigoDeAtivacao.Codigo).ToList().LastOrDefault().Id;
                db.SaveChanges();
            }
            else
            {
                gen.CodigoDeAtivacao.Codigo = codfinal;
                db.Entry(gen).State = EntityState.Modified;
                db.SaveChanges();
            }


            string email = gen.Email;
            string assunto = "confirmação";
            string mensagem = "código de confirmação de email:  " + gen.CodigoDeAtivacao.Codigo;

            if (mensagem != "" && email != "" && assunto != "")
            {
                TempData["MSG"] = Funcoes.Email.EnviarEmail(email, assunto, mensagem);
            }
            else
            {
                TempData["MSG"] = "warning|Preencha todos os campos";
            }
        }

        public void enviofun()
        {
            Funcionario gen = db.Funcionario.Find(idgerente);
            if (gen.CodigoDeAtivacao == null)
            {
                gen.CodigoDeAtivacao = new CodigoDeAtivacao();
                gen.CodigoDeAtivacao.Codigo = "1";
                db.CodigoDeAtivacao.Add(gen.CodigoDeAtivacao);
                db.Entry(gen).State = EntityState.Modified;
                db.SaveChanges();
                gen.CodigoDeAtivacaoId = db.CodigoDeAtivacao.Where(x => x.Codigo == gen.CodigoDeAtivacao.Codigo).ToList().LastOrDefault().Id;
                db.SaveChanges();
            }
            else
            {
                gen.CodigoDeAtivacao.Codigo = "2";
                db.Entry(gen).State = EntityState.Modified;
                db.SaveChanges();
            }


            string email = gen.Email;
            string assunto = "confirmação";
            string mensagem = "código de confirmação de email:  " + gen.CodigoDeAtivacao.Codigo;

            if (mensagem != "" && email != "" && assunto != "")
            {
                TempData["MSG"] = Funcoes.Email.EnviarEmail(email, assunto, mensagem);
            }
            else
            {
                TempData["MSG"] = "warning|Preencha todos os campos";
            }
        }

        [HttpPost]
        public ActionResult ConfirmarEmail(CodigoDeAtivacao cod)
        {
            if (Session["GenID"] != null)
            {
                Gerente gen = db.Gerente.Find(idgerente);

                if (gen.CodigoDeAtivacao.Codigo == cod.Codigo)
                {
                    gen.ativo = true;
                    db.Entry(gen).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Gerentes");
                }
                else
                {
                    TempData["MSG"] = "código errado";
                }
            }
            if (Session["FunID"] != null)
            {
                Funcionario gen = db.Funcionario.Find(idgerente);

                if (gen.CodigoDeAtivacao.Codigo == cod.Codigo)
                {
                    gen.ativo = true;
                    db.Entry(gen).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Movimentacaos");
                }
                else
                {
                    TempData["MSG"] = "código errado";
                }
            }
            return View();
        }
    }
}