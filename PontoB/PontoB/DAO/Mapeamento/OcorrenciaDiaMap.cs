using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO.Mapeamento
{
    
    public class OcorrenciaDiaMap : IEntityTypeConfiguration<OcorrenciaDia>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaDia> builder)
        {
            builder.HasIndex(x=>new {x.Date, x.ColaboradorId, x.CodigoOcorrencia });
        }
    }
    
}