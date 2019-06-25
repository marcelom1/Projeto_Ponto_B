using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PontoB;

namespace PontoB.Migrations
{
    [DbContext(typeof(PontoContex))]
    [Migration("20190625182721_Empresa")]
    partial class Empresa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PontoB.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<int>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Cnpj")
                        .IsRequired();

                    b.Property<string>("Complemento");

                    b.Property<string>("Email");

                    b.Property<string>("Estado");

                    b.Property<string>("Logradouro");

                    b.Property<string>("NomeFantasia");

                    b.Property<int>("Numero");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });
        }
    }
}
