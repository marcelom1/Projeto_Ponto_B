using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public class CenarioBD
    {
        private static EscalaDAO dbEscala = new EscalaDAO();
        private static EmpresaDAO dbEmpresa = new EmpresaDAO();
        private static ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private static RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        private static AusenciaDAO dbAusencia = new AusenciaDAO();
        private static AusenciaColaboradoresDAO dbAusenciaColaboradores = new AusenciaColaboradoresDAO();
        private static EscalaHorarioDAO dbEscalaHorario = new EscalaHorarioDAO();



        public Colaborador Salvar(Colaborador colaborador, IList<EscalaHorario> escalaHorario, Empresa empresa, IList<RegistroPonto>[] registros, IList<AusenciaColaboradores> ausencias)
        {
            Escala escala = EscalaDB(escalaHorario);

            empresa.Id = dbEmpresa.Adiciona(empresa);
            colaborador = ColaboradorDB(colaborador, empresa, escala);

            RegistrosDB(colaborador, registros);

            AusenciaDB(ausencias,colaborador);

            return colaborador;
        }

        private static void AusenciaDB(IList<AusenciaColaboradores> ausencias,Colaborador colaborador)
        {
            if (ausencias.Count > 0)
            {
                var ausencia = new Ausencia
                {
                    Descricao = "Ausencia Teste"
                };
                ausencia.Id = dbAusencia.Adiciona(ausencia);

                foreach (var ausencia2 in ausencias)
                {
                    ausencia2.Id = 0;
                    ausencia2.AusenciaId = ausencia.Id;
                    ausencia2.Ausencia = ausencia;
                    ausencia2.ColaboradorId = colaborador.Id;
                    ausencia2.Colaborador = colaborador;

                    dbAusenciaColaboradores.Adiciona(ausencia2);
                }
            }
        }

        private static void RegistrosDB(Colaborador colaborador, IList<RegistroPonto>[] registros)
        {
            foreach (var registro in registros)
            {
                foreach (var registro2 in registro)
                {
                    registro2.Id = 0;
                    registro2.ColaboradorId = colaborador.Id;
                    registro2.Colaborador = colaborador;

                    dbRegistroPonto.Adiciona(registro2);
                }

            }
        }

        private static Colaborador ColaboradorDB(Colaborador colaborador, Empresa empresa, Escala escala)
        {
            colaborador.EscalaId = escala.Id;
            colaborador.Escala = escala;
            colaborador.EmpresaId = empresa.Id;
            colaborador.Empresa = empresa;

            colaborador = dbColaborador.Adiciona(colaborador);
            return colaborador;
        }

        private static Escala EscalaDB(IList<EscalaHorario> escalaHorario)
        {
            var escala = new Escala
            {
                Descricao = "EscalaTeste"
            };
            escala.Id = dbEscala.Adiciona(escala);

            foreach (var horario in escalaHorario)
            {
                horario.EscalaId = escala.Id;
                dbEscalaHorario.Adiciona(horario);
            }

            return escala;
        }
    }
}
