using Microsoft.EntityFrameworkCore;
using PagedList;
using PontoB.Filtros.FEscala;
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
        public IPagedList<Escala> Lista(int? pagina)
        {
            int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;
            using (var contexto = new PontoContex())
            {
                return contexto.Escala.OrderBy(p => p.Descricao).ToPagedList(numeroPagina, tamanhoPagina);
            }
        }


        public IList<Escala> Filtro(string coluna, string filtro)
        {
            
            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroEscala.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
                         .Escala
                         .AsNoTracking(), filtro);
                return result;
            }

        }

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