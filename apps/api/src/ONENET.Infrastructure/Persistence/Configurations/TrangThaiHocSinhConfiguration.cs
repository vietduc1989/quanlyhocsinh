// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class TrangThaiHocSinhConfiguration : IEntityTypeConfiguration<TrangThaiHocSinh>
    {
        public void Configure(EntityTypeBuilder<TrangThaiHocSinh> builder)
        {
            builder.ToTable("trang_thai_hoc_sinh");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid");

            builder.Property(x => x.MaTrangThai)
                   .HasColumnName("ma_trang_thai")
                   .HasMaxLength(20)
                   .IsRequired();
            builder.HasIndex(x => x.MaTrangThai).IsUnique();

            builder.Property(x => x.TenTrangThai)
                   .HasColumnName("ten_trang_thai")
                   .HasMaxLength(50)
                   .IsRequired();

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

            // Seed Data for TrangThaiHocSinh
            builder.HasData(GetSeedData());
        }

        private static List<TrangThaiHocSinh> GetSeedData()
        {
            var now = DateTime.UtcNow;
            var adminUser = "system_seed";

            return new List<TrangThaiHocSinh>
            {
                new TrangThaiHocSinh
                {
                    Id = Guid.Parse("0a1b2c3d-e4f5-6a7b-8c9d-0e1f2a3b4c5e"), // Arbitrary but consistent GUID
                    MaTrangThai = "DANG_HOC",
                    TenTrangThai = "Đang học",
                    MoTa = "Học sinh đang theo học tại trường.",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new TrangThaiHocSinh
                {
                    Id = Guid.Parse("1a1b2c3d-e4f5-6a7b-8c9d-0e1f2a3b4c5e"),
                    MaTrangThai = "DA_TOT_NGHIEP",
                    TenTrangThai = "Đã tốt nghiệp",
                    MoTa = "Học sinh đã hoàn thành chương trình học và tốt nghiệp.",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new TrangThaiHocSinh
                {
                    Id = Guid.Parse("2a1b2c3d-e4f5-6a7b-8c9d-0e1f2a3b4c5e"),
                    MaTrangThai = "DA_CHUYEN_TRUONG",
                    TenTrangThai = "Đã chuyển trường",
                    MoTa = "Học sinh đã chuyển sang trường khác.",
                    CreatedAt = now, CreatedBy = adminUser
                },
                new TrangThaiHocSinh
                {
                    Id = Guid.Parse("3a1b2c3d-e4f5-6a7b-8c9d-0e1f2a3b4c5e"),
                    MaTrangThai = "TAM_DUNG",
                    TenTrangThai = "Tạm dừng",
                    MoTa = "Học sinh tạm thời dừng việc học.",
                    CreatedAt = now, CreatedBy = adminUser
                }
            };
        }
    }
}