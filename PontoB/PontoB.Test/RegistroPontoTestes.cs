using Xunit;
using PontoB.Models;
using PontoB.Controllers;
using System;
using PontoB.DAO;
using PontoB.Models.RegistroPontoModels;
using PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia;
using PontoB.Business.Utils;
using System.Linq;

namespace PontoB.Test
{
    public class OcorrenciasPontoTestes 
    {
        private static int OcorrenciaPrevistasId = 1;
        private static int OcorrenciaTrabalhadasId = 2;
        private static int OcorrenciaAusenciaAbonaId = 3;
        private static int OcorrenciaAusenciaDescontaId = 4;
        private static int OcorrenciaHorasExedentesId = 5;
        private static int OcorrenciaHorasFaltasId = 6;

        [Fact]
        public void HorasPrevistas()
        {
            
            var escala = new Escala {
                Descricao = "Escala Teste",
            };
            var EscalaDao = new EscalaDAO();
            escala.Id = EscalaDao.Adiciona(escala);
            
            var escalaHorario = new EscalaHorario {
                DiaSemana = "Segunda",
                EntradaHora = 8,
                EntradaMinuto = 0,
                EscalaId = escala.Id,
                SaidaHora = 12,
                SaidaMinuto = 0,
                TotalEmMinutos = (12 - 8) * 60 + 0 + 0
            };

            var EscalaHorarioDao = new EscalaHorarioDAO();
            escalaHorario.Id = EscalaHorarioDao.Adiciona(escalaHorario);


            var empresa = new Empresa {
                Cnpj = "11.352.722/0001-21",
                RazaoSocial = "Empresa Teste",
            };

            var empresaDao = new EmpresaDAO();
            empresa.Id = empresaDao.Adiciona(empresa);

            var colaborador = new Colaborador {
                CPF = "637.955.130-14",
                NomeCompleto = "Colaborador Teste",
                Email = "teste@colaborador.com",
                DataAdmissao = new DateTime(2019,1,10,0,0,0),
                Pis = "120.8827.603-5",
                EscalaId = escala.Id,
                EmpresaId = empresa.Id,
                Senha = "123"
            };
            var colaboradorDao = new ColaboradorDAO();
            colaborador.Id = colaboradorDao.Adiciona(colaborador);

            var dataInicio = new DateTime(2019, 1, 11, 0, 0, 0);
            var dataFim = new DateTime(2019, 1, 10, 0, 0, 0);

            var registroDao = new RegistroPontoDAO();

            for (DateTime data = dataInicio.Date; data <= dataFim.Date; data = data.AddDays(1))
            {
                var hora = 8;
                var minuto = 0;
                for (int i = 0; i < 4; i++) {

                    

                    var registro1 = new RegistroPonto
                    {
                        Colaborador = colaborador,
                        ColaboradorId = colaborador.Id,
                        DataRegistro = data.AddHours(hora).AddMinutes(minuto),
                        DesconsiderarMarcacao = false,
                        HoraRegistro = hora,
                        MinutoRegistro = minuto,
                        RegistroManual = false,
                        Observacao = "TesteAutomatizado"
                    
                    };
                    hora += 4;
                    minuto += 5;

                }
             
            }

            new RegrasOcorrenciaDia().CalculoPonto(colaborador.Id,dataInicio,dataFim);
            var dbOcorrenciaDia = new OcorrenciaDiaDAO();


            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = colaborador.Id

            };
            var ListaOcorrencias = dbOcorrenciaDia.Filtro("OcorrenciaEntreDatas", valores.ToString()).Where(x=>x.CodigoOcorrencia.Equals(OcorrenciaPrevistasId));

            var resultado = ListaOcorrencias.Sum(x => x.QtdMinutos);
            var resultadoEsperado = 100;

            empresaDao.ExcluirEmpresa(empresa);
            EscalaDao.ExcluirEscala(escala);

            Assert.Equal(resultadoEsperado, resultado);

        }

    }
}
