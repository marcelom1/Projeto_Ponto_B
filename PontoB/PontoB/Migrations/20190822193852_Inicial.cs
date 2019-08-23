using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoB.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia");

            migrationBuilder.CreateTable(
                name: "Ausencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ausencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escala",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoUF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sigla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoUF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivoAusencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Abonar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoAusencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscalaHorario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EscalaId = table.Column<int>(nullable: false),
                    DiaSemana = table.Column<string>(nullable: false),
                    EntradaHora = table.Column<int>(nullable: false),
                    EntradaMinuto = table.Column<int>(nullable: false),
                    SaidaHora = table.Column<int>(nullable: false),
                    SaidaMinuto = table.Column<int>(nullable: false),
                    TotalEmMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalaHorario_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CEP = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true),
                    Cidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_EstadoUF_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoUF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    RazaoSocial = table.Column<string>(nullable: false),
                    Cnpj = table.Column<string>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: true),
                    EnderecoEmpresaId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresa_Endereco_EnderecoEmpresaId",
                        column: x => x.EnderecoEmpresaId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    RG = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    DataAdmissao = table.Column<DateTime>(nullable: false),
                    DataDemissao = table.Column<DateTime>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    Pis = table.Column<string>(nullable: false),
                    EnderecoColaboradorId = table.Column<int>(nullable: true),
                    EscalaId = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<int>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Master = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaborador_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Colaborador_Endereco_EnderecoColaboradorId",
                        column: x => x.EnderecoColaboradorId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Colaborador_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AusenciaColaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<int>(nullable: false),
                    MinutoInicio = table.Column<int>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    HoraFim = table.Column<int>(nullable: false),
                    MinutoFim = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    ColaboradorId = table.Column<int>(nullable: false),
                    MotivoAusenciaId = table.Column<int>(nullable: false),
                    AusenciaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AusenciaColaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_Ausencia_AusenciaId",
                        column: x => x.AusenciaId,
                        principalTable: "Ausencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaColaboradores_MotivoAusencia_MotivoAusenciaId",
                        column: x => x.MotivoAusenciaId,
                        principalTable: "MotivoAusencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManutencaoPonto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    ColaboradorId = table.Column<int>(nullable: false),
                    TotalPrevistoEmMinutos = table.Column<int>(nullable: false),
                    TotalRealizadoEmMinutos = table.Column<int>(nullable: false),
                    SaldoEmMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoPonto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoPonto_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcorrenciaDia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ColaboradorId = table.Column<int>(nullable: false),
                    CodigoOcorrencia = table.Column<int>(nullable: false),
                    QtdMinutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaDia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcorrenciaDia_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pontuacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColaboradorId = table.Column<int>(nullable: false),
                    Ponto = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontuacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontuacao_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroPonto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColaboradorId = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false),
                    HoraRegistro = table.Column<int>(nullable: false),
                    MinutoRegistro = table.Column<int>(nullable: false),
                    RegistroManual = table.Column<bool>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    DesconsiderarMarcacao = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroPonto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroPonto_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_AusenciaId",
                table: "AusenciaColaboradores",
                column: "AusenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_ColaboradorId",
                table: "AusenciaColaboradores",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaColaboradores_MotivoAusenciaId",
                table: "AusenciaColaboradores",
                column: "MotivoAusenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_Email",
                table: "Colaborador",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EmpresaId",
                table: "Colaborador",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EnderecoColaboradorId",
                table: "Colaborador",
                column: "EnderecoColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_EscalaId",
                table: "Colaborador",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Cnpj",
                table: "Empresa",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_EnderecoEmpresaId",
                table: "Empresa",
                column: "EnderecoEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstadoId",
                table: "Endereco",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaHorario_EscalaId",
                table: "EscalaHorario",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoPonto_ColaboradorId",
                table: "ManutencaoPonto",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaDia_ColaboradorId",
                table: "OcorrenciaDia",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaDia_Date_ColaboradorId_CodigoOcorrencia",
                table: "OcorrenciaDia",
                columns: new[] { "Date", "ColaboradorId", "CodigoOcorrencia" });

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacao_ColaboradorId",
                table: "Pontuacao",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroPonto_ColaboradorId",
                table: "RegistroPonto",
                column: "ColaboradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AusenciaColaboradores");

            migrationBuilder.DropTable(
                name: "EscalaHorario");

            migrationBuilder.DropTable(
                name: "ManutencaoPonto");

            migrationBuilder.DropTable(
                name: "OcorrenciaDia");

            migrationBuilder.DropTable(
                name: "Pontuacao");

            migrationBuilder.DropTable(
                name: "RegistroPonto");

            migrationBuilder.DropTable(
                name: "Ausencia");

            migrationBuilder.DropTable(
                name: "MotivoAusencia");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Escala");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "EstadoUF");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
