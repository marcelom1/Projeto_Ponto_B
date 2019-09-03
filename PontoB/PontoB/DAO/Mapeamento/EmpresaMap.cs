using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace PontoB.DAO.Config
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(c => c.Id)
                   .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

           

            builder.HasIndex(p => p.Cnpj)
                   .IsUnique();

            builder.ToTable("Empresa");
        }
    }
}