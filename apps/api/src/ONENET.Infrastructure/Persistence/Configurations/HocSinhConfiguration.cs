// QUAN-20260604-153038
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;
using ONENET.Domain.Enums;
using System;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class HocSinhConfiguration : IEntityTypeConfiguration<HocSinh>
    {
        public void Configure(EntityTypeBuilder<HocSinh> builder)
        {
            // Guideline 5. Database: Table/Column naming `snake_case`, plural tables.
            builder.ToTable("hoc_sinhs"); // Plural for table name

            builder.HasKey(h => h.Id); // BaseEntity.Id is PK
            builder.Property(h => h.Id).HasColumnName("id").ValueGeneratedOnAdd(); // Add for new entities

            builder.Property(h => h.MaHocSinh)
                .HasColumnName("ma_hoc_sinh")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(h => h.MaHocSinh).IsUnique(); // BR01: Unique & Indexed

            builder.Property(h => h.HoTen)
                .HasColumnName("ho_ten")
                .HasMaxLength(255)
                .IsRequired();
            builder.HasIndex(h => h.HoTen); // Indexed for search

            builder.Property(h => h.NgaySinh)
                .HasColumnName("ngay_sinh")
                .HasColumnType("date") // Using DateOnly for NgaySinh
                .IsRequired();

            builder.Property(h => h.GioiTinh)
                .HasColumnName("gioi_tinh")
                .HasConversion<string>() // Store enum as string
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(h => h.DiaChi)
                .HasColumnName("dia_chi")
                .HasMaxLength(500);

            builder.Property(h => h.SoDienThoaiPH)
                .HasColumnName("so_dien_thoai_ph")
                .HasMaxLength(20);

            builder.Property(h => h.EmailPH)
                .HasColumnName("email_ph")
                .HasMaxLength(255);

            builder.Property(h => h.LopHocId)
                .HasColumnName("lop_hoc_id")
                .IsRequired();

            builder.Property(h => h.TrangThai)
                .HasColumnName("trang_thai")
                .HasConversion<string>() // Store enum as string
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(h => h.TrangThai); // Indexed for filter

            // BaseEntity properties
            builder.Property(h => h.CreatedAt).HasColumnName("created_at").IsRequired().HasColumnType("timestamp with time zone");
            builder.Property(h => h.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsRequired();
            builder.Property(h => h.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp with time zone");
            builder.Property(h => h.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            builder.Property(h => h.IsDeleted).HasColumnName("is_deleted").IsRequired();

            // Relationships
            // HocSinh N:1 LopHoc (Many students to one class)
            // ON DELETE RESTRICT for LopHoc to HocSinh, which means a LopHoc cannot be hard-deleted if there are associated HocSinhs.
            // This is handled by EF Core's default behavior if LopHoc also has a FK to HocSinh. But if HocSinh is the dependent,
            // then LopHoc.Id is the principal key.
            // Guideline says: "ON DELETE RESTRICT cho mối quan hệ HocSinh.LopHocID với LopHoc.LopHocID"
            // This means if we try to delete a LopHoc, EF Core will prevent it if HocSinhs are linked.
            // For Soft Delete of HocSinh, this FK relation will not be triggered on HocSinh's deletion.
            builder.HasOne(h => h.LopHoc)
                .WithMany(l => l.HocSinhs)
                .HasForeignKey(h => h.LopHocId)
                .OnDelete(DeleteBehavior.Restrict); // Guideline: ON DELETE RESTRICT
        }
    }

    public class LopHocConfiguration : IEntityTypeConfiguration<LopHoc>
    {
        public void Configure(EntityTypeBuilder<LopHoc> builder)
        {
            builder.ToTable("lop_hocs");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(l => l.TenLop)
                .HasColumnName("ten_lop")
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(l => l.TenLop).IsUnique();

            builder.Property(l => l.Khoi)
                .HasColumnName("khoi")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(l => l.NamHoc)
                .HasColumnName("nam_hoc")
                .IsRequired();

            // BaseEntity properties
            builder.Property(l => l.CreatedAt).HasColumnName("created_at").IsRequired().HasColumnType("timestamp with time zone");
            builder.Property(l => l.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsRequired();
            builder.Property(l => l.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp with time zone");
            builder.Property(l => l.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            builder.Property(l => l.IsDeleted).HasColumnName("is_deleted").IsRequired();
        }
    }

    public class HocSinhDiemConfiguration : IEntityTypeConfiguration<HocSinhDiem>
    {
        public void Configure(EntityTypeBuilder<HocSinhDiem> builder)
        {
            builder.ToTable("hoc_sinh_diems");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(d => d.HocSinhId)
                .HasColumnName("hoc_sinh_id")
                .IsRequired();

            builder.Property(d => d.MonHoc)
                .HasColumnName("mon_hoc")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.DiemSo)
                .HasColumnName("diem_so")
                .HasColumnType("numeric(5,2)")
                .IsRequired();

            builder.Property(d => d.HocKy)
                .HasColumnName("hoc_ky")
                .IsRequired();

            builder.Property(d => d.NamHoc)
                .HasColumnName("nam_hoc")
                .IsRequired();

            // BaseEntity properties
            builder.Property(d => d.CreatedAt).HasColumnName("created_at").IsRequired().HasColumnType("timestamp with time zone");
            builder.Property(d => d.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsRequired();
            builder.Property(d => d.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp with time zone");
            builder.Property(d => d.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            builder.Property(d => d.IsDeleted).HasColumnName("is_deleted").IsRequired();

            // Relationships
            builder.HasOne(d => d.HocSinh)
                .WithMany() // Assuming HocSinh doesn't explicitly track HocSinhDiems
                .HasForeignKey(d => d.HocSinhId)
                .OnDelete(DeleteBehavior.Cascade); // If student is hard-deleted (not soft), delete scores.
                                                   // With soft delete, this behavior is usually not hit.
        }
    }
}