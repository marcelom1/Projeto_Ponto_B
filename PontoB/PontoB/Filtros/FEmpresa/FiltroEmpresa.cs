using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.Filtros.FEmpresa
{
    public class FiltroEmpresa
    {
        public static IFiltro<Empresa> ObterFiltroColuna(string coluna)
        {
            if (coluna.Equals("CNPJ", StringComparison.OrdinalIgnoreCase))
                return new FiltroEmpresaCnpj();
            else if (coluna.Equals("Razão Social", StringComparison.OrdinalIgnoreCase))
                return new FiltroEmpresaRazaoSocial();
            else if (coluna.Equals("Nome Fantasia", StringComparison.OrdinalIgnoreCase))
                return new FiltroEmpresaNomeFantasia();
            else if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroEmpresaCodigo();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroEmpresaTodos();


            return null;

        }

    }
}