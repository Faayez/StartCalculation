using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StartCalculation.Services.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Input1 = table.Column<double>(type: "float", nullable: false),
                    Operator = table.Column<byte>(type: "tinyint", nullable: false),
                    Input2 = table.Column<double>(type: "float", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });

            var spInsertCalculations = @"CREATE PROCEDURE [dbo].[InsertCalculations]
                                        @ID UNIQUEIDENTIFIER OUTPUT,
                                        @Status [tinyint],
                                        @Input1 [float],
                                        @Operator [tinyint],
                                        @Input2 [float],
                                        @Result [float],
                                        @CreatedOn [datetime2],
                                        @FinishedOn [datetime2]
                                        AS
                                        SET NOCOUNT ON;
                                        SET @ID = NEWID();  
                                        INSERT INTO Calculations
	                                        (Id, [Status], Input1, Operator, Input2, Result, CreatedOn, FinishedOn)
                                        VALUES
	                                        (@ID, @Status, @Input1, @Operator, @Input2, @Result, @CreatedOn, @FinishedOn)
                                        GO";
            migrationBuilder.Sql(spInsertCalculations);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Calculations");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertCalculations]");
        }
    }
}
