using DataHistoricalRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHistoricalRepository
{
    public class DataHistoContext : DbContext
    {

        public DataHistoContext() : base("HistoricalDataProd")
        {

        }

        public DbSet<HistoricalData> Datas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<HistoricalData>().Property(p => p.Id).HasColumnType("bigint");
            modelBuilder.Entity<HistoricalData>().Property(f => f.Open).HasPrecision(18, 7);
            modelBuilder.Entity<HistoricalData>().Property(f => f.High).HasPrecision(18, 7);
            modelBuilder.Entity<HistoricalData>().Property(f => f.Low).HasPrecision(18, 7);
            modelBuilder.Entity<HistoricalData>().Property(f => f.Close).HasPrecision(18, 7);
        }



    }
}
