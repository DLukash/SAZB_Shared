using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using System.IO;
using System.Data.Common;
using System.Data;

#if WINDOWS_UWP
using SAZB_shared.UWP;
#endif

#if __ANDROID__
using SAZB_shared.Droid;
#endif

namespace SAZB_shared
{

    public class Offline_DB_Context : DbContext
    {
        public DbSet<Field> Fields { get; set; }
        public DbSet<Landplot> Landplots { get; set; }
        public DbSet<Rent> Rents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var db_path = Path.Combine(DependencyService.Get<GetGeoPackageePathService>().GetGeoPackageePath(), "offline_db.db");
            optionsBuilder.UseSqlite(String.Format("Data Source={0}", db_path));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>()
                .HasKey(k => k.index);

            modelBuilder.Entity<Field>()
                .Property(p => p.ObjectID)
                .HasColumnName("T1.dbo.KernelForecast.ObjectID");

            modelBuilder.Entity<Field>()
                .Property(p => p.Name)
                .HasColumnName("T1.DBO.Fields_14.Name");

            modelBuilder.Entity<Field>()
                .Property(p => p.S)
                .HasColumnName("T1.DBO.Fields_14.S");

            modelBuilder.Entity<Field>()
                .Property(p => p.Agrotechnology)
                .HasColumnName("T1.dbo.KernelForecast.AgroTechnology");

            modelBuilder.Entity<Field>()
                .Property(p => p.Kluster)
                .HasColumnName("T1.dbo.KernelForecast.ClusterDescr");

            modelBuilder.Entity<Field>()
                .Property(p => p.District)
                .HasColumnName("T1.dbo.KernelForecast.District");

            modelBuilder.Entity<Field>()
                .Property(p => p.Region)
                .HasColumnName("T1.dbo.KernelForecast.Rayon");

            modelBuilder.Entity<Field>()
                .Property(p => p.Village_council)
                .HasColumnName("T1.dbo.KernelForecast.VillageGovernment");

            modelBuilder.Entity<Field>()
                .Property(p => p.CultName)
                .HasColumnName("T1.dbo.KernelForecast.CultName");

            modelBuilder.Entity<Field>()
                .Property(p => p.Hybrid)
                .HasColumnName("T1.dbo.KernelForecast.Hybrid");

            modelBuilder.Entity<Field>()
                .Property(p => p.Agronomist)
                .HasColumnName("T1.dbo.KernelForecast.LineAgronomist");

            modelBuilder.Entity<Field>()
                .Property(p => p.SuperAgronomist)
                .HasColumnName("T1.dbo.KernelForecast.SuperAgronomist");

            //Landplots

            modelBuilder.Entity<Landplot>()
                .HasKey(k => k.index);
            modelBuilder.Entity<Landplot>()
                .Property(p => p.ObjectID)
                .HasColumnName("OBJECTID");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.Kluster)
                .HasColumnName("Kluster_name");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.District)
                .HasColumnName("District");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.Region)
                .HasColumnName("Region");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.Village_council)
                .HasColumnName("Vilage_consil");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.Square)
                .HasColumnName("Square_PKK");
            modelBuilder.Entity<Landplot>()
                .Property(p => p.CadNumber)
                .HasColumnName("KN_dot");

            //Rent
            modelBuilder.Entity<Rent>()
            .HasKey(k => k.index);
            modelBuilder.Entity<Rent>()
                .Property(p => p.Rent_cash)
                .HasColumnName("Rent");


        }
    }

        public class Field
    {
        //[System.ComponentModel.DataAnnotations.Key]
        public int index { get; set; }

        //[Column("T1.dbo.KernelForecast.ObjectID")]
        public long ObjectID { get; set; }
        
        //[Column("T1.DBO.Fields_14.Name")]
        public string Name { get; set; }
        
        //[Column("T1.DBO.Fields_14.S")]
        public double S { get; set; }
        
        //[Column("T1.dbo.KernelForecast.AgroTechnology")]
        public string Agrotechnology { get; set; }
        
        //[Column("T1.dbo.KernelForecast.ClusterDescr")]
        public string Kluster { get; set; }
        
        //[Column("T1.dbo.KernelForecast.District")]
        public string District { get; set; }
        
        //[Column("T1.dbo.KernelForecast.Rayon")]
        public string Region { get; set; }
        
        //[Column("T1.dbo.KernelForecast.VillageGovernment")]
        public string Village_council { get; set; }
        
        //[Column("T1.dbo.KernelForecast.CultName")]
        public string CultName { get; set; }
        
        //[Column("T1.dbo.KernelForecast.Hybrid")]
        public string Hybrid { get; set; }
        
        //[Column("T1.dbo.KernelForecast.LineAgronomist")]
        public string Agronomist { get; set; }
        
        //[Column("T1.dbo.KernelForecast.SuperAgronomist")]
        public string SuperAgronomist { get; set; }

        //Return a dictionary for popup informing
        public Dictionary<string, string> GetDataDict()
        {
            return new Dictionary<string, string> {
                {"Номер поля", (string)Name },
                {"Площа, га", S.ToString() },
                {"Культура", (string)CultName },
                {"Гібрид", (string)Hybrid },
                {"Агротехнологія", (string)Agrotechnology },
                {"Лінійний агроном", (string)Agronomist },
                {"Провідний агроном", (string)SuperAgronomist },
                {"Кластер", (string)Kluster },
                {"Область", (string)District },
                {"Район", (string)Region },
                {"Сільська рада", (string)Village_council },
            };
        }

    }

    public class Landplot
    {
        //[System.ComponentModel.DataAnnotations.Key]
        public int index { get; set; }

        //[Column("OBJECTID")]
        public long ObjectID { get; set; }

        //[Column("Kluster_name")]
        public string Kluster { get; set; }

        //[Column("District")]
        public string District { get; set; }

        //[Column("Region")]
        public string Region { get; set; }

        //[Column("Vilage_consil")]
        public string Village_council { get; set; }

        //[Column("Square_PKK")]
        public double Square { get; set; }
        
        //[Column("KN_dot")]
        public string CadNumber { get; set; }

        internal Dictionary<string, string> GetDataDict()
        {
            return new Dictionary<string, string>
            {
                {"Кластер", Kluster},
                {"Область", District},
                {"Район", Region},
                {"Сільська рада", Village_council},
                {"Кадастровий номер", CadNumber},
                {"Площа", Square.ToString()}
            };
        }
    }

    public class Rent
    {
        //[System.ComponentModel.DataAnnotations.Key]
        public int index { get; set; }
        public string Company { get; set; }
        public string Klaster { get; set; }
        public string Landlord { get; set; }
        public string IsDead { get; set; }
        public double Square { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string rentForm { get; set; }
        public string LandlordType { get; set; }
        public string AgreementForm { get; set; }
        
        //[Column("Rent")]
        public double Rent_cash { get; set; }
        public string CadastrNumber { get; set; }
        public string Property { get; set; }
        public string Ugid { get; set; }
        public string VillageCouncil { get; set; }
        public string Region { get; set; }
        public string District { get; set; }

        internal Dictionary<string, string> GetDataDict()
        {
            return new Dictionary<string, string>
            {
                {"Орендодавець", Landlord},
                {"Площа, га", Square.ToString() },
                {"Організація", Company },
                {"Форма ОП", rentForm },
                {"Розмір ОП", Rent_cash.ToString()},
                {"Дата укладання ДОЗ", StartDate.ToString() },
                {"Дата закінчення ДОЗ", FinishDate.ToString() },
            };
        }
    }
}
