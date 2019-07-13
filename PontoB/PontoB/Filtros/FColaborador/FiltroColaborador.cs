using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FColaborador
{
    public class FiltroColaborador
    {
        public static IFiltro<Colaborador> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("CNPJ", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorCPF();
            else if (coluna.Equals("Razão Social", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorEmpresa();
            else if (coluna.Equals("Nome Fantasia", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorNome();
            else if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorCodigo();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorTodos();


            return null;

        }

    }
}