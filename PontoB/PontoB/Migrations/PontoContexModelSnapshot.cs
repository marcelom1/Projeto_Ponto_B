﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoB;

namespace PontoB.Migrations
{
    [DbContext(typeof(PontoContex))]
    partial class PontoContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.MinhaSequencia", "'MinhaSequencia', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PontoB.Models.Ausencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("Ausencia");
                });

            modelBuilder.Entity("PontoB.Models.AusenciaColaboradores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AusenciaId");

                    b.Property<int>("Id");

                    b.Property<DateTime?>("DataFim")
                        .IsRequired();

                    b.Property<DateTime?>("DataInicio")
                        .IsRequired();

                    b.Property<int>("HoraFim");

                    b.Property<int>("HoraInicio");

                    b.Property<int>("MinutoFim");

                    b.Property<int>("MinutoInicio");

                    b.Property<int>("MotivoAusenciaId");

                    b.Property<string>("Observacao");

                    b.HasKey("Id");

                    b.HasIndex("AusenciaId");

                    b.HasIndex("Id");

                    b.HasIndex("MotivoAusenciaId");

                    b.ToTable("AusenciaColaboradores");
                });

            modelBuilder.Entity("PontoB.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .IsRequired();

                    b.Property<string>("Cargo");

                    b.Property<DateTime?>("DataAdmissao")
                        .IsRequired();

                    b.Property<DateTime?>("DataDemissao");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EmpresaId");

                    b.Property<int?>("EnderecoColaboradorId");

                    b.Property<int>("EscalaId");

                    b.Property<bool>("Master");

                    b.Property<string>("NomeCompleto")
                        .IsRequired();

                    b.Property<string>("Pis")
                        .IsRequired();

                    b.Property<string>("RG");

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EnderecoColaboradorId");

                    b.HasIndex("EscalaId");

                    b.ToTable("Colaborador");
                });

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

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.HasIndex("EnderecoEmpresaId");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("PontoB.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<int?>("EstadoId");

                    b.Property<string>("Logradouro");

                    b.Property<int>("Numero");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("PontoB.Models.Escala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Escala");
                });

            modelBuilder.Entity("PontoB.Models.EscalaHorario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaSemana")
                        .IsRequired();

                    b.Property<int>("EntradaHora");

                    b.Property<int>("EntradaMinuto");

                    b.Property<int>("EscalaId");

                    b.Property<int>("SaidaHora");

                    b.Property<int>("SaidaMinuto");

                    b.Property<int>("TotalEmMinutos");

                    b.HasKey("Id");

                    b.HasIndex("EscalaId");

                    b.ToTable("EscalaHorario");
                });

            modelBuilder.Entity("PontoB.Models.EstadosUF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.ToTable("EstadoUF");
                });

            modelBuilder.Entity("PontoB.Models.ManutencaoPonto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<DateTime>("Data");

                    b.Property<int>("SaldoEmMinutos");

                    b.Property<int>("TotalPrevistoEmMinutos");

                    b.Property<int>("TotalRealizadoEmMinutos");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ManutencaoPonto");
                });

            modelBuilder.Entity("PontoB.Models.MotivoAusencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Abonar");

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("MotivoAusencia");
                });

            modelBuilder.Entity("PontoB.Models.OcorrenciaDia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoOcorrencia");

                    b.Property<int>("Id");

                    b.Property<DateTime>("Date");

                    b.Property<int>("QtdMinutos");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Date", "Id", "CodigoOcorrencia");

                    b.ToTable("OcorrenciaDia");
                });

            modelBuilder.Entity("PontoB.Models.RegistroPontoModels.RegistroPonto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<DateTime>("DataRegistro");

                    b.Property<bool>("DesconsiderarMarcacao");

                    b.Property<int>("HoraRegistro");

                    b.Property<int>("MinutoRegistro");

                    b.Property<string>("Observacao");

                    b.Property<bool>("RegistroManual");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("RegistroPonto");
                });

            modelBuilder.Entity("PontoB.Models.AusenciaColaboradores", b =>
                {
                    b.HasOne("PontoB.Models.Ausencia", "Ausencia")
                        .WithMany("AusenciaColaboradores")
                        .HasForeignKey("AusenciaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PontoB.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PontoB.Models.MotivoAusencia", "MotivoAusencia")
                        .WithMany("AusenciaColaboradores")
                        .HasForeignKey("MotivoAusenciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PontoB.Models.Colaborador", b =>
                {
                    b.HasOne("PontoB.Models.Empresa", "Empresa")
                        .WithMany("Colaboradores")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PontoB.Models.Endereco", "EnderecoColaborador")
                        .WithMany()
                        .HasForeignKey("EnderecoColaboradorId");

                    b.HasOne("PontoB.Models.Escala", "Escala")
                        .WithMany("Colaboradores")
                        .HasForeignKey("EscalaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PontoB.Models.Empresa", b =>
                {
                    b.HasOne("PontoB.Models.Endereco", "EnderecoEmpresa")
                        .WithMany()
                        .HasForeignKey("EnderecoEmpresaId");
                });

            modelBuilder.Entity("PontoB.Models.Endereco", b =>
                {
                    b.HasOne("PontoB.Models.EstadosUF", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");
                });

            modelBuilder.Entity("PontoB.Models.EscalaHorario", b =>
                {
                    b.HasOne("PontoB.Models.Escala")
                        .WithMany("EscalasHorario")
                        .HasForeignKey("EscalaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PontoB.Models.ManutencaoPonto", b =>
                {
                    b.HasOne("PontoB.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PontoB.Models.OcorrenciaDia", b =>
                {
                    b.HasOne("PontoB.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PontoB.Models.RegistroPontoModels.RegistroPonto", b =>
                {
                    b.HasOne("PontoB.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
