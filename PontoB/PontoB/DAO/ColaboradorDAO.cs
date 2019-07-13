using Microsoft.EntityFrameworkCore;
using PagedList;
using PontoB.Filtros.FColaborador;
using PontoB.Models;
using System.Collections.Generic;
using System.Linq;

namespace PontoB.DAO
{
    public class ColaboradorDAO
    {
        public void Adiciona(Colaborador colaborador)
        {
            using (var context = new PontoContex())
            {
                context.Entry(colaborador.EnderecoColaborador.Estado).State = EntityState.Unchanged;
                context.Colaborador.Add(colaborador);
                context.SaveChanges();
            }
        }
        public IList<Colaborador> Lista()
        {
            
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.OrderBy(p => p.NomeCompleto).ToList();
            }
        }


        public IList<Colaborador> Filtro(string coluna, string filtro)
        {
            
            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroColaborador.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
                         .Colaborador
                         .AsNoTracking(), filtro);

                return result;

            }
        }



        public void ExcluirColaborador(Colaborador colaborador)
        {
            using (var contexto = new PontoContex())
            {


                contexto.Colaborador.Remove(colaborador);
                contexto.SaveChanges();

            }


        }
        public Colaborador BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.Include(e => e.EnderecoColaborador).Include(e => e.EnderecoColaborador.Estado).Where(e => e.Id == id).FirstOrDefault();
            }
        }
        public void Atualiza(Colaborador colaborador)
        {
            using (var contexto = new PontoContex())
            {
                contexto.Colaborador.Update(colaborador);
                contexto.SaveChanges();
            }
        }
    
    }
}