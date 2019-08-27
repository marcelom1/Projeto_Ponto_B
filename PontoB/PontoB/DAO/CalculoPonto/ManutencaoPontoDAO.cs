using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class ManutencaoPontoDAO
    {
        public void Adiciona(ManutencaoPonto calculo)
        {

            using (var context = new PontoContex())
            {

                calculo.Colaborador = context.Colaborador.Find(calculo.ColaboradorId);

                context.ManutencaoPonto.Add(calculo);
                context.SaveChanges();
            }
        }
        public IList<ManutencaoPonto> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.ManutencaoPonto.Include(c =>c.Colaborador ).OrderBy(c => c.Data).ToList();
            }
        }

        


        public void ExcluirRegistroPonto(ManutencaoPonto calculo)
        {
            using (var contexto = new PontoContex())
            {


                contexto.ManutencaoPonto.Remove(calculo);
                contexto.SaveChanges();

            }


        }
        public ManutencaoPonto BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.ManutencaoPonto.Include(e => e.Colaborador).Where(e => e.Id == id).FirstOrDefault();
            }
        }




        public void Atualiza(ManutencaoPonto calculo)
        {
            using (var contexto = new PontoContex())
            {

                calculo.Colaborador = contexto.Colaborador.Find(calculo.ColaboradorId);
                contexto.ManutencaoPonto.Update(calculo);
                contexto.SaveChanges();
            }
        }

    }
}