using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class QualAndClaimAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateUserQualificationClaimViewModel",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    QualificationId = table.Column<Guid>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateUserQualificationClaimViewModel", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserQualfClaims",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    QualificationId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    DisabledBy = table.Column<Guid>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeleteBy = table.Column<Guid>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQualfClaims", x => new { x.UserId, x.QualificationId });
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Uid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    QualfType = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsObselete = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedByGuid = table.Column<Guid>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByGuid = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Photopath = table.Column<string>(nullable: true),
                    QualificationsUid = table.Column<Guid>(nullable: true),
                    CreateUserQualificationClaimViewModelUserId = table.Column<Guid>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    QualificationUid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Qualifications_CreateUserQualificationClaimViewModel_CreateUserQualificationClaimViewModelUserId",
                        column: x => x.CreateUserQualificationClaimViewModelUserId,
                        principalTable: "CreateUserQualificationClaimViewModel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qualifications_Qualifications_QualificationsUid",
                        column: x => x.QualificationsUid,
                        principalTable: "Qualifications",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qualifications_Qualifications_QualificationUid",
                        column: x => x.QualificationUid,
                        principalTable: "Qualifications",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersQualifications",
                columns: table => new
                {
                    Uid = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserQualfClaimUserId = table.Column<Guid>(nullable: true),
                    UserQualfClaimQualificationId = table.Column<Guid>(nullable: true),
                    QualificationUid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersQualifications", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_UsersQualifications_Qualifications_QualificationUid",
                        column: x => x.QualificationUid,
                        principalTable: "Qualifications",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersQualifications_UserQualfClaims_UserQualfClaimUserId_UserQualfClaimQualificationId",
                        columns: x => new { x.UserQualfClaimUserId, x.UserQualfClaimQualificationId },
                        principalTable: "UserQualfClaims",
                        principalColumns: new[] { "UserId", "QualificationId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_CreateUserQualificationClaimViewModelUserId",
                table: "Qualifications",
                column: "CreateUserQualificationClaimViewModelUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_QualificationsUid",
                table: "Qualifications",
                column: "QualificationsUid");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_QualificationUid",
                table: "Qualifications",
                column: "QualificationUid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersQualifications_QualificationUid",
                table: "UsersQualifications",
                column: "QualificationUid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersQualifications_UserQualfClaimUserId_UserQualfClaimQualificationId",
                table: "UsersQualifications",
                columns: new[] { "UserQualfClaimUserId", "UserQualfClaimQualificationId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersQualifications");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "UserQualfClaims");

            migrationBuilder.DropTable(
                name: "CreateUserQualificationClaimViewModel");
        }
    }
}
