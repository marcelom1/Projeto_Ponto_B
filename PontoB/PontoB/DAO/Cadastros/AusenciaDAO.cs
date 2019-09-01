using PagedList;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Filtros.FAusencia;
using Microsoft.EntityFrameworkCore;

namespace PontoB.DAO
{
    public class AusenciaDAO
    {
        public int Adiciona(Ausencia ausencia)
        {
            using (var context = new PontoContex())
            {

                context.Ausencia.Add(ausencia);
                context.SaveChanges();
            }
            return ausencia.Id;
        }
        public IPagedList<Ausencia> Lista(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            using (var contexto = new PontoContex())
            {
                return contexto.Ausencia.OrderBy(p => p.Descricao).ToPagedList(numeroPagina, tamanhoPagina);
            }
        }

        
        public IList<AusenciaColaboradores> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroAusencia.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
                         .AusenciaColaboradores
                         .Include(e=>e.Ausencia)
                         .Include(e=>e.Colaborador)
                         .Include(e=>e.Colaborador.Empresa)
                         .Include(e=>e.MotivoAusencia)
                         .AsNoTracking(), filtro);
                return result;
            }

        }

        public void ExcluirAusencia(Ausencia ausencia)
        {
            using (var contexto = new PontoContex())
            {


                contexto.Ausencia.Remove(ausencia);
                contexto.SaveChanges();

            }


        }
        public Ausencia BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Ausencia.Where(e => e.Id == id).FirstOrDefault();
            }
        }

        public void Atualiza(Ausencia ausencia)
        {
            using (var contexto = new PontoContex())
            {


                contexto.Ausencia.Update(ausencia);
                contexto.SaveChanges();
            }
        }
    }

}