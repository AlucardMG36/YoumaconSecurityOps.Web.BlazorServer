﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
namespace YoumaconSecurityOps.Core.Shared.Context.Configurations;

public partial class LocationConfiguration : IEntityTypeConfiguration<LocationReader>
{
    public void Configure(EntityTypeBuilder<LocationReader> entity)
    {
        entity.ToTable("Locations");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<LocationReader> entity);
}

