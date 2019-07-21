using PagedList;
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
        public bool ConflitoAusenciaDemissao(Colaborador colaborador, AusenciaColaboradores ausenciaColaborador)
        {
            if (colaborador.DataDemissao >= ausenciaColaborador.DataFim && ((colaborador.DataDemissao >= ausenciaColaborador.DataFim) || colaborador.DataDemissao == null))
                return false;
            return true;
        }

        public ActionResult AdicionaAusenciaColaborador(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim, int empresa = 0, bool TodosColaboradores = false, bool TodasEmpresas = false)
        {
            ausenciaColaboradores.HoraInicio = HoraInicio.Hour;
            ausenciaColaboradores.MinutoInicio = HoraInicio.Minute;
            ausenciaColaboradores.HoraFim = HoraFim.Hour;
            ausenciaColaboradores.MinutoFim = HoraFim.Minute;
            ausenciaColaboradores.DataInicio = ausenciaColaboradores.DataInicio.GetValueOrDefault().AddHours(HoraInicio.Hour).AddMinutes(HoraInicio.Minute);
            ausenciaColaboradores.DataFim = ausenciaColaboradores.DataFim.GetValueOrDefault().AddHours(HoraFim.Hour).AddMinutes(HoraFim.Minute);
            ausenciaColaboradores.Ausencia = dbAusencia.BuscarPorId(ausenciaColaboradores.AusenciaId);
            ausenciaColaboradores.Colaborador = dbColaborador.BuscarPorId(ausenciaColaboradores.ColaboradorId);
            ausenciaColaboradores.MotivoAusencia = dbMotivoAusencia.BuscarPorId(ausenciaColaboradores.MotivoAusenciaId);
            ausenciaColaboradores.Colaborador = ausenciaColaboradores.Colaborador ?? new Colaborador();
            var model = new AusenciaViewModels {
                AusenciaColaboradores = ausenciaColaboradores,
                TodosColaboradores = TodosColaboradores,
                TodasEmpresas = TodasEmpresas,
                Empresa = empresa!=0 ? dbEmpresa.BuscarPorId(empresa) : new Empresa()
            };

            if (ModelState.IsValid)
            {
                if (!DataInicioMaiorDataFinal(ausenciaColaboradores.DataInicio, HoraInicio.Hour, HoraInicio.Minute, ausenciaColaboradores.DataFim, HoraFim.Hour, HoraFim.Minute))
                {
                    if (TodosColaboradores)
                    {
                        if (TodasEmpresas)
                        {
                            foreach (var colaborador in dbColaborador.Lista())
                            {
                                ausenciaColaboradores.Id = 0;
                                ausenciaColaboradores.ColaboradorId = colaborador.Id;
                                if (!ConflitoAusenciaDemissao(colaborador,ausenciaColaboradores))
                                    dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                            }
                            model = new AusenciaViewModels();
                        }
                        else
                        {
                            foreach (var colaborador in dbColaborador.Filtro("Empresa", empresa.ToString()))
                            {
                                ausenciaColaboradores.Id = 0;
                                ausenciaColaboradores.ColaboradorId = colaborador.Id;
                                if (!ConflitoAusenciaDemissao(colaborador, ausenciaColaboradores))
                                    dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                            }
                            model = new AusenciaViewModels();
                        }
                    }
                    else
                    {
                        Colaborador buscaColaborador = (dbColaborador.BuscarPorId(ausenciaColaboradores.ColaboradorId));
                        if (!ConflitoAusenciaDemissao(buscaColaborador, ausenciaColaboradores))
                        {
                            dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                            model = new AusenciaViewModels();
                        }
                        else
                            ModelState.AddModelError("ausenciaColaboradores.ColaboradorId", "Data de lançamento em conflito com a data de demissão do colaborador");

                    }

                }
                else
                    
                    ModelState.AddModelError("ausenciaColaboradores.DataInicio", "Data e hora inicial, não pode ser maior que Data e hora final!");

            }
            model.AusenciaColaboradores.Ausencia = ausenciaColaboradores.Ausencia;
            model.AusenciaColaboradoresLista = dbAusenciaColaborador.Lista(ausenciaColaboradores.Ausencia.Id);
            return View("Form", model);

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

        public ActionResult ExcluirAusenciaColaborador(int AusenciaColaboradorId)
        {
            //Antes de excluir é feito a verificação se o horario existe 
            var pesquisa = dbAusenciaColaborador.BuscarPorId(AusenciaColaboradorId);
            var IdAusenciaColaborador = pesquisa.AusenciaId;

            //Caso encontre algo, exluir o registro
            if (pesquisa != null)
                dbAusenciaColaborador.ExcluirAusenciaColaboradores(pesquisa);

            return RedirectToAction("Form", new { id = IdAusenciaColaborador });
        }
        public bool DataInicioMaiorDataFinal(DateTime? dataInicio, int horaInicio, int minutoInicio, DateTime? dataFim, int horaFim, int minutoFim)
        {
            dataInicio.GetValueOrDefault().AddHours(horaInicio).AddMinutes(minutoInicio);
            dataFim.GetValueOrDefault().AddHours(horaFim).AddMinutes(minutoFim);
            if (dataInicio > dataFim)
                return true;

            return false;
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