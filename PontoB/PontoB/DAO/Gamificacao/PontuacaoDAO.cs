using Microsoft.EntityFrameworkCore;
using PontoB.Filtros.FGamificacao;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class PontuacaoDAO
    {

        public void Adiciona(Pontuacao registro)
        {

            using (var context = new PontoContex())
            {

                registro.Colaborador = context.Colaborador.Find(registro.ColaboradorId);

                context.Pontuacao.Add(registro);
                context.SaveChanges();
            }
        }
        public IList<Pontuacao> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.Pontuacao.Include(r => r.Colaborador).OrderByDescending(r => r.DataRegistro).ToList();
            }
        }


        public IList<Pontuacao> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroGamificacao.ObterFiltroColuna(coluna);


                return objFiltro.Filtrar(contexto
                         .Pontuacao
                         .Include(e => e.Colaborador)
                         .AsNoTracking(), filtro);

            }
        }




        public bool ExcluirPontuacao(Pontuacao registro)
        {
            using (var contexto = new PontoContex())
            {

               
                    contexto.Pontuacao.Remove(registro);
                    contexto.SaveChanges();
                    return true;
               
            }


        }
        public Pontuacao BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Pontuacao.Include(e => e.Colaborador).Where(e => e.Id == id).FirstOrDefault();
            }
        }




        public void Atualiza(Pontuacao registro)
        {
            using (var contexto = new PontoContex())
            {

                registro.Colaborador = contexto.Colaborador.Find(registro.ColaboradorId);
                contexto.Pontuacao.Update(registro);
                contexto.SaveChanges();
            }
        }

    }
}