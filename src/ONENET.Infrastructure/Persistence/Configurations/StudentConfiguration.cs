using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        // Đảm bảo tính duy nhất của Mã học sinh, tăng tốc tìm kiếm với Index
        builder.HasIndex(s => s.StudentCode)
            .IsUnique();

        builder.Property(s => s.StudentCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Gender)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Email)
            .HasMaxLength(100);

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(s => s.ParentPhoneNumber)
            .HasMaxLength(20);

        // Cấu hình Query Filter để loại trừ các bản ghi đã xóa mềm (Soft Delete)
        builder.HasQueryFilter(s => !s.IsDeleted);

        // Thiết lập quan hệ 1-N giữa Class và Student
        builder.HasOne(s => s.Class)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}