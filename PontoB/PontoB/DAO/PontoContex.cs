using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PontoB.Controllers;
using PontoB.DAO;
using PontoB.DAO.Config;
using PontoB.DAO.Mapeamento;
using PontoB.Models;


namespace PontoB
{
    public class PontoContex : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<EstadosUF> EstadoUF { get; set; }
        public DbSet<Escala> Escala { get; set; }
        public DbSet<EscalaHorario> EscalaHorario { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PontoDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new EscalaMap());

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }
    }
}