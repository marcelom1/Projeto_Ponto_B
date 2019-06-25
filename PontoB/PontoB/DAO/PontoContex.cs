using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PontoB.Controllers;
using PontoB.Models;

namespace PontoB
{
    public class PontoContex : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ControlePontoDB;Trusted_Connection=true;");
        }
    }
}