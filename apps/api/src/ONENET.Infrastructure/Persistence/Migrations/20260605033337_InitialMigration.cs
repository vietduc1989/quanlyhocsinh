using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONENET.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    action_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entity_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    changes = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lop_hocs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ten_lop = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    khoi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nam_hoc = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lop_hocs", x => x.id);
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
                name: "hoc_sinhs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ma_hoc_sinh = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ho_ten = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ngay_sinh = table.Column<DateOnly>(type: "date", nullable: false),
                    gioi_tinh = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    dia_chi = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    so_dien_thoai_ph = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email_ph = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    lop_hoc_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trang_thai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TrangThaiHocSinhId = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoc_sinhs", x => x.id);
                    table.ForeignKey(
                        name: "FK_hoc_sinhs_lop_hocs_lop_hoc_id",
                        column: x => x.lop_hoc_id,
                        principalTable: "lop_hocs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hoc_sinhs_trang_thai_hoc_sinh_TrangThaiHocSinhId",
                        column: x => x.TrangThaiHocSinhId,
                        principalTable: "trang_thai_hoc_sinh",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "hoc_sinh_diems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hoc_sinh_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mon_hoc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    diem_so = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    hoc_ky = table.Column<int>(type: "integer", nullable: false),
                    nam_hoc = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoc_sinh_diems", x => x.id);
                    table.ForeignKey(
                        name: "FK_hoc_sinh_diems_hoc_sinhs_hoc_sinh_id",
                        column: x => x.hoc_sinh_id,
                        principalTable: "hoc_sinhs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_auditlogs_entity_id",
                table: "audit_log",
                column: "entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditlogs_timestamp",
                table: "audit_log",
                column: "timestamp",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinh_diems_hoc_sinh_id",
                table: "hoc_sinh_diems",
                column: "hoc_sinh_id");

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinhs_ho_ten",
                table: "hoc_sinhs",
                column: "ho_ten");

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinhs_lop_hoc_id",
                table: "hoc_sinhs",
                column: "lop_hoc_id");

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinhs_ma_hoc_sinh",
                table: "hoc_sinhs",
                column: "ma_hoc_sinh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinhs_trang_thai",
                table: "hoc_sinhs",
                column: "trang_thai");

            migrationBuilder.CreateIndex(
                name: "IX_hoc_sinhs_TrangThaiHocSinhId",
                table: "hoc_sinhs",
                column: "TrangThaiHocSinhId");

            migrationBuilder.CreateIndex(
                name: "IX_lop_hocs_ten_lop",
                table: "lop_hocs",
                column: "ten_lop",
                unique: true);

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
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "hoc_sinh_diems");

            migrationBuilder.DropTable(
                name: "hoc_sinhs");

            migrationBuilder.DropTable(
                name: "lop_hocs");

            migrationBuilder.DropTable(
                name: "trang_thai_hoc_sinh");
        }
    }
}
