using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;
using ONENET.Domain.Enums;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(s => s.MaHocSinh)
                .HasColumnName("ma_hoc_sinh")
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(s => s.MaHocSinh).IsUnique(); // Unique Index

            builder.Property(s => s.HoVaTen)
                .HasColumnName("ho_va_ten")
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(s => s.HoVaTen); // Index for searching/sorting

            builder.Property(s => s.NgaySinh)
                .HasColumnName("ngay_sinh")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(s => s.GioiTinh)
                .HasColumnName("gioi_tinh")
                .HasConversion<string>() // Store enum as string
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(s => s.DiaChi)
                .HasColumnName("dia_chi")
                .HasMaxLength(255);

            builder.Property(s => s.SdtPhuHuynh)
                .HasColumnName("sdt_phu_huynh")
                .HasMaxLength(20);

            builder.Property(s => s.EmailPhuHuynh)
                .HasColumnName("email_phu_huynh")
                .HasMaxLength(100);

            builder.Property(s => s.LopId)
                .HasColumnName("lop_id")
                .HasColumnType("uuid")
                .IsRequired();
            builder.HasIndex(s => s.LopId); // FK Index

            builder.HasOne(s => s.Lop)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.LopId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete if related data exists, default is no action

            builder.Property(s => s.NgayNhapHoc)
                .HasColumnName("ngay_nhap_hoc")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(s => s.TrangThaiId)
                .HasColumnName("trang_thai_id")
                .HasColumnType("uuid")
                .IsRequired();
            builder.HasIndex(s => s.TrangThaiId); // FK Index

            builder.HasOne(s => s.TrangThai)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TrangThaiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.CreatedAt)
                .HasColumnName("ngay_tao")
                .IsRequired();

            builder.Property(s => s.CreatedBy)
                .HasColumnName("nguoi_tao")
                .HasMaxLength(50);

            builder.Property(s => s.UpdatedAt)
                .HasColumnName("ngay_cap_nhat");

            builder.Property(s => s.UpdatedBy)
                .HasColumnName("nguoi_cap_nhat")
                .HasMaxLength(50);

            builder.Property(s => s.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();

            // Concurrency control using xmin for PostgreSQL
            builder.Property(s => s.RowVersion)
                .HasColumnName("row_version")
                .IsConcurrencyToken()
                .IsRowVersion(); // Uses PostgreSQL's xmin column
        }
    }
}
