using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class EstadosUFDAO
    {
        public void Adiciona(EstadosUF estado )
        {
            using (var context = new PontoContex())
            {
                context.EstadoUF.Add(estado);
                context.SaveChanges();
            }
        }
        public IList<EstadosUF> Lista()
        {
            using (var contexto = new PontoContex())
            {
                return contexto.EstadoUF.ToList();
            }
        }

        public void ExcluirEstado(EstadosUF estado)
        {
            using (var contexto = new PontoContex())
            {

               
                contexto.EstadoUF.Remove(estado);
                contexto.SaveChanges();

            }


        }
        public EstadosUF BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.EstadoUF.Find(id);
            }
        }
        public void Atualiza(EstadosUF estado)
        {
            using (var contexto = new PontoContex())
            {

                contexto.EstadoUF.Update(estado);
                contexto.SaveChanges();
            }
        }
    }

}