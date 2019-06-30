using Microsoft.EntityFrameworkCore;
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

        public ActionResult Form(int id =0)
        {
            if (id != 0)
            {
                
                EmpresaDAO dao = new EmpresaDAO();
                Empresa empresa = dao.BuscarPorId(id);
                ViewBag.Empresa = empresa;

                //EnderecoDAO daoEndereco = new EnderecoDAO();
                /*using (var contexto = new PontoContex())
                {
                    var enderecoempresa = contexto.Empresa.Include(e => e.EnderecoEmpresa).Where(e=> e.Id==id).FirstOrDefault();

                    ViewBag.Endereco = enderecoempresa;
                }*/

                
                

                return View();
            }

           
            ViewBag.Empresa = new Empresa();
            ViewBag.Empresa.EnderecoEmpresa = new Endereco();
            return View();

        }

        [HttpPost]
        public ActionResult Adiciona(Empresa empresa)
        {
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(empresa.Id);
            /*pesquisa.NomeFantasia = empresa.NomeFantasia;
            pesquisa.RazaoSocial = empresa.RazaoSocial;
            pesquisa.Telefone = empresa.Telefone;
            pesquisa.Email = empresa.Email;
            pesquisa.Cnpj = empresa.Cnpj;
            pesquisa.EnderecoEmpresa = empresa.EnderecoEmpresa;
            dao.Atualiza(pesquisa);*/
            
            if (dao.BuscarPorId(empresa.Id) != null)
            {
                dao.Atualiza(empresa);

            }
            else
            {
                dao.Adiciona(empresa);
            }
            return RedirectToAction("Index", "Empresa");
           
        }

        public ActionResult Alterar(Empresa empresa)
        {
            

            return RedirectToAction("Index", "Empresa");
        }
    }
}