﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoB;

namespace PontoB.Migrations
{
    [DbContext(typeof(PontoContex))]
    [Migration("20190628192359_EnderecoEmpresa")]
    partial class EnderecoEmpresa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.MinhaSequencia", "'MinhaSequencia', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PontoB.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

                    b.Property<string>("Cnpj")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int?>("EnderecoEmpresaId");

                    b.Property<string>("NomeFantasia");

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoEmpresaId");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("PontoB.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro");

                    b.Property<int>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Estado");

                    b.Property<string>("Logradouro");

                    b.Property<int>("Numero");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("PontoB.Models.Empresa", b =>
                {
                    b.HasOne("PontoB.Models.Endereco", "EnderecoEmpresa")
                        .WithMany()
                        .HasForeignKey("EnderecoEmpresaId");
                });
#pragma warning restore 612, 618
        }
    }
}
