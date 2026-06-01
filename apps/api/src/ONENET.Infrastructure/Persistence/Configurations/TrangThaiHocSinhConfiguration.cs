<!-- QUAN-20260530-2301 -->
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class TrangThaiHocSinhConfiguration : IEntityTypeConfiguration<TrangThaiHocSinh>
    {
        public void Configure(EntityTypeBuilder<TrangThaiHocSinh> builder)
        {
            builder.ToTable("trang_thai_hoc_sinh");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(t => t.MaTrangThai)
                .HasColumnName("ma_trang_thai")
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(t => t.MaTrangThai).IsUnique(); // Unique Index

            builder.Property(t => t.TenTrangThai)
                .HasColumnName("ten_trang_thai")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.MoTa)
                .HasColumnName("mo_ta")
                .HasMaxLength(255);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("ngay_tao")
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .HasColumnName("nguoi_tao")
                .HasMaxLength(50);

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("ngay_cap_nhat");

            builder.Property(t => t.UpdatedBy)
                .HasColumnName("nguoi_cap_nhat")
                .HasMaxLength(50);

            builder.Property(t => t.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();
        }
    }
}