using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ONENET.Infrastructure.Persistence;

#nullable disable

namespace ONENET.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ONENET.Domain.Entities.Lop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_tao");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_tao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("mo_ta");

                    b.Property<string>("TenLop")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ten_lop");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_cap_nhat");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_cap_nhat");

                    b.HasKey("Id");

                    b.HasIndex("TenLop")
                        .IsUnique();

                    b.ToTable("lops", (string)null);
                });

            modelBuilder.Entity("ONENET.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_tao");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_tao");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("dia_chi");

                    b.Property<string>("EmailPhuHuynh")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email_phu_huynh");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("gioi_tinh");

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("ho_va_ten");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid>("LopId")
                        .HasColumnType("uuid")
                        .HasColumnName("lop_id");

                    b.Property<string>("MaHocSinh")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ma_hoc_sinh");

                    b.Property<DateTime>("NgayNhapHoc")
                        .HasColumnType("date")
                        .HasColumnName("ngay_nhap_hoc");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("date")
                        .HasColumnName("ngay_sinh");

                    b.Property<uint>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("row_version");

                    b.Property<string>("SdtPhuHuynh")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("sdt_phu_huynh");

                    b.Property<Guid>("TrangThaiId")
                        .HasColumnType("uuid")
                        .HasColumnName("trang_thai_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_cap_nhat");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_cap_nhat");

                    b.HasKey("Id");

                    b.HasIndex("HoVaTen");

                    b.HasIndex("LopId");

                    b.HasIndex("MaHocSinh")
                        .IsUnique();

                    b.HasIndex("TrangThaiId");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("ONENET.Domain.Entities.TrangThaiHocSinh", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_tao");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_tao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("MaTrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ma_trang_thai");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("mo_ta");

                    b.Property<string>("TenTrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ten_trang_thai");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ngay_cap_nhat");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nguoi_cap_nhat");

                    b.HasKey("Id");

                    b.HasIndex("MaTrangThai")
                        .IsUnique();

                    b.ToTable("trang_thai_hoc_sinh", (string)null);
                });

            modelBuilder.Entity("ONENET.Domain.Entities.Student", b =>
                {
                    b.HasOne("ONENET.Domain.Entities.Lop", "Lop")
                        .WithMany("Students")
                        .HasForeignKey("LopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ONENET.Domain.Entities.TrangThaiHocSinh", "TrangThai")
                        .WithMany("Students")
                        .HasForeignKey("TrangThaiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Lop");

                    b.Navigation("TrangThai");
                });

            modelBuilder.Entity("ONENET.Domain.Entities.Lop", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("ONENET.Domain.Entities.TrangThaiHocSinh", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}

// NOTE: The code generation tool cannot directly generate migration snapshots.
// This migration file is a basic representation.
// In a real project, you would typically run `dotnet ef migrations add InitialMigration`
// and then copy the generated Up/Down methods.
// I've used `DateTime` instead of `DateOnly` in the entity for audit fields consistency,
// but configured `HasColumnType("date")` for specific date fields.