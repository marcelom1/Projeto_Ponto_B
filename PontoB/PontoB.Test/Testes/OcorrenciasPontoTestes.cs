using Xunit;
using PontoB.Models;
using PontoB.DAO;
using PontoB.Models.RegistroPontoModels;
using PontoB.Controllers.RegrasDeNegocios.ROcorrenciaDia;
using PontoB.Business.Utils;
using System.Linq;
using System.Collections.Generic;
using PontoB.Test.Cadastro;

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

        private static EscalaDAO dbEscala = new EscalaDAO();
        private static EmpresaDAO dbEmpresa = new EmpresaDAO();
        private static AusenciaDAO dbAusencia = new AusenciaDAO();
        



        [Theory]
        [ClassData(typeof(CenarioOcorrencia))]
        public void VerificandoOsValoresCalculadosAoCalcularAsOcorrenciasDias(int[] resultadoEsperado,
                                                                              FiltroPeriodoValores dataPeriodo,
                                                                              IList<EscalaHorario> escala,
                                                                              Colaborador colaborador,
                                                                              Empresa empresa,
                                                                              IList<AusenciaColaboradores> ausencia,
                                                                              IList<RegistroPonto>[] registros)
        {

            //Arranje - Cenário
            colaborador = new CenarioBD().Salvar(colaborador, escala, empresa, registros,ausencia);
            var dataInicio = dataPeriodo.Inicio;
            var dataFim = dataPeriodo.Fim;


            //ACT - metodo sob teste
            new RegrasOcorrenciaDia().CalculoPonto(colaborador.Id, dataInicio.Value, dataFim.Value);
            var dbOcorrenciaDia = new OcorrenciaDiaDAO();


            //Assert
            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = colaborador.Id

            };
            var ListaOcorrencias = dbOcorrenciaDia.Filtro("OcorrenciaEntreDatas", valores.ToString());
            if (ausencia.Count() > 0)
                dbAusencia.ExcluirAusencia(ausencia.First().Ausencia);
            dbEmpresa.ExcluirEmpresa(colaborador.Empresa);
            dbEscala.ExcluirEscala(colaborador.Escala);
            
                                

            var ResultadoMinutosPrevistos = ListaOcorrencias.Where(x => x.CodigoOcorrencia.Equals(OcorrenciaPrevistasId)).Sum(x => x.QtdMinutos);
            var EsperadoMinutosPrevistos = resultadoEsperado[0];

            var ResultadoMinutosTrabalhados = ListaOcorrencias.Where(x => x.CodigoOcorrencia.Equals(OcorrenciaTrabalhadasId)).Sum(x => x.QtdMinutos);
            var EsperadoMinutosTrabalhados = resultadoEsperado[1];

            var ResultadoMinutosAusencia = ListaOcorrencias.Where(x => x.CodigoOcorrencia.Equals(OcorrenciaAusenciaAbonaId)).Sum(x => x.QtdMinutos);
            var EsperadoMinutosAusencia = resultadoEsperado[2];

            var ResultadoMinutosExcedentes = ListaOcorrencias.Where(x => x.CodigoOcorrencia.Equals(OcorrenciaHorasExedentesId)).Sum(x => x.QtdMinutos);
            var EsperadoMinutosExcedentes = resultadoEsperado[3];

            var ResultadoMinutosFaltas = ListaOcorrencias.Where(x => x.CodigoOcorrencia.Equals(OcorrenciaHorasFaltasId)).Sum(x => x.QtdMinutos);
            var EsperadoMinutosFaltas = resultadoEsperado[4];


            Assert.Equal(EsperadoMinutosPrevistos, ResultadoMinutosPrevistos);
            Assert.Equal(EsperadoMinutosTrabalhados, ResultadoMinutosTrabalhados);
            Assert.Equal(EsperadoMinutosExcedentes, ResultadoMinutosExcedentes);
            Assert.Equal(EsperadoMinutosFaltas, ResultadoMinutosFaltas);
        }

        
    }
}
