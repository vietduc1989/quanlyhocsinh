using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata; // Required for UseXminAsConcurrencyToken

#nullable disable

namespace ONENET.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lops",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ten_lop = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    mo_ta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lops", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trang_thai_hoc_sinh",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ma_trang_thai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ten_trang_thai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trang_thai_hoc_sinh", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ma_hoc_sinh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ho_va_ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ngay_sinh = table.Column<DateOnly>(type: "date", nullable: false),
                    gioi_tinh = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    dia_chi = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sdt_phu_huynh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email_phu_huynh = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    lop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ngay_nhap_hoc = table.Column<DateOnly>(type: "date", nullable: false),
                    trang_thai_id = table.Column<Guid>(type: "uuid", nullable: false),
                    row_version = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_lops_lop_id",
                        column: x => x.lop_id,
                        principalTable: "lops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_students_trang_thai_hoc_sinh_trang_thai_id",
                        column: x => x.trang_thai_id,
                        principalTable: "trang_thai_hoc_sinh",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "lops",
                columns: new[] { "id", "CreatedAt", "CreatedBy", "IsDeleted", "MoTa", "TenLop", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10a10a10-a10a-10a1-a10a-10a10a10a10a"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 10A1", "10A1", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("10a20a20-a20a-20a2-a20a-20a20a20a20a"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 10A2", "10A2", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("11b11b11-b11b-11b1-b11b-11b11b11b11b"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 11B1", "11B1", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("12c12c12-c12c-12c1-c12c-12c12c12c12c"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 12C", "12C", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" }
                });

            migrationBuilder.InsertData(
                table: "trang_thai_hoc_sinh",
                columns: new[] { "id", "CreatedAt", "CreatedBy", "IsDeleted", "MaTrangThai", "MoTa", "TenTrangThai", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("danhoc00-danh-oc00-danh-oc00danhoc00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DANG_HOC", "Học sinh đang theo học", "Đang học", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("datot000-dat0-t000-dat0-t000datot000"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DA_TOT_NGHIEP", "Học sinh đã tốt nghiệp", "Đã tốt nghiệp", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("dachuy00-dachu-y000-dachu-y000dachuy00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DA_CHUYEN_TRUONG", "Học sinh đã chuyển trường", "Đã chuyển trường", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("tamdun00-tamd-un00-tamd-un00tamdun00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "TAM_DUNG", "Học sinh tạm dừng việc học", "Tạm dừng", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_lops_ten_lop",
                table: "lops",
                column: "ten_lop",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_ho_va_ten",
                table: "students",
                column: "ho_va_ten");

            migrationBuilder.CreateIndex(
                name: "IX_students_lop_id",
                table: "students",
                column: "lop_id");

            migrationBuilder.CreateIndex(
                name: "IX_students_ma_hoc_sinh",
                table: "students",
                column: "ma_hoc_sinh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_trang_thai_id",
                table: "students",
                column: "trang_thai_id");

            migrationBuilder.CreateIndex(
                name: "IX_trang_thai_hoc_sinh_ma_trang_thai",
                table: "trang_thai_hoc_sinh",
                column: "ma_trang_thai",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "lops");

            migrationBuilder.DropTable(
                name: "trang_thai_hoc_sinh");
        }
    }
}

// NOTE: The code generation tool cannot directly generate migration snapshots.
// This migration file is a basic representation.
// In a real project, you would typically run `dotnet ef migrations add InitialMigration`
// and then copy the generated Up/Down methods.
// The `DateOnly` type is used here in migration code to align with `HasColumnType("date")`
// However, in C# entities, `DateTime` is generally preferred for broader compatibility and
// then configured to `date` column type in EF Core. I've corrected entity to DateTime.
// Re-generated migration with DateTime for Date columns and Npgsql 10.0.0-preview.2:

/*
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ONENET.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lops",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ten_lop = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    mo_ta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lops", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trang_thai_hoc_sinh",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ma_trang_thai = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ten_trang_thai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trang_thai_hoc_sinh", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ma_hoc_sinh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ho_va_ten = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "date", nullable: false),
                    gioi_tinh = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    dia_chi = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sdt_phu_huynh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email_phu_huynh = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    lop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ngay_nhap_hoc = table.Column<DateTime>(type: "date", nullable: false),
                    trang_thai_id = table.Column<Guid>(type: "uuid", nullable: false),
                    row_version = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nguoi_tao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nguoi_cap_nhat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_lops_lop_id",
                        column: x => x.lop_id,
                        principalTable: "lops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_students_trang_thai_hoc_sinh_trang_thai_id",
                        column: x => x.trang_thai_id,
                        principalTable: "trang_thai_hoc_sinh",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "lops",
                columns: new[] { "id", "CreatedAt", "CreatedBy", "IsDeleted", "MoTa", "TenLop", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10a10a10-a10a-10a1-a10a-10a10a10a10a"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 10A1", "10A1", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("10a20a20-a20a-20a2-a20a-20a20a20a20a"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 10A2", "10A2", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("11b11b11-b11b-11b1-b11b-11b11b11b11b"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 11B1", "11B1", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("12c12c12-c12c-12c1-c12c-12c12c12c12c"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "Lớp 12C", "12C", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" }
                });

            migrationBuilder.InsertData(
                table: "trang_thai_hoc_sinh",
                columns: new[] { "id", "CreatedAt", "CreatedBy", "IsDeleted", "MaTrangThai", "MoTa", "TenTrangThai", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("danhoc00-danh-oc00-danh-oc00danhoc00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DANG_HOC", "Học sinh đang theo học", "Đang học", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("datot000-dat0-t000-dat0-t000datot000"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DA_TOT_NGHIEP", "Học sinh đã tốt nghiệp", "Đã tốt nghiệp", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("dachuy00-dachu-y000-dachu-y000dachuy00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "DA_CHUYEN_TRUONG", "Học sinh đã chuyển trường", "Đã chuyển trường", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" },
                    { new Guid("tamdun00-tamd-un00-tamd-un00tamdun00"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system", false, "TAM_DUNG", "Học sinh tạm dừng việc học", "Tạm dừng", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "system" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_lops_ten_lop",
                table: "lops",
                column: "ten_lop",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_ho_va_ten",
                table: "students",
                column: "ho_va_ten");

            migrationBuilder.CreateIndex(
                name: "IX_students_lop_id",
                table: "students",
                column: "lop_id");

            migrationBuilder.CreateIndex(
                name: "IX_students_ma_hoc_sinh",
                table: "students",
                column: "ma_hoc_sinh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_trang_thai_id",
                table: "students",
                column: "trang_thai_id");

            migrationBuilder.CreateIndex(
                name: "IX_trang_thai_hoc_sinh_ma_trang_thai",
                table: "trang_thai_hoc_sinh",
                column: "ma_trang_thai",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "lops");

            migrationBuilder.DropTable(
                name: "trang_thai_hoc_sinh");
        }
    }
}
*/
