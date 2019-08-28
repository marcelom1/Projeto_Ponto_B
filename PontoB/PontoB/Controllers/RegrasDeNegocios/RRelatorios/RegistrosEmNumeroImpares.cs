using PontoB.Business.Utils;
using PontoB.DAO;
using PontoB.Models.ViewModels.VRelatorios.Manutencao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RRelatorios
{
    public class RegistrosEmNumeroImpares
    {
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        public List<ViewModelRelatorioRegistroImpares> RegistrosImpares(DateTime dataInicio, DateTime dataFim, int empresaId)
        {
            var tabela = new List<ViewModelRelatorioRegistroImpares>();

            var valores = new FiltroPeriodoValores
            {
                Inicio = dataInicio,
                Fim = dataFim,
                Id = empresaId
            };

            var registros = dbRegistroPonto.Filtro("FiltroRegistroPontoTodosRegistros", valores.ToString()).Where(x => x.Colaborador.DataAdmissao <= dataFim && (x.Colaborador.DataDemissao == null || x.Colaborador.DataDemissao >= dataInicio));


            foreach (var c in registros.Select(x => x.Colaborador).Distinct())
            {
                foreach (var data in registros.Where(x => x.ColaboradorId == c.Id).Select(x => x.DataRegistro.Date).Distinct())
                {

                    var lista = (registros.Where(x => x.ColaboradorId.Equals(c.Id) && x.DataRegistro.Date.Equals(data) && x.DesconsiderarMarcacao == false));

                    if (lista.Count() % 2 != 0)
                    {

                        tabela.Add(new ViewModelRelatorioRegistroImpares
                        {
                            Data = data,
                            Colaborador = c,
                            Registro = lista.ToList()
                        });

                    }
                }
            }

            return tabela;
        }
    }
}