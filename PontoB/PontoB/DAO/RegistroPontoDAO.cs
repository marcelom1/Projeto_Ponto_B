using Microsoft.EntityFrameworkCore;
using PontoB.Filtros.FRegistroPonto;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PontoB.DAO
{
    public class RegistroPontoDAO
    {
        public void Adiciona(RegistroPonto registro)
        {

            using (var context = new PontoContex())
            {

                registro.Colaborador = context.Colaborador.Find(registro.ColaboradorId);

                context.RegistroPonto.Add(registro);
                context.SaveChanges();
            }
        }
        public IList<RegistroPonto> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.RegistroPonto.Include(r => r.Colaborador).OrderByDescending(r => r.DataRegistro).ToList();
            }
        }


        public IList<RegistroPonto> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroRegistroPonto.ObterFiltroColuna(coluna);


                return objFiltro.Filtrar(contexto
                         .RegistroPonto
                         .Include(e => e.Colaborador)
                         .AsNoTracking(), filtro);

            }
        }

   


        public bool ExcluirRegistroPonto(RegistroPonto registro)
        {
            using (var contexto = new PontoContex())
            {

                if (registro.RegistroManual)
                {
                    contexto.RegistroPonto.Remove(registro);
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }


        }
        public RegistroPonto BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.RegistroPonto.Include(e => e.Colaborador).Where(e => e.Id == id).FirstOrDefault();
            }
        }

       


        public void Atualiza(RegistroPonto registro)
        {
            using (var contexto = new PontoContex())
            {

                registro.Colaborador = contexto.Colaborador.Find(registro.ColaboradorId);
                contexto.RegistroPonto.Update(registro);
                contexto.SaveChanges();
            }
        }

       

    }
}
