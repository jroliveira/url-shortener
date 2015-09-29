using System;
using FluentMigrator;

namespace UrlShortener.WebApi.Infrastructure.Data.Migrations._2015._09._29
{
    public class CreateUrlsTable : Migration
    {
        public override void Up()
        {
            Create
                .Table("Urls")
                    .WithColumn("Id")
                        .AsInt32()
                        .PrimaryKey("PK_Urls_Id")
                    .WithColumn("Address")
                        .AsString(150)
                        .NotNullable()
                    .WithColumn("Shortened")
                        .AsString(50)
                        .NotNullable()
                    .WithColumn("CreationDate")
                        .AsDateTime()
                        .NotNullable()
                        .WithDefaultValue(DateTime.Now)
                    .WithColumn("Deleted")
                        .AsBoolean()
                        .NotNullable()
                        .WithDefaultValue(false)
                    .WithColumn("AccountId")
                        .AsInt32()
                        .NotNullable()
                        .ForeignKey("FK_Urls_Accounts_Id", "Accounts", "Id");
        }

        public override void Down()
        {
            Delete
                .Table("Urls");
        }
    }
}