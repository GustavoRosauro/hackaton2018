using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using hackaton2018.Models;

namespace hackaton2018.Controllers
{
    public class CadastroController : Controller
    {
        Cadastro c = new Cadastro();
        private hackaton2018Context db = new hackaton2018Context();

        // GET: Cadastro

        public ActionResult Index()
        {
            return View(db.Cadastroes.ToList());
        }

        // GET: Cadastro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastro cadastro = db.Cadastroes.Find(id);
            if (cadastro == null)
            {
                return HttpNotFound();
            }
            return View(cadastro);
        }

        // GET: Cadastro/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Compra()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Compra(string compra, string localizacao)
        {
            Cadastro c = Session["Cadastro"] as Cadastro;
            EnvioEmail envio = new EnvioEmail();
            envio.Email(c.Celular, "Confirmação de Compra site Teste", "Compra no Valor de " + compra +" na cidade de "+localizacao+" Por favor confirme a compra");
            return View();
        }

        // POST: Cadastro/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cpf,Celular,Endereco")] Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                db.Cadastroes.Add(cadastro);
                db.SaveChanges();
                return RedirectToAction("Recepcao");
            }

            return View(cadastro);
        }

        // GET: Cadastro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastro cadastro = db.Cadastroes.Find(id);
            if (cadastro == null)
            {
                return HttpNotFound();
            }
            return View(cadastro);
        }
        public ActionResult Recepcao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Recepcao(string cpf)
        {
            var lista = db.Cadastroes.ToList().Find(x => x.Cpf == cpf);
            if (lista == null)
            {
                Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                Response.StatusDescription = "Não há esse CPF em nosso registro!!";
            }
            else
            {
                Session["Cadastro"] = lista;
                return RedirectToAction("Compra");
            }
            return View();
        }

        // POST: Cadastro/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cpf,Celular,Endereco")] Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cadastro);
        }

        // GET: Cadastro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cadastro cadastro = db.Cadastroes.Find(id);
            if (cadastro == null)
            {
                return HttpNotFound();
            }
            return View(cadastro);
        }

        // POST: Cadastro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cadastro cadastro = db.Cadastroes.Find(id);
            db.Cadastroes.Remove(cadastro);
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
