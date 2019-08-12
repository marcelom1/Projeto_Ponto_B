using PagedList;
using PontoB.Business.Utils;
using PontoB.Controllers.RegrasDeNegocios.RAusencia;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.ViewModels.VAusencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PontoB.Controllers
{
    [Authorize(Roles = "Master")]
    public class AusenciaController : Controller
    {
        private AusenciaDAO dbAusencia = new AusenciaDAO();
        private MotivoAusenciaDAO dbMotivoAusencia = new MotivoAusenciaDAO();
        private AusenciaColaboradoresDAO dbAusenciaColaborador = new AusenciaColaboradoresDAO();
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        private RegrasAusencia RegrasAusencia = new RegrasAusencia();

        // GET: Ausencia
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
            return View(dbAusenciaColaborador.Lista().ToPagedList(pagina, 10));
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetColaboradores(string searchTerm)
        {
            var empresa = dbColaborador.Filtro("Nome Completo", searchTerm);
            

            var modifica = empresa.Select(x => new
            {
                id = x.Id,
                text = x.NomeCompleto
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetMotivoAusencia(string searchTerm)
        {
            var empresa = dbMotivoAusencia.Filtro("Descricao", searchTerm);

            var modifica = empresa.Select(x => new
            {
                id = x.Id,
                text = x.Descricao
            });

            return Json(modifica, JsonRequestBehavior.AllowGet);


        }


        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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
       

        public ActionResult AdicionaAusenciaColaborador(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim, int empresa = 0, bool TodosColaboradores = false, bool TodasEmpresas = false)
        {
            ausenciaColaboradores = RegrasAusencia.MontarAusenciaColaboradores(ausenciaColaboradores, HoraInicio, HoraFim);

            var model = new AusenciaViewModels {
                AusenciaColaboradores = ausenciaColaboradores,
                TodosColaboradores = TodosColaboradores,
                TodasEmpresas = TodasEmpresas,
                Empresa = empresa!=0 ? dbEmpresa.BuscarPorId(empresa) : new Empresa()
            };

            if (ModelState.IsValid)
            {
                try
                {
                    model = RegrasAusencia.ValidacaoRegraNegocio(ausenciaColaboradores, HoraInicio, HoraFim, TodosColaboradores, TodasEmpresas, model);

                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("ausenciaColaboradores.Erro", e.Message);
                    
                }
            }
            model.AusenciaColaboradores.Ausencia = ausenciaColaboradores.Ausencia;
            model.AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(ausenciaColaboradores.Ausencia.Id);
            return View("Form", model);

        }



        public string AdicionaAusenciaColaboradorPelaManutencao(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim)
        {
            ausenciaColaboradores = RegrasAusencia.MontarAusenciaColaboradores(ausenciaColaboradores, HoraInicio, HoraFim);

            var model = new AusenciaViewModels
            {
                AusenciaColaboradores = ausenciaColaboradores,
            };

            if (ModelState.IsValid)
            {
                try
                {
                    model = RegrasAusencia.ValidacaoRegraNegocio(ausenciaColaboradores, HoraInicio, HoraFim, false, false, model);

                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("ausenciaColaboradores.Erro", e.Message);
                    return e.Message;
                    

                }

            }
            return "";
        }



        public ActionResult DetalhesAusencia(int id = 0)
        {
            var model = new AusenciaViewModels
            {
                AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(0)
            };

            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados da ausência
            if (id != 0)
            {
                model.AusenciaColaboradores.Ausencia = dbAusencia.BuscarPorId(id) ?? new Ausencia();
                model.AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(id);

                //return View(model);
            }

            return PartialView(model);
        }


        public ActionResult Form(int id = 0)
        {
            var model = new AusenciaViewModels
            {
                AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(0)
            };

            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados da ausência
            if (id != 0)
            {
                model.AusenciaColaboradores.Ausencia = dbAusencia.BuscarPorId(id)?? new Ausencia();
                model.AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(id);

                //return View(model);
            }

            //Senão a ausência é nova 
            return View(model);
        }

        public ActionResult TabelaAusenciaPorDia(DateTime? data, int colaboradorId)
        {

            var valores = new FiltroPeriodoValores
            {
                Inicio = data,
                Fim = data,
                ColaboradorId = colaboradorId

            };
            var texto = valores.ToString();

            var resultado = dbAusencia.Filtro("ColaboradorEntreData", texto);

            var model = new AusenciaViewModels
            {
                AusenciaColaboradoresLista = resultado
            };

            return PartialView(model);
        }



        [HttpPost]
        public ActionResult Adiciona(Ausencia ausencia)
        {

            //Busca se já existe a ausência
            var pesquisa = dbAusencia.BuscarPorId(ausencia.Id);
            var model = new AusenciaViewModels();
            model.AusenciaColaboradores.Ausencia = pesquisa;
            model.AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(ausencia.Id);

            if (ModelState.IsValid)
            {
                //Caso a ausencia já existir ela será atualizada
                if (pesquisa != null)
                {
                    dbAusencia.Atualiza(ausencia);
                    return RedirectToAction("Index", "Ausencia");
                }
                else //Senão é uma ausencia nova sendo adicionada
                {
                    dbAusencia.Adiciona(ausencia);
                    return RedirectToAction("Form", new { id = ausencia.Id });
                }
            }
            return View("Form", model);
        }

        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {

            //Faz a Busca do filtro
            IPagedList<AusenciaColaboradores> filtro = dbAusencia.Filtro(coluna, texto).ToPagedList(pagina, 10);

            //Preenche as ViewBag com os resultado do filtro
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;

            return View("Index", filtro);
        }

        public ActionResult ExcluirAusenciaColaborador(int AusenciaColaboradorId, string ViewOrigem, DateTime? data, int? idColaborador)
        {
            if (ViewOrigem.Equals("TabelaAusenciaPorDia")) {
                RegrasAusencia.ExcluirAusencia(AusenciaColaboradorId);
                return RedirectToAction(ViewOrigem, new { data, colaboradorId = idColaborador });
            }
            return RedirectToAction(ViewOrigem, new { id = RegrasAusencia.ExcluirAusencia(AusenciaColaboradorId) });
        }

       
        [HttpPost]
        public ActionResult Excluir(Ausencia ausencia)
        {
            //Antes de excluir é feito a verificação se a escala existe 
            var pesquisa = dbAusencia.BuscarPorId(ausencia.Id);

            //Caso encontre algo, exluir o registro
            if (pesquisa != null)
                dbAusencia.ExcluirAusencia(pesquisa);

            return RedirectToAction("Index", "Ausencia");
        }

        public ActionResult Excluir(int id)
        {
            //Antes de excluir é feito a verificação se a escala existe 
            var pesquisa = dbAusencia.BuscarPorId(id);

            //Caso encontre algo, exluir o registro
            if (pesquisa != null)
                dbAusencia.ExcluirAusencia(pesquisa);

            return RedirectToAction("Index", "Ausencia");
        }
    }
}