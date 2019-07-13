﻿using PagedList;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PontoB.Controllers
{
    public class ColaboradorController : Controller
    {
        
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        private EscalaDAO dbEscala = new EscalaDAO();
        private EnderecoDAO dbEndereco = new EnderecoDAO();
        private EstadosUFDAO dbEstado = new EstadosUFDAO();



        public ActionResult Index(int pagina = 1, string coluna = "", string filtro = "")
        {
            //Aplica paginação no Filtro
            if (filtro != "" && coluna != "")
            {
                return RedirectToAction("Filtro", new { coluna, texto = filtro, pagina });

            }
            //Caso não tenha Filtro, set VieBag vazia 
            ViewBag.FiltroColuna = "";
            ViewBag.Filtro = "";

            //retorna todas os colaboradores 
            return View(dbColaborador.Lista().ToPagedList(pagina,10));
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat =ResponseFormat.Json)]
        public JsonResult GetEmpresas(string searchTerm)
        {
            var empresa = dbEmpresa.Filtro("Razão Social", searchTerm);

            var modifica = empresa.Select(x => new
            {
                id = x.Id,
                text = x.RazaoSocial
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);

            
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult getEscala(string searchTerm)
        {
            var empresa = dbEscala.Filtro("Descrição", searchTerm);

            var modifica = empresa.Select(x => new
            {
                id = x.Id,
                text = x.Descricao
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }


        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {
            
            //Faz a Busca do filtro
            IPagedList<Colaborador> filtro = dbColaborador.Filtro(coluna, texto).ToPagedList(pagina,10);

            //Preenche as ViewBag com os resultado do filtro
            ViewBag.Colaborador = filtro;
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;

            return View("Index", filtro);
        }


        public ActionResult Form(int id = 0)
        {
            //Lista todas as UF
            IList<EstadosUF> estados = dbEstado.Lista();
            ViewBag.EstadosUf = estados;

            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados do colaborador
            if (id != 0)
            {
                ViewBag.Colaborador = dbColaborador.BuscarPorId(id);
                return View();
            }

            //Senão o colaborador é novo 
            ViewBag.Colaborador = new Colaborador();
            ViewBag.Colaborador.EnderecoColaborador = new Endereco();
            ViewBag.Colaborador.EnderecoColaborador.Estado = new EstadosUF();

            return View();

        }

        [HttpPost]
        public ActionResult Adiciona(Colaborador colaborador)
        {
            //Lista todas as UF
            var pesquisa = dbColaborador.BuscarPorId(colaborador.Id);
            IList<EstadosUF> estados = dbEstado.Lista();

            //Set os campos ViewBag com o que foi mandado pela View
            ViewBag.EstadosUf = estados;
            ViewBag.Colaborador = colaborador;
            ViewBag.Colaborador.EnderecoColaborador = colaborador.EnderecoColaborador;
            ViewBag.Colaborador.EnderecoColaborador.Estado = colaborador.EnderecoColaborador.Estado;


            if (ModelState.IsValid)//Validação de Todos os Campos
            {

                var pesquisarEstado = dbEstado.BuscarPorId(colaborador.EnderecoColaborador.Estado.Id);
                colaborador.EnderecoColaborador.Estado = pesquisarEstado;



                if (pesquisa != null)
                {
                    dbColaborador.Atualiza(colaborador);

                }
                else
                {
                    try
                    {
                        dbColaborador.Adiciona(colaborador);
                    }
                    catch (Exception)
                    {
                        //Caso o CNPJ já esteja Cadastrado retorna para View com o erro
                        ViewBag.Colaborador.Id = 0;
                        ViewBag.Colaborador.EnderecoColaborador.Id = 0;
                        ModelState.AddModelError("colaborador.Cpf", "CPF já consta cadastrado no banco de dados");

                        return View("Form");
                    }

                }
                return RedirectToAction("Index", "Colaborador");
            }
            else
            {

                return View("Form");
            }
        }

        [HttpPost]
        public ActionResult Excluir(Colaborador colaborador)
        {

            var pesquisa = dbColaborador.BuscarPorId(colaborador.Id);

            if (pesquisa != null)
            {

                dbColaborador.ExcluirColaborador(pesquisa);
                dbEndereco.ExcluirEndereco(pesquisa.EnderecoColaborador);
            }

            return RedirectToAction("Index", "Colaborador");
        }

        public ActionResult Excluir(int id)
        {

            var pesquisa = dbColaborador.BuscarPorId(id);
            if (pesquisa != null)
            {
                dbColaborador.ExcluirColaborador(pesquisa);

            }
            return RedirectToAction("Index", "Colaborador");
        }
    }
}
