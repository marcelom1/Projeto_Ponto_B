using Microsoft.EntityFrameworkCore;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO
{
    public class EscalaHorarioDAO
    {
        public IList<EscalaHorario> Lista(int IdEscala)
        {
            using (var contexto = new PontoContex())
            {

                return contexto.EscalaHorario.Where(p => p.EscalaId.Equals(IdEscala)).ToList();



            }
        }
    }
}