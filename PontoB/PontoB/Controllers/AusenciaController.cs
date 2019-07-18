using PagedList;
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

        public ActionResult AdicionaAusenciaColaborador(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim, int? empresa, bool TodosColaboradores = false, bool TodasEmpresas = false)
        {
            if (!DataInicioMaiorDataFinal(ausenciaColaboradores.DataInicio, HoraInicio.Hour, HoraInicio.Minute, ausenciaColaboradores.DataFim, HoraFim.Hour, HoraFim.Minute))
            {
                ausenciaColaboradores.HoraInicio = HoraInicio.Hour;
                ausenciaColaboradores.MinutoInicio = HoraInicio.Minute;
                ausenciaColaboradores.HoraFim = HoraFim.Hour;
                ausenciaColaboradores.MinutoFim = HoraFim.Minute;
                if (TodosColaboradores)
                {
                    if (TodasEmpresas)
                    {
                        foreach (var colaborador in dbColaborador.Lista())
                        {
                            ausenciaColaboradores.Id = 0;
                            ausenciaColaboradores.ColaboradorId = colaborador.Id;
                            dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                        }
                    }
                    else
                    {
                        foreach (var colaborador in dbColaborador.Filtro("Empresa", empresa.ToString()))
                        {
                            ausenciaColaboradores.Id = 0;
                            ausenciaColaboradores.ColaboradorId = colaborador.Id;
                            dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                        }
                    }
                }
                else
                {
                    dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
                }
            }


            return RedirectToAction("Form", new { id = ausenciaColaboradores.AusenciaId });
        }


        public ActionResult Form(int id = 0)
        {
            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados da ausência
            if (id != 0)
            {
                ViewBag.Ausencia = dbAusencia.BuscarPorId(id);
                var model = dbAusenciaColaborador.Lista(id);

                return View(model);
            }

            //Senão a ausência é nova 
            ViewBag.Ausencia = new Ausencia();
            

            return View(dbAusenciaColaborador.Lista(0));
        }

        [HttpPost]
        public ActionResult Adiciona(Ausencia ausencia)
        {

            //Busca se já existe a ausência
            var pesquisa = dbAusencia.BuscarPorId(ausencia.Id);
            ViewBag.Ausencia = pesquisa;
            var model = dbAusenciaColaborador.Lista(ausencia.Id);

            if (ModelState.IsValid)
            {
                //Caso a ausencia já existir ela será atualizada
                if (pesquisa != null)
                {
                    dbAusencia.Atualiza(ausencia);
                    return RedirectToAction("Index", "Ausencia");
                }
                else //Senão é uma escala nova sendo adicionada
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
        public bool DataInicioMaiorDataFinal(DateTime dataInicio, int horaInicio, int minutoInicio, DateTime dataFim, int horaFim, int minutoFim)
        {
            dataInicio.AddHours(horaInicio).AddMinutes(minutoInicio);
            dataFim.AddHours(horaFim).AddMinutes(minutoFim);
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