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
        // GET: Escalas
        public ActionResult Index()
        {
            EscalaDAO dao = new EscalaDAO();
            IList<Escala> escala = dao.Lista();
            ViewBag.Escalas = escala;

            return View();
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
                    EscalaHorarioDAO dao = new EscalaHorarioDAO();
                    dao.Adiciona(escalaHorario);
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

                EscalaHorarioDAO dao = new EscalaHorarioDAO();
                dao.Adiciona(escalaHorario);
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
                //EnderecoDAO daoe = new EnderecoDAO();
                //daoe.ExcluirEndereco(pesquisa.EnderecoEmpresa);

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


        /*public ActionResult Filtro(string coluna, string texto)
        {
            EscalaDAO dao = new EscalaDAO();
            IList<Escala> filtro = dao.Filtro(coluna, texto);
            ViewBag.Empresas = filtro;
            return View("Index");
           
        }*/
        /* public ActionResult Adiciona(Empresa empresa)
         {
         }

         public ActionResult Excluir(Empresa empresa)
         {

         }
         public ActionResult Excluir(int id)
         {
         }*/

    }

}
