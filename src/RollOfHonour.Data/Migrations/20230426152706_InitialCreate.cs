using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollOfHonour.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "ArmedServices",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.ArmedServices", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "DataProblems",
            //     columns: table => new
            //     {
            //         DataProblemId = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: true),
            //         PersonId = table.Column<int>(type: "int", nullable: true),
            //         RecordedNameId = table.Column<int>(type: "int", nullable: true),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         SubmittedByUserProfileId = table.Column<int>(type: "int", nullable: true),
            //         SubmittedOn = table.Column<DateTime>(type: "datetime", nullable: false),
            //         Active = table.Column<bool>(type: "bit", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.DataProblems", x => x.DataProblemId);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Decorations",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Initials = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.Decorations", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Regiments",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.Regiments", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "UserProfile",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.UserProfile", x => x.UserId);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "WarMemorials",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UKNIWMRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Easting = table.Column<int>(type: "int", nullable: false),
            //         Northing = table.Column<int>(type: "int", nullable: false),
            //         MainPhotoId = table.Column<int>(type: "int", nullable: true),
            //         NamesCount = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
            //         District = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.WarMemorials", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Wars",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         StartYear = table.Column<int>(type: "int", nullable: false),
            //         EndYear = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.Wars", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "webpages_Membership",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //         ConfirmationToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //         IsConfirmed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
            //         LastPasswordFailureDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //         PasswordFailuresSinceLastSuccess = table.Column<int>(type: "int", nullable: false),
            //         Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
            //         PasswordChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //         PasswordSalt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
            //         PasswordVerificationToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //         PasswordVerificationTokenExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK__webpages__1788CC4C4AB81AF0", x => x.UserId);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "webpages_OAuthMembership",
            //     columns: table => new
            //     {
            //         Provider = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //         ProviderUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //         UserId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK__webpages__F53FC0ED46E78A0C", x => new { x.Provider, x.ProviderUserId });
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "SubUnits",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         RegimentId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.SubUnits", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.SubUnits_dbo.Regiments_RegimentId",
            //             column: x => x.RegimentId,
            //             principalTable: "Regiments",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         When = table.Column<DateTime>(type: "datetime", nullable: false),
            //         UserId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.AuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.AuditItems_dbo.UserProfile_UserId",
            //             column: x => x.UserId,
            //             principalTable: "UserProfile",
            //             principalColumn: "UserId",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "UserProfileWarMemorial",
            //     columns: table => new
            //     {
            //         UserProfileUserId = table.Column<int>(type: "int", nullable: false),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.UserProfileWarMemorials", x => new { x.UserProfileUserId, x.WarMemorialId });
            //         table.ForeignKey(
            //             name: "FK_dbo.UserProfileWarMemorials_dbo.UserProfile_UserProfile_UserId",
            //             column: x => x.UserProfileUserId,
            //             principalTable: "UserProfile",
            //             principalColumn: "UserId",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_dbo.UserProfileWarMemorials_dbo.WarMemorials_WarMemorial_Id",
            //             column: x => x.WarMemorialId,
            //             principalTable: "WarMemorials",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "DataProblemAuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false),
            //         DataProblemId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.DataProblemAuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.DataProblemAuditItems_dbo.AuditItems_Id",
            //             column: x => x.Id,
            //             principalTable: "AuditItems",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.DataProblemAuditItems_dbo.DataProblems_DataProblemId",
            //             column: x => x.DataProblemId,
            //             principalTable: "DataProblems",
            //             principalColumn: "DataProblemId",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "WarMemorialAuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.WarMemorialAuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.WarMemorialAuditItems_dbo.AuditItems_Id",
            //             column: x => x.Id,
            //             principalTable: "AuditItems",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.WarMemorialAuditItems_dbo.WarMemorials_WarMemorialId",
            //             column: x => x.WarMemorialId,
            //             principalTable: "WarMemorials",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "DecorationPersons",
            //     columns: table => new
            //     {
            //         DecorationId = table.Column<int>(name: "Decoration_Id", type: "int", nullable: false),
            //         PersonId = table.Column<int>(name: "Person_Id", type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.DecorationPersons", x => new { x.DecorationId, x.PersonId });
            //         table.ForeignKey(
            //             name: "FK_dbo.DecorationPersons_dbo.Decorations_Decoration_Id",
            //             column: x => x.DecorationId,
            //             principalTable: "Decorations",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "DecorationRecordedNames",
            //     columns: table => new
            //     {
            //         DecorationId = table.Column<int>(name: "Decoration_Id", type: "int", nullable: false),
            //         RecordedNameId = table.Column<int>(name: "RecordedName_Id", type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.DecorationRecordedNames", x => new { x.DecorationId, x.RecordedNameId });
            //         table.ForeignKey(
            //             name: "FK_dbo.DecorationRecordedNames_dbo.Decorations_Decoration_Id",
            //             column: x => x.DecorationId,
            //             principalTable: "Decorations",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "People",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         FirstNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Rank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ServiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
            //         DateOfDeath = table.Column<DateTime>(type: "datetime", nullable: true),
            //         AgeAtDeath = table.Column<int>(type: "int", nullable: true),
            //         Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         MainPhotoId = table.Column<int>(type: "int", nullable: true),
            //         SubUnitId = table.Column<int>(type: "int", nullable: true),
            //         WarId = table.Column<int>(name: "War_Id", type: "int", nullable: true),
            //         ArmedServiceId = table.Column<int>(name: "ArmedService_Id", type: "int", nullable: true),
            //         Deleted = table.Column<bool>(type: "bit", nullable: false),
            //         AddressAtEnlistment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Cwgc = table.Column<int>(type: "int", nullable: true),
            //         PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         EmploymentHobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         FamilyHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         MilitaryHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.People", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.People_dbo.ArmedServices_ArmedService_Id",
            //             column: x => x.ArmedServiceId,
            //             principalTable: "ArmedServices",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.People_dbo.SubUnits_SubUnitId",
            //             column: x => x.SubUnitId,
            //             principalTable: "SubUnits",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.People_dbo.Wars_War_Id",
            //             column: x => x.WarId,
            //             principalTable: "Wars",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "PersonAuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false),
            //         PersonId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.PersonAuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.PersonAuditItems_dbo.AuditItems_Id",
            //             column: x => x.Id,
            //             principalTable: "AuditItems",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.PersonAuditItems_dbo.People_PersonId",
            //             column: x => x.PersonId,
            //             principalTable: "People",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "PersonModerations",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         FirstNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Rank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ServiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
            //         DateOfDeath = table.Column<DateTime>(type: "datetime", nullable: true),
            //         AgeAtDeath = table.Column<int>(type: "int", nullable: true),
            //         Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Deleted = table.Column<bool>(type: "bit", nullable: false),
            //         AddressAtEnlistment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Cwgc = table.Column<int>(type: "int", nullable: true),
            //         PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         EmploymentHobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         FamilyHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         MilitaryHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         SubUnitId = table.Column<int>(type: "int", nullable: true),
            //         PersonId = table.Column<int>(type: "int", nullable: false),
            //         Accepted = table.Column<bool>(type: "bit", nullable: true),
            //         UploadDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         ModeratorId = table.Column<int>(type: "int", nullable: true),
            //         ModerationComplete = table.Column<DateTime>(type: "datetime", nullable: true),
            //         ModeratorFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.PersonModerations", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.PersonModerations_dbo.People_PersonId",
            //             column: x => x.PersonId,
            //             principalTable: "People",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_dbo.PersonModerations_dbo.SubUnits_SubUnitId",
            //             column: x => x.SubUnitId,
            //             principalTable: "SubUnits",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.PersonModerations_dbo.UserProfile_UserId",
            //             column: x => x.UserId,
            //             principalTable: "UserProfile",
            //             principalColumn: "UserId",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "PhotoModerations",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: true),
            //         PersonId = table.Column<int>(type: "int", nullable: true),
            //         Accepted = table.Column<bool>(type: "bit", nullable: true),
            //         UploadDate = table.Column<DateTime>(type: "datetime", nullable: false),
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         ModeratorId = table.Column<int>(type: "int", nullable: true),
            //         ModerationComplete = table.Column<DateTime>(type: "datetime", nullable: true),
            //         ModeratorFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Copyright = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.PhotoModerations", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.PhotoModerations_dbo.People_PersonId",
            //             column: x => x.PersonId,
            //             principalTable: "People",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.PhotoModerations_dbo.UserProfile_UserId",
            //             column: x => x.UserId,
            //             principalTable: "UserProfile",
            //             principalColumn: "UserId",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_dbo.PhotoModerations_dbo.WarMemorials_WarMemorialId",
            //             column: x => x.WarMemorialId,
            //             principalTable: "WarMemorials",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Photos",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: true),
            //         PersonId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.Photos", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.Photos_dbo.People_PersonId",
            //             column: x => x.PersonId,
            //             principalTable: "People",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.Photos_dbo.WarMemorials_WarMemorialId",
            //             column: x => x.WarMemorialId,
            //             principalTable: "WarMemorials",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "RecordedNames",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         IWMNameRefNo = table.Column<int>(type: "int", nullable: true),
            //         AsRecorded = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Rank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ServiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         WarMemorialId = table.Column<int>(type: "int", nullable: false),
            //         WarId = table.Column<int>(type: "int", nullable: true),
            //         PersonId = table.Column<int>(type: "int", nullable: true),
            //         SubUnitId = table.Column<int>(type: "int", nullable: true),
            //         ArmedServiceId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.RecordedNames", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNames_dbo.ArmedServices_ArmedServiceId",
            //             column: x => x.ArmedServiceId,
            //             principalTable: "ArmedServices",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNames_dbo.People_PersonId",
            //             column: x => x.PersonId,
            //             principalTable: "People",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNames_dbo.SubUnits_SubUnitId",
            //             column: x => x.SubUnitId,
            //             principalTable: "SubUnits",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNames_dbo.WarMemorials_WarMemorialId",
            //             column: x => x.WarMemorialId,
            //             principalTable: "WarMemorials",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNames_dbo.Wars_WarId",
            //             column: x => x.WarId,
            //             principalTable: "Wars",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "PhotoAuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false),
            //         PhotoId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.PhotoAuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.PhotoAuditItems_dbo.AuditItems_Id",
            //             column: x => x.Id,
            //             principalTable: "AuditItems",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.PhotoAuditItems_dbo.Photos_PhotoId",
            //             column: x => x.PhotoId,
            //             principalTable: "Photos",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "RecordedNameAuditItems",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false),
            //         RecordedNameId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_dbo.RecordedNameAuditItems", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNameAuditItems_dbo.AuditItems_Id",
            //             column: x => x.Id,
            //             principalTable: "AuditItems",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_dbo.RecordedNameAuditItems_dbo.RecordedNames_RecordedNameId",
            //             column: x => x.RecordedNameId,
            //             principalTable: "RecordedNames",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UserId",
            //     table: "AuditItems",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_DataProblemId",
            //     table: "DataProblemAuditItems",
            //     column: "DataProblemId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Id",
            //     table: "DataProblemAuditItems",
            //     column: "Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Decoration_Id",
            //     table: "DecorationPersons",
            //     column: "Decoration_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Person_Id",
            //     table: "DecorationPersons",
            //     column: "Person_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Decoration_Id",
            //     table: "DecorationRecordedNames",
            //     column: "Decoration_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_RecordedName_Id",
            //     table: "DecorationRecordedNames",
            //     column: "RecordedName_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_ArmedService_Id",
            //     table: "People",
            //     column: "ArmedService_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_MainPhotoId",
            //     table: "People",
            //     column: "MainPhotoId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_SubUnitId",
            //     table: "People",
            //     column: "SubUnitId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_War_Id",
            //     table: "People",
            //     column: "War_Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Id",
            //     table: "PersonAuditItems",
            //     column: "Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PersonId",
            //     table: "PersonAuditItems",
            //     column: "PersonId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PersonId",
            //     table: "PersonModerations",
            //     column: "PersonId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_SubUnitId",
            //     table: "PersonModerations",
            //     column: "SubUnitId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UserId",
            //     table: "PersonModerations",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Id",
            //     table: "PhotoAuditItems",
            //     column: "Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PhotoId",
            //     table: "PhotoAuditItems",
            //     column: "PhotoId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PersonId",
            //     table: "PhotoModerations",
            //     column: "PersonId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UserId",
            //     table: "PhotoModerations",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarMemorialId",
            //     table: "PhotoModerations",
            //     column: "WarMemorialId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PersonId",
            //     table: "Photos",
            //     column: "PersonId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarMemorialId",
            //     table: "Photos",
            //     column: "WarMemorialId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Id",
            //     table: "RecordedNameAuditItems",
            //     column: "Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_RecordedNameId",
            //     table: "RecordedNameAuditItems",
            //     column: "RecordedNameId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_ArmedServiceId",
            //     table: "RecordedNames",
            //     column: "ArmedServiceId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_PersonId",
            //     table: "RecordedNames",
            //     column: "PersonId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_SubUnitId",
            //     table: "RecordedNames",
            //     column: "SubUnitId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarId",
            //     table: "RecordedNames",
            //     column: "WarId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarMemorialId",
            //     table: "RecordedNames",
            //     column: "WarMemorialId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_RegimentId",
            //     table: "SubUnits",
            //     column: "RegimentId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UserProfile_UserId",
            //     table: "UserProfileWarMemorial",
            //     column: "UserProfileUserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarMemorial_Id",
            //     table: "UserProfileWarMemorial",
            //     column: "WarMemorialId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Id",
            //     table: "WarMemorialAuditItems",
            //     column: "Id");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_WarMemorialId",
            //     table: "WarMemorialAuditItems",
            //     column: "WarMemorialId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_MainPhotoId",
            //     table: "WarMemorials",
            //     column: "MainPhotoId");
            //
            // migrationBuilder.AddForeignKey(
            //     name: "FK_dbo.DecorationPersons_dbo.People_Person_Id",
            //     table: "DecorationPersons",
            //     column: "Person_Id",
            //     principalTable: "People",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
            //
            // migrationBuilder.AddForeignKey(
            //     name: "FK_dbo.DecorationRecordedNames_dbo.RecordedNames_RecordedName_Id",
            //     table: "DecorationRecordedNames",
            //     column: "RecordedName_Id",
            //     principalTable: "RecordedNames",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
            //
            // migrationBuilder.AddForeignKey(
            //     name: "FK_dbo.People_dbo.Photos_MainPhotoId",
            //     table: "People",
            //     column: "MainPhotoId",
            //     principalTable: "Photos",
            //     principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_dbo.Photos_dbo.People_PersonId",
            //     table: "Photos");
            //
            // migrationBuilder.DropTable(
            //     name: "DataProblemAuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "DecorationPersons");
            //
            // migrationBuilder.DropTable(
            //     name: "DecorationRecordedNames");
            //
            // migrationBuilder.DropTable(
            //     name: "PersonAuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "PersonModerations");
            //
            // migrationBuilder.DropTable(
            //     name: "PhotoAuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "PhotoModerations");
            //
            // migrationBuilder.DropTable(
            //     name: "RecordedNameAuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "UserProfileWarMemorial");
            //
            // migrationBuilder.DropTable(
            //     name: "WarMemorialAuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "webpages_Membership");
            //
            // migrationBuilder.DropTable(
            //     name: "webpages_OAuthMembership");
            //
            // migrationBuilder.DropTable(
            //     name: "DataProblems");
            //
            // migrationBuilder.DropTable(
            //     name: "Decorations");
            //
            // migrationBuilder.DropTable(
            //     name: "RecordedNames");
            //
            // migrationBuilder.DropTable(
            //     name: "AuditItems");
            //
            // migrationBuilder.DropTable(
            //     name: "UserProfile");
            //
            // migrationBuilder.DropTable(
            //     name: "People");
            //
            // migrationBuilder.DropTable(
            //     name: "ArmedServices");
            //
            // migrationBuilder.DropTable(
            //     name: "Photos");
            //
            // migrationBuilder.DropTable(
            //     name: "SubUnits");
            //
            // migrationBuilder.DropTable(
            //     name: "Wars");
            //
            // migrationBuilder.DropTable(
            //     name: "WarMemorials");
            //
            // migrationBuilder.DropTable(
            //     name: "Regiments");
        }
    }
}
