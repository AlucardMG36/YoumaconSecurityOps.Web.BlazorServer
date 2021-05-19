﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Data.EntityFramework.ModelBuilders;

namespace YoumaconSecurityOps.Data.EntityFramework
{
    public class YoumaconSecurityDbContext : DbContext
    {
        public YoumaconSecurityDbContext(DbContextOptions<YoumaconSecurityDbContext> options)
        : base(options)
        {

        }

        public DbSet<ContactReader> Contacts { get; set; }

        public DbSet<IncidentReader> Incidents { get; set; }

        public DbSet<LocationReader> Locations { get; set; }

        public DbSet<RadioScheduleReader> RadioSchedules { get; set; }

        public DbSet<RoomScheduleReader> RoomSchedules { get; set; }

        public DbSet<ShiftReader> Shifts { get; set; }

        public DbSet<StaffReader> StaffMembers { get; set; }

        public DbSet<StaffRole> StaffRoles { get; set; }

        public DbSet<StaffType> StaffTypes { get; set; }

        public DbSet<StaffTypesRoles> StaffTypesRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ContactModelBuilder.BuildModel(modelBuilder.Entity<ContactReader>());

            IncidentModelBuilder.BuildModel(modelBuilder.Entity<IncidentReader>());

            LocationModelBuilder.BuildModel(modelBuilder.Entity<LocationReader>());

            RadioScheduleModelBuilder.BuildModel(modelBuilder.Entity<RadioScheduleReader>());

            RoomScheduleModelBuilder.BuildModel(modelBuilder.Entity<RoomScheduleReader>());

            ShiftModelBuilder.BuildModel(modelBuilder.Entity<ShiftReader>());

            StaffModelBuilder.BuildModel(modelBuilder.Entity<StaffReader>());

            StaffRoleModelBuilder.BuildModel(modelBuilder.Entity<StaffRole>());

            StaffTypeModelBuilder.BuildModel(modelBuilder.Entity<StaffType>());

            StaffTypesRolesModelBuilder.BuildModel(modelBuilder.Entity<StaffTypesRoles>());
        }
        
    }
}
