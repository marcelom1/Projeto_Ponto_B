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
            builder.Property(c => c.Id)
                   .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasOne(c => c.EnderecoColaborador);

            builder.HasOne(c => c.EscalaColaborador)
                .WithMany();

            builder.HasOne(c => c.EmpresaColaborador)
                .WithMany();

            builder.HasIndex(c => c.CPF)
                   .IsUnique();

            builder.ToTable("Colaborador");
        }
    }
}