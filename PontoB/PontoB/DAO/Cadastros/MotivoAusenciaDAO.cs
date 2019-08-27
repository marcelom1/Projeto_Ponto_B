using Microsoft.EntityFrameworkCore;
using PontoB.Filtros.FAusencia;
using PontoB.Filtros.FAusencia.FMotivoAusencia;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class MotivoAusenciaDAO
    {
        public void Adiciona(MotivoAusencia motivo)
        {
            using (var context = new PontoContex())
            {
                context.MotivoAusencia.Add(motivo);
                context.SaveChanges();
            }
        }
        public IList<MotivoAusencia> Lista()
        {
            using (var contexto = new PontoContex())
            {
                return contexto.MotivoAusencia.ToList();
            }
        }

        public IList<MotivoAusencia> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroMotivoAusencia.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
                         .MotivoAusencia
                         .AsNoTracking(), filtro);
                return result;
            }

        }

        public void ExcluirEstado(MotivoAusencia motivo)
        {
            using (var contexto = new PontoContex())
            {


                contexto.MotivoAusencia.Remove(motivo);
                contexto.SaveChanges();

            }


        }
        public MotivoAusencia BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.MotivoAusencia.Find(id);
            }
        }
        public void Atualiza(MotivoAusencia motivo)
        {
            using (var contexto = new PontoContex())
            {

                contexto.MotivoAusencia.Update(motivo);
                contexto.SaveChanges();
            }
        }
    }
}