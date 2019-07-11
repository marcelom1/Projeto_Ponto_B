
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
        public void Adiciona(Empresa empresa)
        {
            using (var context = new PontoContex())
            {
                context.Entry(empresa.EnderecoEmpresa.Estado).State = EntityState.Unchanged;
                context.Empresa.Add(empresa);
                context.SaveChanges();
            }
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


        public IPagedList<Empresa> Filtro(string coluna, string filtro, int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroEmpresa.ObterFiltroColuna(coluna);
                var result= objFiltro.Filtrar(contexto
                         .Empresa
                         .AsNoTracking(), filtro);
                return result.ToPagedList(numeroPagina,tamanhoPagina);
                
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