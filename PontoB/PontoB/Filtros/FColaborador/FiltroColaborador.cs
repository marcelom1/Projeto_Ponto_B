﻿using PontoB.Models;
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
            if (coluna.Equals("CPF", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorCPF();
            else if (coluna.Equals("Empresa", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorEmpresa();
            else if (coluna.Equals("Nome Completo", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorNome();
            else if (coluna.Equals("Código", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorCodigo();
            else if (coluna.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorTodos();
            else if (coluna.Equals("ColaboradorAtivo", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorAtivoPorNome();
            else if (coluna.Equals("EmpresaId", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorEmpresaID();
            else if (coluna.Equals("FiltroColaboradorAtivoEntreDatas", StringComparison.OrdinalIgnoreCase))
                return new FiltroColaboradorAtivoEntreDatas();

            

            return null;

        }

    }
}