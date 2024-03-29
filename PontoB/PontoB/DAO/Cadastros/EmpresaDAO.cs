﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;
using Empresa = PontoB.Models.Empresa;
using Microsoft.EntityFrameworkCore;
using PontoB.Filtros;
using PontoB.Filtros.FEmpresa;
using PagedList;

namespace PontoB.DAO
{
    public class EmpresaDAO
    {
        public int Adiciona(Empresa empresa)
        {
            using (var context = new PontoContex())
            {
                if (empresa.EnderecoEmpresa != null)
                    context.Entry(empresa.EnderecoEmpresa.Estado).State = EntityState.Unchanged;
                context.Empresa.Add(empresa);
                context.SaveChanges();
            }
            return empresa.Id;
        }
        public IPagedList<Empresa> Lista(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            using(var contexto = new PontoContex())
            {
                return contexto.Empresa.OrderBy(p=>p.RazaoSocial).ToPagedList(numeroPagina,tamanhoPagina);
            }
        }


        public IList<Empresa> Filtro(string coluna, string filtro)
        {
            
            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroEmpresa.ObterFiltroColuna(coluna);
                var result= objFiltro.Filtrar(contexto
                         .Empresa
                         .AsNoTracking(), filtro);
                return result;
                
            }
        }
               
       

        public void ExcluirEmpresa(Empresa empresa)
        {
            using(var contexto = new PontoContex())
            {

              //  contexto.Endereco.Remove(empresa.EnderecoEmpresa);
                contexto.Empresa.Remove(empresa);
                contexto.SaveChanges();
                
            }


        }
        public Empresa BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Empresa.Include(e => e.EnderecoEmpresa).Include(e=>e.EnderecoEmpresa.Estado).Where(e=> e.Id==id).FirstOrDefault();
            }
        }
        public void Atualiza(Empresa empresa)
        {
            using (var contexto = new PontoContex())
            {
                contexto.Empresa.Update(empresa);
                contexto.SaveChanges();
            }
        }
    }
}