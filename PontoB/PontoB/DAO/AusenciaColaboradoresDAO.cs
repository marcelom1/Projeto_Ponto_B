using PagedList;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace PontoB.DAO
{
    public class AusenciaColaboradoresDAO
    {
        public void Adiciona(AusenciaColaboradores ausencia)
        {
            using (var context = new PontoContex())
            {
                ausencia.Colaborador = context.Colaborador.Find(ausencia.ColaboradorId);
                ausencia.MotivoAusencia = context.MotivoAusencia.Find(ausencia.MotivoAusenciaId);
                ausencia.Ausencia = context.Ausencia.Find(ausencia.AusenciaId);
                context.AusenciaColaboradores.Add(ausencia);
                context.SaveChanges();
            }
        }
        public IList<AusenciaColaboradores> Lista()
        {

            using (var contexto = new PontoContex())
            {
                return contexto.AusenciaColaboradores.Include(e=>e.MotivoAusencia).Include(e=>e.Ausencia).Include(e=>e.Colaborador).Include(e=>e.Colaborador.Empresa).OrderByDescending(a => a.DataInicio).ToList();
            }
        }

        public IList<AusenciaColaboradores> Lista(int idAusencia)
        {

            using (var contexto = new PontoContex())
            {
                return contexto.AusenciaColaboradores.Include(e=>e.Colaborador).Include(e=>e.Ausencia).Include(e=>e.Colaborador.Empresa).Include(e=>e.MotivoAusencia).Where(a=>a.AusenciaId.Equals(idAusencia)).OrderByDescending(a => a.DataInicio).ToList();
            }
        }



        
        /*public IList<Escala> Filtro(string coluna, string filtro)
        {

            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroEscala.ObterFiltroColuna(coluna);
                var result = objFiltro.Filtrar(contexto
                         .Escala
                         .AsNoTracking(), filtro);
                return result;
            }

        }*/

        public void ExcluirAusenciaColaboradores(AusenciaColaboradores ausencia)
        {
            using (var contexto = new PontoContex())
            {


                contexto.AusenciaColaboradores.Remove(ausencia);
                contexto.SaveChanges();

            }


        }
         public AusenciaColaboradores BuscarPorId(int id)
         {
             using (var contexto = new PontoContex())
             {
                 return contexto.AusenciaColaboradores.Where(e => e.Id == id).FirstOrDefault();
             }
         }
        public void Atualiza(AusenciaColaboradores ausencia)
        {
            using (var contexto = new PontoContex())
            {


                contexto.AusenciaColaboradores.Update(ausencia);
                contexto.SaveChanges();
            }
        }
    }
}