// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class LopConfiguration : IEntityTypeConfiguration<Lop>
    {
        public void Configure(EntityTypeBuilder<Lop> builder)
        {
            builder.ToTable("lops");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid");

            builder.Property(x => x.TenLop)
                   .HasColumnName("ten_lop")
                   .HasMaxLength(20)
                   .IsRequired();
            builder.HasIndex(x => x.TenLop).IsUnique();

            builder.Property(x => x.MoTa)
                   .HasColumnName("mo_ta")
                   .HasMaxLength(255);

            // BaseEntity properties
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.HasQueryFilter(x => !x.IsDeleted);

            // Seed Data for Lop
            builder.HasData(GetSeedData());
        }

        private static List<Lop> GetSeedData()
        {
            // Using predefined Guids for consistency across environments
            var now = DateTime.UtcNow;
            var adminUser = "system_seed";

            return new List<Lop>
            {
                new Lop
                {
                    Id = Guid.Parse("01b1e2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5d"), // Arbitrary but consistent GUID
                    TenLop = "10A1",
                    MoTa = "Lớp 10 Ban A1",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new Lop
                {
                    Id = Guid.Parse("02b1e2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5d"),
                    TenLop = "10A2",
                    MoTa = "Lớp 10 Ban A2",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new Lop
                {
                    Id = Guid.Parse("03b1e2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5d"),
                    TenLop = "11B1",
                    MoTa = "Lớp 11 Ban B1",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new Lop
                {
                    Id = Guid.Parse("04b1e2c3-d4e5-6f7a-8b9c-0d1e2f3a4b5d"),
                    TenLop = "12C",
                    MoTa = "Lớp 12 Ban C",
                    CreatedAt = now, CreatedBy = adminUser
                }
            };
        }
    }
}