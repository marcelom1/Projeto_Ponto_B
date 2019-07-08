﻿using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Empresa.EscalaHorario = escala.EscalasHorario;
            if (ModelState.IsValid)
            {
               
                if (pesquisa != null)
                {
                    dao.Atualiza(escala);

                }
                else
                {
                    //try
                   // {
                        dao.Adiciona(escala);
                    //}
                    //catch (Exception)
                    //{
                        /*ViewBag.Empresa.Id = 0;
                        ViewBag.Empresa.EnderecoEmpresa.Id = 0;
                        ModelState.AddModelError("empresa.Cnpj", "CNPJ já consta cadastrado no Banco de Dados");
                        */
                     //   return View("Form");
                    //}

                }
                return RedirectToAction("Index", "Escala");
            }
            else
            {

                return View("Form");
            }
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

            return RedirectToAction("Index", "Escala");
        }

        public ActionResult Excluir(int id)
        {
            EscalaDAO dao = new EscalaDAO();
            var pesquisa = dao.BuscarPorId(id);

            if (pesquisa != null)
            {
                dao.ExcluirEscala(pesquisa);

            }

            return RedirectToAction("Index", "Escala");
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