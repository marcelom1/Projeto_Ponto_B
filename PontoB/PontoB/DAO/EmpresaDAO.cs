
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;
using Empresa = PontoB.Models.Empresa;
using Microsoft.EntityFrameworkCore;
using PontoB.Filtros;
using PontoB.Filtros.FEmpresa;

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
        public IList<Empresa> Lista()
        {
            using(var contexto = new PontoContex())
            {
                return contexto.Empresa.ToList();
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


                if (coluna == "Todos")
                {
                    var resultado = contexto
                         .Empresa
                         .AsNoTracking()
                         .Where(e => e.RazaoSocial.Contains(filtro) || e.NomeFantasia.Contains(filtro) || e.Cnpj.Contains(filtro))
                         .ToList();
                    if (int.TryParse(filtro,out int numero))
                    {
                        resultado.AddRange(contexto
                        .Empresa
                        .AsNoTracking()
                        .Where(e => e.Id.Equals(numero)).ToList());
                       
                    }
                    return resultado;

                }
                else
                {
                    if (coluna == "Código")
                    {
                        return contexto
                             .Empresa
                             .AsNoTracking()
                             .Where(e => e.Id.Equals(filtro))
                             .ToList();
                    }
                    else
                    {
                        if (coluna == "Razão Social")
                        {
                            return contexto
                               .Empresa
                               .AsNoTracking()
                               .Where(e => e.RazaoSocial.Equals(filtro))
                               .ToList();
                        }
                        else
                        {
                            if (coluna == "Nome Fantasia")
                            {
                                return contexto
                                   .Empresa
                                   .AsNoTracking()
                                   .Where(e => e.NomeFantasia.Equals(filtro))
                                   .ToList();
                            }
                            else
                            {
                                if (coluna == "CNPJ")
                                {
                                    return contexto
                                       .Empresa
                                       .AsNoTracking()
                                       .Where(e => e.Cnpj.Equals(filtro))
                                       .ToList();
                                }
                            }
                        }
                    }
                }
              return contexto.Empresa.ToList();
            }
        }
               
       

        public void ExcluirEmpresa(Empresa empresa)
        {
            using(var contexto = new PontoContex())
            {

               // contexto.Endereco.Remove(empresa.EnderecoEmpresa);
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