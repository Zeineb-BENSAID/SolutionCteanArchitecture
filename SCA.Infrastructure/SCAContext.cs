using Microsoft.EntityFrameworkCore;
using SCA.ApplicationCore.Domain;
using SCA.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Infrastructure
{
    public class SCAContext:DbContext
    {
        //DbSet
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
              Initial Catalog=CleanSolutionDB;Integrated Security=true;
              MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies(true);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired();
            
            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(250);
            
            base.ConfigureConventions(configurationBuilder);
        }

    }
}
