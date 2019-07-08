using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class EscalaDAO
    {
        public void Adiciona(Escala escala)
        {
            using (var context = new PontoContex())
            {
                
                context.Escala.Add(escala);
                context.SaveChanges();
            }
        }
        public IList<Escala> Lista()
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Escala.ToList();
            }
        }


       /* public IList<Escala> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroEmpresa.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
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
                    if (int.TryParse(filtro, out int numero))
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
        }*/



        public void ExcluirEscala(Escala escala)
        {
            using (var contexto = new PontoContex())
            {

               
                contexto.Escala.Remove(escala);
                contexto.SaveChanges();

            }


        }
       public Escala BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Escala.Include(e=>e.EscalasHorario).Where(e => e.Id == id).FirstOrDefault();
            }
        }
        public void Atualiza(Escala escala)
        {
            using (var contexto = new PontoContex())
            {


                contexto.Escala.Update(escala);
                contexto.SaveChanges();
            }
        }
    }
}