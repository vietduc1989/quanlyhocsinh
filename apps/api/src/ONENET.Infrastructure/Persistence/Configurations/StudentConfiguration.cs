// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid");

            builder.Property(x => x.MaHocSinh)
                   .HasColumnName("ma_hoc_sinh")
                   .HasMaxLength(20)
                   .IsRequired();
            builder.HasIndex(x => x.MaHocSinh).IsUnique(); // QUAN-20260530-2301-BR01

            builder.Property(x => x.HoVaTen)
                   .HasColumnName("ho_va_ten")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.NgaySinh)
                   .HasColumnName("ngay_sinh")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.GioiTinh)
                   .HasColumnName("gioi_tinh")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.DiaChi)
                   .HasColumnName("dia_chi")
                   .HasMaxLength(255);

            builder.Property(x => x.SDTPhuHuynh)
                   .HasColumnName("sdt_phu_huynh")
                   .HasMaxLength(20);

            builder.Property(x => x.EmailPhuHuynh)
                   .HasColumnName("email_phu_huynh")
                   .HasMaxLength(100);

            builder.Property(x => x.LopId)
                   .HasColumnName("lop_id")
                   .HasColumnType("uuid")
                   .IsRequired();
            builder.HasOne(x => x.Lop)
                   .WithMany(l => l.Students)
                   .HasForeignKey(x => x.LopId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for lookup tables

            builder.Property(x => x.NgayNhapHoc)
                   .HasColumnName("ngay_nhap_hoc")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(x => x.TrangThaiId)
                   .HasColumnName("trang_thai_id")
                   .HasColumnType("uuid")
                   .IsRequired();
            builder.HasOne(x => x.TrangThaiHocSinh)
                   .WithMany(t => t.Students)
                   .HasForeignKey(x => x.TrangThaiId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for lookup tables

            // BaseEntity properties
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.HasQueryFilter(x => !x.IsDeleted); // Soft delete

            // Concurrency Token
            builder.Property(x => x.RowVersion).HasColumnName("row_version").IsConcurrencyToken().UseXminAsConcurrencyToken();

            // Index for search/filter/sort
            builder.HasIndex(x => x.HoVaTen);
            builder.HasIndex(x => x.LopId);
            builder.HasIndex(x => x.TrangThaiId);
        }
    }
}