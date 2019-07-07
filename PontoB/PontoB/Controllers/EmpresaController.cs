using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
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


        public ActionResult Filtro(string coluna, string texto)
        {
            EmpresaDAO dao = new EmpresaDAO();
            IList<Empresa> filtro = dao.Filtro(coluna, texto);
            ViewBag.Empresas = filtro;
            return View("Index");
        }
        public ActionResult Form(int id=0)
        {
            EstadosUFDAO EstadoDao = new EstadosUFDAO();
            IList<EstadosUF> estados = EstadoDao.Lista();
            ViewBag.EstadosUf = estados;

            if (id != 0)
            {
                
                EmpresaDAO dao = new EmpresaDAO();
                Empresa empresa = dao.BuscarPorId(id);
                ViewBag.Empresa = empresa;

                
                return View();
            }

            
            ViewBag.Empresa = new Empresa();
            ViewBag.Empresa.EnderecoEmpresa = new Endereco();
            ViewBag.Empresa.EnderecoEmpresa.Estado = new EstadosUF();





            return View();
        
        }

        [HttpPost]
        public ActionResult Adiciona(Empresa empresa)
        {
            ModelState.Clear();
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(empresa.Id);
            EstadosUFDAO EstadoDao = new EstadosUFDAO();
            IList<EstadosUF> estados = EstadoDao.Lista();
            ViewBag.EstadosUf = estados;
            ViewBag.Empresa = empresa;
            ViewBag.Empresa.EnderecoEmpresa = empresa.EnderecoEmpresa;
            ViewBag.Empresa.EnderecoEmpresa.Estado = empresa.EnderecoEmpresa.Estado;
            if (ModelState.IsValid)
            {
                EstadosUFDAO daoEstado = new EstadosUFDAO();
                var pesquisarEstado = daoEstado.BuscarPorId(empresa.EnderecoEmpresa.Estado.Id);
                empresa.EnderecoEmpresa.Estado = pesquisarEstado;

                

                if (pesquisa != null)
                {
                    dao.Atualiza(empresa);

                }
                else
                {
                    try
                    {
                        dao.Adiciona(empresa);
                    }
                    catch (Exception)
                    {
                        ViewBag.Empresa.Id = 0;
                        ViewBag.Empresa.EnderecoEmpresa.Id = 0;
                        ModelState.AddModelError("empresa.Cnpj","CNPJ já consta cadastrado no Banco de Dados");
                  
                        return View("Form");
                    }

                }
                return RedirectToAction("Index", "Empresa");
            }
            else
            {

                return View("Form");
            }
        }

       

        [HttpPost]
        public ActionResult Excluir(Empresa empresa)
        {
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(empresa.Id);

            if (pesquisa != null)
            {
                
                dao.ExcluirEmpresa(pesquisa);
                EnderecoDAO daoe = new EnderecoDAO();
                daoe.ExcluirEndereco(pesquisa.EnderecoEmpresa);

            }
           
            return RedirectToAction("Index", "Empresa");
        }

       public ActionResult Excluir(int id)
        {
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(id);

            if (pesquisa != null)
            {
                dao.ExcluirEmpresa(pesquisa);

            }

            return RedirectToAction("Index", "Empresa");
        }


    }
}