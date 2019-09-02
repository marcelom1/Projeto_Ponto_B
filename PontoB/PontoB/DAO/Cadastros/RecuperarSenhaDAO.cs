using Microsoft.EntityFrameworkCore;
using PontoB.Filtros.FColaborador;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO.Cadastros
{
    public class RecuperarSenhaDAO
    {
        public RecuperarSenha Adiciona(RecuperarSenha token)
        {

            using (var context = new PontoContex())
            {

                token.Colaborador = context.Colaborador.Find(token.ColaboradorId);
                token.Chave = Encrypt.Encrypt.getMD5Hash(token.Chave);

                context.RecuperarSenha.Add(token);
                context.SaveChanges();
            }
            return token;
        }
        
               

        public void ExcluirChave(RecuperarSenha token)
        {
            using (var contexto = new PontoContex())
            {


                contexto.RecuperarSenha.Remove(token);
                contexto.SaveChanges();

            }


        }
        public RecuperarSenha BuscarPorChave(string id, string email)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.RecuperarSenha.Include(e => e.Colaborador).Where(e => e.Chave == Encrypt.Encrypt.getMD5Hash(id) && e.Colaborador.Email.Equals(email) && e.Validade>DateTime.Now).FirstOrDefault();
            }
        }






    }
}