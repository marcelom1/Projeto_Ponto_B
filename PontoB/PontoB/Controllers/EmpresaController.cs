﻿using Microsoft.EntityFrameworkCore;
using PontoB.DAO;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {

            EmpresaDAO dao = new EmpresaDAO();
            IList<Empresa> empresas = dao.Lista();
            ViewBag.Empresas = empresas;
            return View();
        }


        public ActionResult Filtro(string coluna, string texto)
        {
            EmpresaDAO dao = new EmpresaDAO();
            IList<Empresa> filtro = dao.Filtro(coluna, texto);
            ViewBag.Empresas = filtro;
            return View("Index");
        }
        public ActionResult Form(int id=0)
        {
            EstadosUFDAO EstadoDao = new EstadosUFDAO();
            IList<EstadosUF> estados = EstadoDao.Lista();
            ViewBag.EstadosUf = estados;

            if (id != 0)
            {
                
                EmpresaDAO dao = new EmpresaDAO();
                Empresa empresa = dao.BuscarPorId(id);
                ViewBag.Empresa = empresa;

                
                return View();
            }

            
            ViewBag.Empresa = new Empresa();
            ViewBag.Empresa.EnderecoEmpresa = new Endereco();
           



            return View();
        
        }

        [HttpPost]
        public ActionResult Adiciona(Empresa empresa)
        {

            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(empresa.Id);
           
            if (ModelState.IsValid)
            {
                EstadosUFDAO daoEstado = new EstadosUFDAO();
                var pesquisarEstado = daoEstado.BuscarPorId(empresa.EnderecoEmpresa.Estado.Id);
                empresa.EnderecoEmpresa.Estado = pesquisarEstado;
                
                if (pesquisa != null)
                {
                    dao.Atualiza(empresa);

                }
                else
                {

                    dao.Adiciona(empresa);
                }
                return RedirectToAction("Index", "Empresa");
            }
            else
            {
                ViewBag.Empresa = empresa;
                return View("Form");
            }
        }

       

        [HttpPost]
        public ActionResult Excluir(Empresa empresa)
        {
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(empresa.Id);

            if (pesquisa != null)
            {
                dao.ExcluirEmpresa(pesquisa);

            }
           
            return RedirectToAction("Index", "Empresa");
        }

       public ActionResult Excluir(int id)
        {
            EmpresaDAO dao = new EmpresaDAO();
            var pesquisa = dao.BuscarPorId(id);

            if (pesquisa != null)
            {
                dao.ExcluirEmpresa(pesquisa);

            }

            return RedirectToAction("Index", "Empresa");
        }


    }
}