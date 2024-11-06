using BoatApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectLayers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLayers.Data.Context
{
    public class BoatAppDbContext : DbContext
    {

        public BoatAppDbContext(DbContextOptions<BoatAppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new BoatConfiguration());
            modelBuilder.ApplyConfiguration(new BoatFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());


            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });





            base.OnModelCreating(modelBuilder);
        }




        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
        public DbSet<BoatEntity> Boats => Set<BoatEntity>();
        public DbSet<BoatFeatureEntity> BoatFeatures => Set<BoatFeatureEntity>();
        public DbSet<SalesEntity> Sales => Set<SalesEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();

    }
}
