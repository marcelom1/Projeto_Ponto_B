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
                var filtro = contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana== "Segunda").OrderBy(p=>p.EntradaHora).OrderBy(p=>p.EntradaMinuto).ToList();
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Terça").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Quarta").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Quinta").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Sexta").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Sábado").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                filtro.AddRange(contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala) && p.DiaSemana == "Domingo").OrderBy(p => p.EntradaHora).OrderBy(p => p.EntradaMinuto).ToList());
                return filtro;
 
            }
        }

        public void Adiciona(EscalaHorario escalaHorario)
        {
            using (var context = new PontoContex())
            {
                
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