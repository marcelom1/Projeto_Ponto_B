using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {

            EmpresaDAO dao = new EmpresaDAO();
            IList<Empresa> empresas = dao.Lista();
            ViewBag.Empresas = empresas;
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }

        public ActionResult Adiciona(Empresa empresa)
        {
            
                EmpresaDAO dao = new EmpresaDAO();
                dao.Adiciona(empresa);

                return RedirectToAction("Index", "Empresa");
           
        }
    }
}