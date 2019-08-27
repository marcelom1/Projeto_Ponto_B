using PagedList;
using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.ViewModels.VEscala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PontoB.Controllers
{
    [Authorize(Roles = "Master")]
    public class EscalasController : Controller
    {
        
        private EscalaDAO dbEscala = new EscalaDAO();
        private EscalaHorarioDAO dbEscalaHorario = new EscalaHorarioDAO();
        


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

            //retorna todas as empresas 
            return View(dbEscala.Lista(pagina));
        }
       

        public ActionResult Form(int id = 0)
        {
            //Caso o Id for != de Zero é efetuado uma busca no banco para trazer os dados da Escala
            if (id != 0)
            {
                ViewBag.Escalas = dbEscala.BuscarPorId(id);
                ViewBag.Escalas.EscalasHorario = dbEscalaHorario.Lista(id);

                return View();
            }

            //Senão a Escala é nova 
            ViewBag.Escalas = new Escala();
            ViewBag.Escalas.EscalasHorario = dbEscalaHorario.Lista(0);

            return View();
        }

        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {
            //Faz a Busca do filtro
            IPagedList<Escala> filtro = dbEscala.Filtro(coluna, texto).ToPagedList(pagina,10);

            //Preenche as ViewBag com os resultado do filtro
            ViewBag.Escala = filtro;
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;

            return View("Index", filtro);
        }

        [HttpPost]
        public ActionResult Adiciona(Escala escala)
        {
            //Busca se já existe a escala
            var pesquisa = dbEscala.BuscarPorId(escala.Id);
            ViewBag.Escalas = escala;
            ViewBag.Escalas.EscalasHorario = dbEscalaHorario.Lista(escala.Id); 

            if (ModelState.IsValid)
            {
                //Caso a Escala já existir ela será atualizada
                if (pesquisa != null)
                {
                    dbEscala.Atualiza(escala);
                    return RedirectToAction("Index", "Escalas");
                }
                else //Senão é uma escala nova sendo adicionada
                {
                    dbEscala.Adiciona(escala);
                    return RedirectToAction("Form", new { id = escala.Id });
                }
            }
            return View("Form");
        }


        //Metodo Para verificar se um determinado horario se sobrepões outro horario já Salvo
        public bool SobrePoemHorario(int hora,int minuto,string DiaSemana, int IdEscala)
        {
            //Busco todas as horas de uma determinada Escala
            IList<EscalaHorario> lista = dbEscalaHorario.Lista(IdEscala);
            string Hora = hora.ToString("D2") + minuto.ToString("D2");

            foreach (var horario in lista)
            {
                //Verifico apenas os horarios do dia da semana
                if (DiaSemana == horario.DiaSemana)
                {
                    //Formato as horas Exemplo de 12:45 para 1245
                    string HoraEntradaSalva = horario.EntradaHora.ToString("D2") + horario.EntradaMinuto.ToString("D2");
                    string HoraSaidaSalva = horario.SaidaHora.ToString("D2") + horario.SaidaMinuto.ToString("D2");

                    //Verifico se o valor novo se encontra no meio do valor já salvo
                    if (int.Parse(Hora) >= int.Parse(HoraEntradaSalva) && int.Parse(Hora) <= int.Parse(HoraSaidaSalva))
                        return true;
                }
            }
            return false;
        }

        [HttpPost]
        public ActionResult NovoHorario(IList<string> checks, DateTime NovoHoraEntrada, DateTime NovoHoraSaida, int EscalaID)
        {

            if (ModelState.IsValid)
            {
                //Para cada dia da semana selecionado é criado uma nova linha
                foreach (var Semana in checks)
                {
                        EscalaHorario escalaHorario = new EscalaHorario()
                        {
                            EscalaId = EscalaID,
                            DiaSemana = Semana,
                            EntradaHora = NovoHoraEntrada.Hour,
                            EntradaMinuto = NovoHoraEntrada.Minute,
                            SaidaHora = NovoHoraSaida.Hour,
                            SaidaMinuto = NovoHoraSaida.Minute,
                            TotalEmMinutos = ((NovoHoraSaida.Hour * 60) + NovoHoraSaida.Minute) - ((NovoHoraEntrada.Hour * 60) + NovoHoraEntrada.Minute)

                        };

                        //Verificação de Horario SobrePosto
                        if ((!SobrePoemHorario(escalaHorario.EntradaHora, escalaHorario.EntradaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId)) && (!SobrePoemHorario(escalaHorario.SaidaHora, escalaHorario.SaidaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId)))
                            dbEscalaHorario.Adiciona(escalaHorario);
                        else
                            ModelState.AddModelError("AdicionarNovoHorario", "O novo horário não pode sobrepor outro horário existente");
                    
                }
            }


            ViewBag.Escalas = dbEscala.BuscarPorId(EscalaID);
            ViewBag.Escalas.EscalasHorario = dbEscalaHorario.Lista(EscalaID);


            return View("Form");


        }
        
        public ActionResult ExcluirEscalaHorario(int EscalaHorarioID)
        {
            //Antes de excluir é feito a verificação se o horario existe 
            var pesquisa = dbEscalaHorario.BuscarPorId(EscalaHorarioID);
            var EscalaID = pesquisa.EscalaId;

            //Caso encontre algo, exluir o registro
            if (pesquisa != null)
                dbEscalaHorario.ExcluirEscalaHorario(pesquisa);
 
            return RedirectToAction("Form", new { id = EscalaID });
        }

        [HttpPost]
        public ActionResult Excluir(Escala escala)
        {
            //Antes de excluir é feito a verificação se a escala existe 
            var pesquisa = dbEscala.BuscarPorId(escala.Id);

            //Caso encontre algo, exluir o registro
            try
            {
                if (pesquisa != null)
                    dbEscala.ExcluirEscala(pesquisa);
            }
            catch (Exception)
            {
                ModelState.AddModelError("erro", "Não é possivel excluir escalas que já estão atrelada a algum colaborador");
                ViewBag.Escalas = dbEscala.BuscarPorId(pesquisa.Id);
                ViewBag.Escalas.EscalasHorario = dbEscalaHorario.Lista(pesquisa.Id);
                return View("Form", new { id = pesquisa.Id });
            }

            return RedirectToAction("Index", "Escalas");
        }

        public ActionResult Excluir(int id)
        {
            //Antes de excluir é feito a verificação se a escala existe 
            var pesquisa = dbEscala.BuscarPorId(id);

            //Caso encontre algo, exluir o registro
            try
            {
                if (pesquisa != null)
                    dbEscala.ExcluirEscala(pesquisa);
            }
            catch (Exception)
            {
                ModelState.AddModelError("erro", "Não é possivel excluir escalas que já estão atrelada a algum colaborador");
                ViewBag.FiltroColuna = "";
                ViewBag.Filtro = "";
                return View("Index", dbEscala.Lista(1));
            }

            return RedirectToAction("Index", "Escalas");
        }


        


    }

}
