using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO.Mapeamento
{
    public class EscalaMap : IEntityTypeConfiguration<Escala>

    {
        public void Configure(EntityTypeBuilder<Escala> builder)
        {
            builder.HasMany(p => p.EscalasHorario)
                  .WithOne();
        }
    }
}