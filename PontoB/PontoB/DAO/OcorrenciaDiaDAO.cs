using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class OcorrenciaDiaDAO
    {
        public void Adiciona(OcorrenciaDia registro)
        {

            using (var context = new PontoContex())
            {

                registro.Colaborador = context.Colaborador.Find(registro.ColaboradorId);

                context.OcorrenciaDia.Add(registro);
                context.SaveChanges();
            }
        }
        public IList<OcorrenciaDia> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.OcorrenciaDia.Include(r => r.Colaborador).ToList();
            }
        }

        /*
        public IList<OcorrenciaDia> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroRegistroPonto.ObterFiltroColuna(coluna);


                return objFiltro.Filtrar(contexto
                         .OcorrenciaDia
                         .Include(e => e.Colaborador)
                         .AsNoTracking(), filtro);

            }
        }
        */



        public bool ExcluirOcorrenciaDia(OcorrenciaDia registro)
        {
            using (var contexto = new PontoContex())
            {

                    contexto.OcorrenciaDia.Remove(registro);
                    contexto.SaveChanges();
                    return true;
               
            }


        }
        public OcorrenciaDia BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.OcorrenciaDia.Include(e => e.Colaborador).Where(e => e.Id == id).FirstOrDefault();
            }
        }




        public void Atualiza(OcorrenciaDia registro)
        {
            using (var contexto = new PontoContex())
            {

                registro.Colaborador = contexto.Colaborador.Find(registro.ColaboradorId);
                contexto.OcorrenciaDia.Update(registro);
                contexto.SaveChanges();
            }
        }


    }
}