using PontoB.DAO;
using PontoB.Models;
using PontoB.Models.ViewModels.VAusencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Controllers.RegrasDeNegocios.RAusencia
{
    public class RegrasAusencia
    {

        private AusenciaDAO dbAusencia = new AusenciaDAO();
        private MotivoAusenciaDAO dbMotivoAusencia = new MotivoAusenciaDAO();
        private AusenciaColaboradoresDAO dbAusenciaColaborador = new AusenciaColaboradoresDAO();
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private EmpresaDAO dbEmpresa = new EmpresaDAO();
        public AusenciaViewModels ValidacaoRegraNegocio(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim, bool TodosColaboradores, bool TodasEmpresas, AusenciaViewModels model)
        {
            if (!DataInicioMaiorDataFinal(ausenciaColaboradores.DataInicio, HoraInicio.Hour, HoraInicio.Minute, ausenciaColaboradores.DataFim, HoraFim.Hour, HoraFim.Minute))
            {
                if (TodosColaboradores)
                {
                    if (TodasEmpresas)
                    {
                        AdicionarEmTodosOsColaboradoresDeTodasAsEmpresas(ausenciaColaboradores);
                        model = new AusenciaViewModels();
                    }
                    else
                    {
                        AdicionarEmTodosColaboradoresEmUmaDeterminadaEmpresa(ausenciaColaboradores, model.Empresa);
                        model = new AusenciaViewModels();
                    }
                }
                else
                {
                   
                    AdicionarAusenciaEmUmColaborador(ausenciaColaboradores);
                    model = new AusenciaViewModels();
                   
                }

            }
            else
                throw new System.ArgumentException("Data e hora inicial, não pode ser maior que Data e hora final!", "HoraInicio");
            return model;
        }
        public bool ConflitoAusenciaDemissao(Colaborador colaborador, AusenciaColaboradores ausenciaColaborador)
        {
            if (colaborador.DataDemissao >= ausenciaColaborador.DataFim && (colaborador.DataDemissao >= ausenciaColaborador.DataFim) || colaborador.DataDemissao == null)
                return false;
            return true;
        }

        public AusenciaColaboradores MontarAusenciaColaboradores(AusenciaColaboradores ausenciaColaboradores, DateTime HoraInicio, DateTime HoraFim)
        {
            ausenciaColaboradores.HoraInicio = HoraInicio.Hour;
            ausenciaColaboradores.MinutoInicio = HoraInicio.Minute;
            ausenciaColaboradores.HoraFim = HoraFim.Hour;
            ausenciaColaboradores.MinutoFim = HoraFim.Minute;
            ausenciaColaboradores.DataInicio = ausenciaColaboradores.DataInicio.GetValueOrDefault().AddHours(HoraInicio.Hour).AddMinutes(HoraInicio.Minute);
            ausenciaColaboradores.DataFim = ausenciaColaboradores.DataFim.GetValueOrDefault().AddHours(HoraFim.Hour).AddMinutes(HoraFim.Minute);
            ausenciaColaboradores.Ausencia = dbAusencia.BuscarPorId(ausenciaColaboradores.AusenciaId);
            ausenciaColaboradores.Colaborador = dbColaborador.BuscarPorId(ausenciaColaboradores.ColaboradorId);
            ausenciaColaboradores.MotivoAusencia = dbMotivoAusencia.BuscarPorId(ausenciaColaboradores.MotivoAusenciaId);
            ausenciaColaboradores.Colaborador = ausenciaColaboradores.Colaborador ?? new Colaborador();

            return ausenciaColaboradores;
        }

        public bool DataInicioMaiorDataFinal(DateTime? dataInicio, int horaInicio, int minutoInicio, DateTime? dataFim, int horaFim, int minutoFim)
        {
            dataInicio.GetValueOrDefault().AddHours(horaInicio).AddMinutes(minutoInicio);
            dataFim.GetValueOrDefault().AddHours(horaFim).AddMinutes(minutoFim);
            if (dataInicio > dataFim)
                return true;

            return false;
        }

        public void AdicionarEmTodosOsColaboradoresDeTodasAsEmpresas(AusenciaColaboradores ausenciaColaboradores)
        {
            foreach (var colaborador in dbColaborador.Lista())
            {
                ausenciaColaboradores.Id = 0;
                ausenciaColaboradores.ColaboradorId = colaborador.Id;
                if (!ConflitoAusenciaDemissao(colaborador, ausenciaColaboradores))
                    dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
            }
        }

        public void AdicionarEmTodosColaboradoresEmUmaDeterminadaEmpresa(AusenciaColaboradores ausenciaColaboradores, Empresa empresa)
        {
            foreach (var colaborador in dbColaborador.Filtro("EmpresaId", empresa.Id.ToString()))
            {
                ausenciaColaboradores.Id = 0;
                ausenciaColaboradores.ColaboradorId = colaborador.Id;
                if (!ConflitoAusenciaDemissao(colaborador, ausenciaColaboradores))
                    dbAusenciaColaborador.Adiciona(ausenciaColaboradores);
            }
        }

        public void AdicionarAusenciaEmUmColaborador(AusenciaColaboradores ausenciaColaboradores)
        {
            Colaborador buscaColaborador = (dbColaborador.BuscarPorId(ausenciaColaboradores.ColaboradorId));
            if (!ConflitoAusenciaDemissao(buscaColaborador, ausenciaColaboradores))
            {
                dbAusenciaColaborador.Adiciona(ausenciaColaboradores);

            }
            else
                throw new System.ArgumentException("Data de lançamento em conflito com a data de demissão do colaborador", "ausenciaColaboradores.DataInicio");
                
        }

        public int ExcluirAusencia(int AusenciaColaboradorId)
        {
            //Antes de excluir é feito a verificação se o horario existe 
            var pesquisa = dbAusenciaColaborador.BuscarPorId(AusenciaColaboradorId);
            var IdAusenciaColaborador = pesquisa.AusenciaId;

            //Caso encontre algo, exluir o registro
            if (pesquisa != null)
                dbAusenciaColaborador.ExcluirAusenciaColaboradores(pesquisa);
            return IdAusenciaColaborador;
        }

    }
}