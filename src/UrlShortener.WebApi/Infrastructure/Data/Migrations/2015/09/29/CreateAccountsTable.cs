using System;
using FluentMigrator;

namespace UrlShortener.WebApi.Infrastructure.Data.Migrations._2015._09._29
{
    public class CreateAccountsTable : Migration
    {
        public override void Up()
        {
            Create
                .Table("Accounts")
                    .WithColumn("Id")
                        .AsInt32()
                        .PrimaryKey("PK_Accounts_Id")
                    .WithColumn("Name")
                        .AsString(50)
                        .NotNullable()
                    .WithColumn("Email")
                        .AsString(50)
                        .NotNullable()
                    .WithColumn("Password")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("CreationDate")
                        .AsDateTime()
                        .NotNullable()
                        .WithDefaultValue(DateTime.Now)
                    .WithColumn("Deleted")
                        .AsBoolean()
                        .NotNullable()
                        .WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete
                .Table("Accounts");
        }
    }
}