using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace OctoFX.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PasswordHashed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Rate = table.Column<decimal>(nullable: false),
                    SellBuyCurrencyPair = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    BuyAmount = table.Column<decimal>(nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(nullable: false),
                    QuotedDate = table.Column<DateTimeOffset>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    SellAmount = table.Column<decimal>(nullable: false),
                    SellBuyCurrencyPair = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "BeneficiaryAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    SwiftBicBsb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiaryAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiaryAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Deal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    BuyAmount = table.Column<decimal>(nullable: false),
                    EnteredDate = table.Column<DateTimeOffset>(nullable: false),
                    NominatedBeneficiaryAccountId = table.Column<int>(nullable: true),
                    SellAmount = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deal_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deal_BeneficiaryAccount_NominatedBeneficiaryAccountId",
                        column: x => x.NominatedBeneficiaryAccountId,
                        principalTable: "BeneficiaryAccount",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Deal");
            migrationBuilder.DropTable("ExchangeRate");
            migrationBuilder.DropTable("Quote");
            migrationBuilder.DropTable("BeneficiaryAccount");
            migrationBuilder.DropTable("Account");
        }
    }
}
