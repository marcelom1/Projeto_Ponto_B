using Microsoft.EntityFrameworkCore;
using PagedList;
using PontoB.Filtros.FColaborador;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PontoB.DAO
{
    public class ColaboradorDAO
    {
        public int Adiciona(Colaborador colaborador)
        {
            
            using (var context = new PontoContex())
            {

                colaborador.Escala = context.Escala.Find(colaborador.EscalaId);
                colaborador.Empresa = context.Empresa.Find(colaborador.EmpresaId);
                colaborador.EnderecoColaborador.Estado = context.EstadoUF.Find(colaborador.EnderecoColaborador.Estado.Id);
                colaborador.Senha = Encrypt.Encrypt.getMD5Hash(colaborador.Senha);

                context.Colaborador.Add(colaborador);
                context.SaveChanges();
            }
            return colaborador.Id;
        }
        public IList<Colaborador> Lista()
        {
            
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.Include(p=>p.Empresa).Include(p=>p.Escala).OrderBy(p => p.NomeCompleto).ToList();
            }
        }


        public IList<Colaborador> Filtro(string coluna, string filtro)
        {
            
            using (var contexto = new PontoContex())
            {
                var objFiltro = FiltroColaborador.ObterFiltroColuna(coluna);
               

                return objFiltro.Filtrar(contexto
                         .Colaborador
                         .Include(e=>e.Empresa)
                         .Include(e=>e.Escala)
                         .AsNoTracking(), filtro);

            }
        }



        public void ExcluirColaborador(Colaborador colaborador)
        {
            using (var contexto = new PontoContex())
            {


                contexto.Colaborador.Remove(colaborador);
                contexto.SaveChanges();

            }


        }
        public Colaborador BuscarPorId(int id)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.Include(e => e.EnderecoColaborador).Include(e=>e.EnderecoColaborador.Estado).Include(e=>e.Empresa).Include(e=>e.Escala).Include(e => e.EnderecoColaborador.Estado).Where(e => e.Id == id).FirstOrDefault();
            }
        }

        public Colaborador BuscarEmail(string email)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.Where(e => e.Email == email).FirstOrDefault();
            }
        }


        public void Atualiza(Colaborador colaborador)
        {
            using (var contexto = new PontoContex())
            {
                colaborador.Escala = contexto.Escala.Find(colaborador.EscalaId);
                colaborador.Empresa = contexto.Empresa.Find(colaborador.EmpresaId);
                colaborador.EnderecoColaborador.Estado = contexto.EstadoUF.Find(colaborador.EnderecoColaborador.Estado.Id);
                if (!string.IsNullOrEmpty(colaborador.Senha))
                    colaborador.Senha = Encrypt.Encrypt.getMD5Hash(colaborador.Senha);
                else
                    colaborador.Senha = BuscarPorId(colaborador.Id).Senha;

                contexto.Colaborador.Update(colaborador);
                contexto.SaveChanges();
            }
        }

        public Colaborador ConfirmacaoAutenticacao(string login, string senha)
        {
            using (var contexto = new PontoContex())
            {
                return contexto.Colaborador.Include(x=>x.EnderecoColaborador).Include(x=>x.EnderecoColaborador.Estado).FirstOrDefault(a => a.Email == login && a.Senha == Encrypt.Encrypt.getMD5Hash(senha));
            }
        }

    }
}