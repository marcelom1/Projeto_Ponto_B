﻿using PontoB.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;
using Empresa = PontoB.Models.Empresa;
using Microsoft.EntityFrameworkCore;

namespace PontoB.DAO
{
    public class EmpresaDAO
    {
        public void Adiciona(Empresa empresa)
        {
            using (var context = new PontoContex())
            {
                context.Empresa.Add(empresa);
                context.SaveChanges();
            }
        }
        public IList<Empresa> Lista()
        {
            using(var contexto = new PontoContex())
            {
                return contexto.Empresa.ToList();
            }
        }

        public void ExcluirEmpresa(Empresa empresa)
        {
            using(var contexto = new PontoContex())
            {
                contexto.Empresa.Remove(empresa);
                contexto.SaveChanges();
            }

        }
        public Empresa BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Empresa.Include("Categoria")
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }
        public void Atualiza(Empresa empresa)
        {
            using (var contexto = new PontoContex())
            {
                //contexto.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
                //contexto.SaveChanges();
            }
        }
    }
}