using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class EscalaHorarioDAO
    {
        public IList<EscalaHorario> Lista(int IdEscala)
        {
            using (var contexto = new PontoContex())
            {

                return contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala)).ToList();



            }
        }

        public void Adiciona(EscalaHorario escalaHorario)
        {
            using (var context = new PontoContex())
            {
                //context.Entry(new { escalaHorario.EscalaId }).State = EntityState.Unchanged;
                context.EscalaHorario.Add(escalaHorario);
                context.SaveChanges();
            }
        }

        public EscalaHorario BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.EscalaHorario.Where(e => e.Id == id).FirstOrDefault();
            }
        }

        public void ExcluirEscalaHorario(EscalaHorario horario)
        {
            using (var contexto = new PontoContex())
            {


                contexto.EscalaHorario.Remove(horario);
                contexto.SaveChanges();

            }


        }


    }
}