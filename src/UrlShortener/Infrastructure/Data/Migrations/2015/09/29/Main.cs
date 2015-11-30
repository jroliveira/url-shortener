using FluentMigrator;

namespace UrlShortener.Infrastructure.Data.Migrations._2015._09._29
{
    [Migration(20150929)]
    public class Main : Migration
    {
        private readonly CreateAccountsTable _createAccountsTable;
        private readonly CreateUrlsTable _createUrlsTable;

        public Main()
        {
            _createUrlsTable = new CreateUrlsTable();
            _createAccountsTable = new CreateAccountsTable();
        }

        public override void Up()
        {
            _createAccountsTable.Up();
            _createUrlsTable.Up();
        }

        public override void Down()
        {
            _createAccountsTable.Down();
            _createUrlsTable.Down();
        }
    }
}