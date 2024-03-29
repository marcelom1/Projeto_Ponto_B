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
    [Authorize]
    public class ColaboradorController : Controller
    {
        
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        private EscalaDAO dbEscala = new EscalaDAO();
        private EnderecoDAO dbEndereco = new EnderecoDAO();
        private EstadosUFDAO dbEstado = new EstadosUFDAO();

        [Authorize(Roles = "Master")]
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

        [Authorize(Roles = "Master")]
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

        [Authorize(Roles = "Master")]
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

        [Authorize(Roles = "Master")]
        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {
            
            //Faz a Busca do filtro
            IPagedList<Colaborador> filtro = dbColaborador.Filtro(coluna, texto).ToPagedList(pagina,10);

            //Preenche as ViewBag com os resultado do filtro
          
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;

            return View("Index", filtro);
        }

        [Authorize(Roles = "Master")]
        public ActionResult Form(int id = 0)
        {
            //Lista todas as UF
            IList<EstadosUF> estados = dbEstado.Lista();
            ViewBag.EstadosUf = estados;

            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados do colaborador
            if (id != 0)
            {
                //ViewBag.Colaborador = ;
                var pesquisa = dbColaborador.BuscarPorId(id);
                
                

                return View(dbColaborador.BuscarPorId(id));
            }

            //Senão o colaborador é novo 
           
            var colaborador = new Colaborador
            {
                Empresa = new Empresa(),
                Escala = new Escala(),

                EnderecoColaborador = new Endereco
                {
                    Estado = new EstadosUF()
                }
            };
            return View(colaborador);
        }
        [Authorize(Roles = "Master")]
        [HttpPost]
        public ActionResult Adiciona(Colaborador colaborador)
        {
           
            var pesquisa = dbColaborador.BuscarPorId(colaborador.Id);
            

            //Lista todas as UF
            IList <EstadosUF> estados = dbEstado.Lista();
            ViewBag.EstadosUf = estados;
            //Set os campos ViewBag com o que foi mandado pela View
            var model = colaborador;
            model.EnderecoColaborador = colaborador.EnderecoColaborador;
            model.EnderecoColaborador.Estado = colaborador.EnderecoColaborador.Estado;
            model.Empresa = dbEmpresa.BuscarPorId(colaborador.EmpresaId);
            model.Escala = dbEscala.BuscarPorId(colaborador.EscalaId);
            
           if (colaborador.EmpresaId==0)
                ModelState.AddModelError("colaborador.EmpresaId", "Empresa é um campo obrigatório!");
           if(colaborador.EscalaId==0)
                ModelState.AddModelError("colaborador.EscalaId", "Escala é um campo obrigatório!");


            if (ModelState.IsValid)//Validação de Todos os Campos
            {

                if (pesquisa != null)
                {
                    dbColaborador.Atualiza(colaborador);

                }
                else
                {
                    IList<Colaborador> registros = dbColaborador.Filtro("CPF", colaborador.CPF);
                    if (registros.Count>0)
                    {
                        foreach (var registro in registros )
                        {
                            if ((!registro.DataDemissao.HasValue) )
                            {
                                model.Id = 0;
                                model.EnderecoColaborador.Id = 0;
                                
                                ModelState.AddModelError("colaborador.Cpf", "CPF já cadastrado e sem registro de demissão!");
                                return View("Form",model);
                            }
                            else
                            {
                                if (registro.DataDemissao > model.DataAdmissao)
                                {
                                    model.Id = 0;
                                    model.EnderecoColaborador.Id = 0;
                                    ModelState.AddModelError("colaborador.Cpf", "CPF já cadastrado e com registro de demissão maior que o novo registro de admissão!");
                                    return View("Form", model);
                                }

                            }
                            
                        }
                    }

                    dbColaborador.Adiciona(colaborador);
                    

                }
                return RedirectToAction("Index", "Colaborador");
            }
            else
            {
                model.Empresa = new Empresa();
                model.Escala = new Escala();
                return View("Form",model);
            }
        }
        [Authorize(Roles = "Master")]
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
        [Authorize(Roles = "Master")]
        public ActionResult Excluir(int id)
        {

            var pesquisa = dbColaborador.BuscarPorId(id);
            if (pesquisa != null)
            {
                dbColaborador.ExcluirColaborador(pesquisa);

            }
            return RedirectToAction("Index", "Colaborador");
        }
        

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AlterarSenhaColaborador(string senhaAtual, string novaSenha, string confirmacaoSenha)
        {
            var login = User.Identity.Name;
            Colaborador autenticado = dbColaborador.ConfirmacaoAutenticacao(login, senhaAtual);
            if (autenticado != null) {
                if (novaSenha.Equals(confirmacaoSenha))
                {
                    autenticado.Senha = novaSenha;
                    dbColaborador.Atualiza(autenticado);
                    return Json("Senha alterada com sucesso!", JsonRequestBehavior.AllowGet);
                }
                return Json("Erro nova senha e a confirmação não são as mesmas", JsonRequestBehavior.AllowGet);
            }
            return Json("A senha atual não confere com a cadastrada", JsonRequestBehavior.AllowGet);

        }

    }
}
