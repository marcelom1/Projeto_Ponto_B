using PontoB.Business.Utils;
using PontoB.Models;
using PontoB.Models.RegistroPontoModels;
using PontoB.Test.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PontoB.Test.Cadastro
{
    public class CenarioOcorrencia : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        
        public IEnumerator<object[]> GetEnumerator()
        {
            _data.Add(new object[]{new int[]{ 1530 , 2575, 0 ,1045, 0 },
                                new FiltroPeriodoValores {Inicio = new DateTime(2019, 1, 11, 0, 0, 0), Fim = new DateTime(2019, 1, 15, 0, 0, 0) },
                                new EscalaTeste().EscalaPadraoSegundaSextaGeral(),
                                new ColaboradorTeste().CriarColaboradorPadrao(new DateTime(2019,1,1,0,0,0),null),
                                new EmpresaTeste().CriarEmpresaPadrao(),
                                new List<AusenciaColaboradores>(),
                                new IList<RegistroPonto>[]{new RegistrosTeste().CriarRegistrosIntervalo(new DateTime(2019, 1, 11, 8, 0, 0), new DateTime(2019, 1, 15, 12, 0, 0)),
                                                           new RegistrosTeste().CriarRegistrosIntervalo(new DateTime(2019, 1, 11, 13, 0, 0), new DateTime(2019, 1, 15, 17, 35, 0))
                                }
            });
            _data.Add(new object[]{new int[]{ 1020 , 515, 510, 5, 0 },
                                new FiltroPeriodoValores {Inicio = new DateTime(2019, 1, 11, 0, 0, 0), Fim = new DateTime(2019, 1, 15, 0, 0, 0) },
                                new EscalaTeste().EscalaPadraoSegundaSextaGeral(),
                                new ColaboradorTeste().CriarColaboradorPadrao(new DateTime(2019,1,1,0,0,0),new DateTime(2019,1,14,0,0,0)),
                                new EmpresaTeste().CriarEmpresaPadrao(),
                                new List<AusenciaColaboradores>{new AusenciaTeste().CriarAusencia(new FiltroPeriodoValores {Inicio = new DateTime(2019, 1, 11, 0, 0, 0), Fim = new DateTime(2019, 1, 11, 23, 59, 59) },1) },
                                new IList<RegistroPonto>[]{new RegistrosTeste().CriarRegistrosIntervalo(new DateTime(2019, 1, 14, 8, 0, 0), new DateTime(2019, 1, 14, 12, 0, 0)),
                                                           new RegistrosTeste().CriarRegistrosIntervalo(new DateTime(2019, 1, 14, 13, 0, 0), new DateTime(2019, 1, 14, 17, 35, 0))
                                }
            });


            return _data.GetEnumerator();


        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
