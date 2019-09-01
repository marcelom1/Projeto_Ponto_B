using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public class RegistrosTeste
    {
        private static RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        public  IList<RegistroPonto> CriarRegistrosIntervalo(DateTime dataInicio1, DateTime dataFim1)
        {

            var lista = new List<RegistroPonto>();

            for (DateTime data = dataInicio1.Date; data <= dataFim1.Date; data = data.AddDays(1))
            {


                var registro = new RegistroPonto
                {
                    
                    ColaboradorId = 0,
                    DataRegistro = data.AddHours(dataInicio1.Hour).AddMinutes(dataInicio1.Minute),
                    DesconsiderarMarcacao = false,
                    HoraRegistro = dataInicio1.Hour,
                    MinutoRegistro = dataInicio1.Minute,
                    RegistroManual = false,
                    Observacao = "TesteAutomatizado"

                };

                lista.Add(registro);

                var registro2 = new RegistroPonto
                {

                    ColaboradorId = 0,
                    DataRegistro = data.AddHours(dataFim1.Hour).AddMinutes(dataFim1.Minute),
                    DesconsiderarMarcacao = false,
                    HoraRegistro = dataFim1.Hour,
                    MinutoRegistro = dataFim1.Minute,
                    RegistroManual = false,
                    Observacao = "TesteAutomatizado"

                };

                
                lista.Add(registro2);



            }

            return lista;
        }
    }
}
