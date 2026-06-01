// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations;

public class TrangThaiHocSinhConfiguration : IEntityTypeConfiguration<TrangThaiHocSinh>
{
    public void Configure(EntityTypeBuilder<TrangThaiHocSinh> builder)
    {
        builder.ToTable("trang_thai_hoc_sinh"); // snake_case cho PostgreSQL

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid");

        builder.Property(x => x.MaTrangThai)
            .HasColumnName("ma_trang_thai")
            .HasMaxLength(20)
            .IsRequired();
        builder.HasIndex(x => x.MaTrangThai).IsUnique(); // Unique Index

        builder.Property(x => x.TenTrangThai)
            .HasColumnName("ten_trang_thai")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MoTa)
            .HasColumnName("mo_ta")
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();

        // Seed Data for TrangThaiHocSinhs
        builder.HasData(
            new TrangThaiHocSinh(Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), "DANG_HOC", "Đang học", "Học sinh đang theo học") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new TrangThaiHocSinh(Guid.Parse("f6e5d4c3-b2a1-8b7a-9d0c-1e2f3a4b5c6d"), "DA_TOT_NGHIEP", "Đã tốt nghiệp", "Học sinh đã hoàn thành chương trình") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new TrangThaiHocSinh(Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-e1f2a3b4c5d6"), "DA_CHUYEN_TRUONG", "Đã chuyển trường", "Học sinh đã chuyển sang trường khác") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new TrangThaiHocSinh(Guid.Parse("d2c1b3a4-f5e6-7d8c-9b0a-1c2d3e4f5a6b"), "TAM_DUNG", "Tạm dừng", "Học sinh tạm dừng việc học") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" }
        );
    }
}