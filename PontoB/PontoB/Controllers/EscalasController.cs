using PagedList;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    public class EscalasController : Controller
    {
        public ActionResult Index(int pagina = 1, string coluna = "", string filtro = "")
        {
            if (filtro != "" && coluna != "")
            {
                return RedirectToAction("Filtro", new { coluna, texto = filtro, pagina });

            }
            EscalaDAO dao = new EscalaDAO();
            ViewBag.FiltroColuna = "";
            ViewBag.Filtro = "";

            return View(dao.Lista(pagina));
        }
       

        public ActionResult Form(int id = 0)
        {

            EscalaHorarioDAO horarioDao = new EscalaHorarioDAO();
            IList<EscalaHorario> escalaHorario;

            if (id != 0)
            {

                EscalaDAO dao = new EscalaDAO();
                Escala escala = dao.BuscarPorId(id);
                ViewBag.Escalas = escala;
                escalaHorario = horarioDao.Lista(id);
                ViewBag.Escalas.EscalasHorario = escalaHorario;


                return View();
            }


            ViewBag.Escalas = new Escala();
            escalaHorario = horarioDao.Lista(0);
            ViewBag.Escalas.EscalasHorario = escalaHorario;

            return View();
        }


        [HttpPost]
        public ActionResult Adiciona(Escala escala)
        {

            EscalaDAO dao = new EscalaDAO();
            var pesquisa = dao.BuscarPorId(escala.Id);
            ViewBag.Escalas = escala;
            EscalaHorarioDAO daoHorario = new EscalaHorarioDAO();
            ViewBag.Escalas.EscalasHorario = daoHorario.Lista(escala.Id); 




            if (ModelState.IsValid)
            {
               
                if (pesquisa != null)
                {
                    dao.Atualiza(escala);

                }
                else
                {
                    
                    dao.Adiciona(escala);
                    return RedirectToAction("Form", new {id=escala.Id });
                    

                }
                return RedirectToAction("Index", "Escalas");
            }
            else
            {

                return View("Form");
            }
        }

        public bool SobrePoemHorario(int hora,int minuto,string DiaSemana, int IdEscala)
        {
            
            EscalaHorarioDAO dao = new EscalaHorarioDAO();
            IList<EscalaHorario> lista = dao.Lista(IdEscala);
            string Hora = hora.ToString("D2") + minuto.ToString("D2");

            foreach (var horario in lista)
            {
                if (DiaSemana == horario.DiaSemana)
                {
                    string HoraEntradaSalva = horario.EntradaHora.ToString("D2") + horario.EntradaMinuto.ToString("D2");
                    string HoraSaidaSalva = horario.SaidaHora.ToString("D2") + horario.SaidaMinuto.ToString("D2");
                    if (int.Parse(Hora) >= int.Parse(HoraEntradaSalva) && int.Parse(Hora) <= int.Parse(HoraSaidaSalva))
                    {
                        return true;
                    }
                }


            }
            return false;
        }

        [HttpPost]
        public ActionResult NovoHorario(string DiasDaSemana,DateTime NovoHoraEntrada, DateTime NovoHoraSaida, int EscalaID)
        {
            if (DiasDaSemana == "Segunda a Sexta")
            {
                string[] semana = { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };
                for (int i = 0; i < semana.Length; i++)
                {
                    EscalaHorario escalaHorario = new EscalaHorario()
                    {
                        EscalaId = EscalaID,
                        DiaSemana = semana[i],
                        EntradaHora = NovoHoraEntrada.Hour,
                        EntradaMinuto = NovoHoraEntrada.Minute,
                        SaidaHora = NovoHoraSaida.Hour,
                        SaidaMinuto = NovoHoraSaida.Minute,
                        TotalEmMinutos = ((NovoHoraSaida.Hour * 60) + NovoHoraSaida.Minute) - ((NovoHoraEntrada.Hour * 60) + NovoHoraEntrada.Minute)

                    };
                    if (!SobrePoemHorario(escalaHorario.EntradaHora, escalaHorario.EntradaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId))
                        if (!SobrePoemHorario(escalaHorario.SaidaHora, escalaHorario.SaidaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId))
                        {
                            EscalaHorarioDAO dao = new EscalaHorarioDAO();
                            dao.Adiciona(escalaHorario);
                        }
                }
            }
            else
            {

                EscalaHorario escalaHorario = new EscalaHorario()
                {
                    EscalaId = EscalaID,
                    DiaSemana = DiasDaSemana,
                    EntradaHora = NovoHoraEntrada.Hour,
                    EntradaMinuto = NovoHoraEntrada.Minute,
                    SaidaHora = NovoHoraSaida.Hour,
                    SaidaMinuto = NovoHoraSaida.Minute,
                    TotalEmMinutos = ((NovoHoraSaida.Hour * 60) + NovoHoraSaida.Minute) - ((NovoHoraEntrada.Hour * 60) + NovoHoraEntrada.Minute)

                };
                if (!SobrePoemHorario(escalaHorario.EntradaHora, escalaHorario.EntradaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId))
                    if (!SobrePoemHorario(escalaHorario.SaidaHora, escalaHorario.SaidaMinuto, escalaHorario.DiaSemana, escalaHorario.EscalaId))
                    {
                        EscalaHorarioDAO dao = new EscalaHorarioDAO();
                        dao.Adiciona(escalaHorario);
                    }
            }
            return RedirectToAction("Form", new { id = EscalaID });
          
        }

        public ActionResult ExcluirEscalaHorario(int EscalaHorarioID)
        {
            EscalaHorarioDAO dao = new EscalaHorarioDAO();
            var pesquisa = dao.BuscarPorId(EscalaHorarioID);
            var EscalaID = pesquisa.EscalaId;
            if (pesquisa != null)
            {

                dao.ExcluirEscalaHorario(pesquisa);
                //EnderecoDAO daoe = new EnderecoDAO();
                //daoe.ExcluirEndereco(pesquisa.EnderecoEmpresa);

            }

            return RedirectToAction("Form", new { id = EscalaID });
        }


        [HttpPost]
        public ActionResult Excluir(Escala escala)
        {
            EscalaDAO dao = new EscalaDAO();
            var pesquisa = dao.BuscarPorId(escala.Id);

            if (pesquisa != null)
            {

                dao.ExcluirEscala(pesquisa);
               

            }

            return RedirectToAction("Index", "Escalas");
        }

        public ActionResult Excluir(int id)
        {
            EscalaDAO dao = new EscalaDAO();
            var pesquisa = dao.BuscarPorId(id);

            if (pesquisa != null)
            {
                dao.ExcluirEscala(pesquisa);

            }

            return RedirectToAction("Index", "Escalas");
        }

        public ActionResult Filtro(string coluna, string texto, int pagina = 1)
        {
            EscalaDAO dao = new EscalaDAO();

            IPagedList<Escala> filtro = dao.Filtro(coluna, texto, pagina);
            ViewBag.Escala = filtro;
            ViewBag.FiltroColuna = coluna;
            ViewBag.Filtro = texto;
            return View("Index", filtro);
        }


    }

}
