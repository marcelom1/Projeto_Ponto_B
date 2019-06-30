
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoB.Models;
using Empresa = PontoB.Models.Empresa;
using Microsoft.EntityFrameworkCore;

namespace PontoB.DAO
{
    public class EnderecoDAO
    {
        public void Adiciona(Endereco endereco)
        {
            using (var context = new PontoContex())
            {
                context.Endereco.Add(endereco);
                context.SaveChanges();
            }
        }
        public IList<Endereco> Lista()
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Endereco.ToList();
            }
        }

        public void ExcluirEndereco(Endereco endereco)
        {
            using (var contexto = new PontoContex())
            {
                contexto.Endereco.Remove(endereco);
                contexto.SaveChanges();
            }

        }
        public Endereco BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Endereco.Find(id);
            }
        }
        public void Atualiza(Endereco empresa)
        {
            using (var contexto = new PontoContex())
            {
                // contexto.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
                //contexto.SaveChanges();
            }
        }
    }
}