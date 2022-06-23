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
    public partial class PronounConfiguration : IEntityTypeConfiguration<Pronoun>
    {
        public void Configure(EntityTypeBuilder<Pronoun> entity)
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Pronoun> entity);
    }
}
