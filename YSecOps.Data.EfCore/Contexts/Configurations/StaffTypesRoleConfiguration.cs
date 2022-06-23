﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using YSecOps.Data.EfCore.Contexts;
using YSecOps.Data.EfCore.Models;

namespace YSecOps.Data.EfCore.Contexts.Configurations
{
    public partial class StaffTypesRoleConfiguration : IEntityTypeConfiguration<StaffTypesRole>
    {
        public void Configure(EntityTypeBuilder<StaffTypesRole> entity)
        {
            entity.HasKey(e => e.Id)
                .IsClustered(false);

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StaffTypesRole> entity);
    }
}
