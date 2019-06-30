using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PontoB.Controllers;
using PontoB.DAO;
using PontoB.DAO.Config;
using PontoB.Models;


namespace PontoB
{
    public class PontoContex : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ControlePontoDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpresaMap());

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }
    }
}