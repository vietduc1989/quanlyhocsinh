using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class LopConfiguration : IEntityTypeConfiguration<Lop>
    {
        public void Configure(EntityTypeBuilder<Lop> builder)
        {
            builder.ToTable("lops");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(l => l.TenLop)
                .HasColumnName("ten_lop")
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(l => l.TenLop).IsUnique(); // Unique Index

            builder.Property(l => l.MoTa)
                .HasColumnName("mo_ta")
                .HasMaxLength(255);

            builder.Property(l => l.CreatedAt)
                .HasColumnName("ngay_tao")
                .IsRequired();

            builder.Property(l => l.CreatedBy)
                .HasColumnName("nguoi_tao")
                .HasMaxLength(50);

            builder.Property(l => l.UpdatedAt)
                .HasColumnName("ngay_cap_nhat");

            builder.Property(l => l.UpdatedBy)
                .HasColumnName("nguoi_cap_nhat")
                .HasMaxLength(50);

            builder.Property(l => l.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();
        }
    }
}
