using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoB.DAO.Mapeamento
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            

            builder.Property(c=>c.Senha)
                .IsRequired();

            builder.HasIndex(c => c.Email)
                   .IsUnique();

            builder.ToTable("Colaborador");
        }
    }
}