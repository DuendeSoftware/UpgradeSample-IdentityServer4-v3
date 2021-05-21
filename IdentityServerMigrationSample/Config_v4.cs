using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServerMigrationSample.Migrations.ConfigurationDb
{
    public partial class Config_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = typeof(Program).Assembly;
            using (var s = assembly.GetManifestResourceStream("IdentityServerMigrationSample.ConfigurationDb_v4_delta.sql"))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var sql = sr.ReadToEnd();
                    migrationBuilder.Sql(sql);
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
