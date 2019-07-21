using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;

namespace PontoB.Controllers
{
    [Authorize(Roles = "Master")]
    public class EmpresaController : Controller
    {
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        private EnderecoDAO dbEndereco = new EnderecoDAO();
        private EstadosUFDAO dbEstado = new EstadosUFDAO();
        

        public ActionResult Index(int pagina = 1,string coluna="" ,string filtro="")
        {
            //Aplica paginação no Filtro
            if (filtro != "" && coluna !="")
            {
                return RedirectToAction("Filtro", new { coluna, texto=filtro, pagina });
               
            }
            //Caso não tenha Filtro, set VieBag vazia 
            ViewBag.FiltroColuna = "";
            ViewBag.Filtro = "";
           
            //retorna todas as empresas 
            return View(dbEmpresa.Lista(pagina));
        }



        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {
            //Faz a Busca do filtro
            IPagedList<Empresa> filtro = dbEmpresa.Filtro(coluna, texto).ToPagedList(pagina,10);

            //Preenche as ViewBag com os resultado do filtro
            ViewBag.Empresas = filtro;
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;

            return View("Index", filtro);
        }


        public ActionResult Form(int id=0)
        {
            //Lista todas as UF
            IList<EstadosUF> estados = dbEstado.Lista();
            ViewBag.EstadosUf = estados;

            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados da empresa
            if (id != 0)
            {
                ViewBag.Empresa = dbEmpresa.BuscarPorId(id);
                return View();
            }

            //Senão a Empresa é nova 
            ViewBag.Empresa = new Empresa();
            ViewBag.Empresa.EnderecoEmpresa = new Endereco();
            ViewBag.Empresa.EnderecoEmpresa.Estado = new EstadosUF();

            return View();
        
        }

        [HttpPost]
        public ActionResult Adiciona(Empresa empresa)
        {
            //Lista todas as UF
            var pesquisa = dbEmpresa.BuscarPorId(empresa.Id);
            IList<EstadosUF> estados = dbEstado.Lista();

            //Set os campos ViewBag com o que foi mandado pela View
            ViewBag.EstadosUf = estados;
            ViewBag.Empresa = empresa;
            ViewBag.Empresa.EnderecoEmpresa = empresa.EnderecoEmpresa;
            ViewBag.Empresa.EnderecoEmpresa.Estado = empresa.EnderecoEmpresa.Estado;


            if (ModelState.IsValid)//Validação de Todos os Campos
            {
               
                var pesquisarEstado = dbEstado.BuscarPorId(empresa.EnderecoEmpresa.Estado.Id);
                empresa.EnderecoEmpresa.Estado = pesquisarEstado;

                

                if (pesquisa != null)
                {
                    dbEmpresa.Atualiza(empresa);

                }
                else
                {
                    try
                    {
                        dbEmpresa.Adiciona(empresa);
                    }
                    catch (Exception)
                    {
                        //Caso o CNPJ já esteja Cadastrado retorna para View com o erro
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
            
            var pesquisa = dbEmpresa.BuscarPorId(empresa.Id);

            if (pesquisa != null)
            {

                dbEmpresa.ExcluirEmpresa(pesquisa);
                dbEndereco.ExcluirEndereco(pesquisa.EnderecoEmpresa);
            }
           
            return RedirectToAction("Index", "Empresa");
        }

        public ActionResult Excluir(int id)
        {
           
            var pesquisa = dbEmpresa.BuscarPorId(id);
            if (pesquisa != null)
            {
                dbEmpresa.ExcluirEmpresa(pesquisa);
                dbEndereco.ExcluirEndereco(pesquisa.EnderecoEmpresa);

            }
            return RedirectToAction("Index", "Empresa");
        }
    }
}