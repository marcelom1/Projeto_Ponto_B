using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class CalculoPontoDAO
    {
        public void Adiciona(CalculoPonto calculo)
        {

            using (var context = new PontoContex())
            {

                calculo.Colaborador = context.Colaborador.Find(calculo.ColaboradorId);

                context.CalculoPonto.Add(calculo);
                context.SaveChanges();
            }
        }
        public IList<CalculoPonto> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.CalculoPonto.Include(c =>c.Colaborador ).OrderBy(c => c.Data).ToList();
            }
        }

        /*
        public IList<CalculoPonto> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroRegistroPonto.ObterFiltroColuna(coluna);


                return objFiltro.Filtrar(contexto
                         .RegistroPonto
                         .Include(e => e.Colaborador)
                         .AsNoTracking(), filtro);

            }
        }*/


        public void ExcluirRegistroPonto(CalculoPonto calculo)
        {
            using (var contexto = new PontoContex())
            {


                contexto.CalculoPonto.Remove(calculo);
                contexto.SaveChanges();

            }


        }
        public CalculoPonto BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.CalculoPonto.Include(e => e.Colaborador).Where(e => e.Id == id).FirstOrDefault();
            }
        }




        public void Atualiza(CalculoPonto calculo)
        {
            using (var contexto = new PontoContex())
            {

                calculo.Colaborador = contexto.Colaborador.Find(calculo.ColaboradorId);
                contexto.CalculoPonto.Update(calculo);
                contexto.SaveChanges();
            }
        }

    }
}