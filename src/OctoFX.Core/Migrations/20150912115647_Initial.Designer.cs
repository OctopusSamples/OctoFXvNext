using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using OctoFX.Core.Model;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace OctoFX.Core.Migrations
{
    [DbContext(typeof(OctoFXContext))]
    [Migration("20150912115647_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15679")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("OctoFX.Core.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHashed");

                    b.Key("Id");
                });

            modelBuilder.Entity("OctoFX.Core.Model.BeneficiaryAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Country");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Nickname");

                    b.Property<string>("SwiftBicBsb");

                    b.Key("Id");
                });

            modelBuilder.Entity("OctoFX.Core.Model.Deal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<decimal>("BuyAmount");

                    b.Property<DateTimeOffset>("EnteredDate");

                    b.Property<int?>("NominatedBeneficiaryAccountId");

                    b.Property<decimal>("SellAmount");

                    b.Property<int>("Status");

                    b.Key("Id");
                });

            modelBuilder.Entity("OctoFX.Core.Model.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Rate");

                    b.Property<string>("SellBuyCurrencyPairStringValue")
                        .Annotation("Relational:ColumnName", "SellBuyCurrencyPair");

                    b.Key("Id");
                });

            modelBuilder.Entity("OctoFX.Core.Model.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BuyAmount");

                    b.Property<DateTimeOffset>("ExpiryDate");

                    b.Property<DateTimeOffset>("QuotedDate");

                    b.Property<decimal>("Rate");

                    b.Property<decimal>("SellAmount");

                    b.Property<string>("SellBuyCurrencyPairStringValue")
                        .Annotation("Relational:ColumnName", "SellBuyCurrencyPair");

                    b.Key("Id");
                });

            modelBuilder.Entity("OctoFX.Core.Model.BeneficiaryAccount", b =>
                {
                    b.Reference("OctoFX.Core.Model.Account")
                        .InverseCollection()
                        .ForeignKey("AccountId");
                });

            modelBuilder.Entity("OctoFX.Core.Model.Deal", b =>
                {
                    b.Reference("OctoFX.Core.Model.Account")
                        .InverseCollection()
                        .ForeignKey("AccountId");

                    b.Reference("OctoFX.Core.Model.BeneficiaryAccount")
                        .InverseCollection()
                        .ForeignKey("NominatedBeneficiaryAccountId");
                });
        }
    }
}
