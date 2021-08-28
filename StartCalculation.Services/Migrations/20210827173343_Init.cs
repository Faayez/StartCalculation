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
                    ProcessEstimate = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });

            var spInsertCalculation = @"CREATE PROCEDURE [dbo].[InsertCalculation]
                                        @ID UNIQUEIDENTIFIER OUTPUT,
                                        @Status [tinyint],
                                        @Input1 [float],
                                        @Operator [tinyint],
                                        @Input2 [float],
                                        @CreatedOn [datetime2],
                                        @ProcessEstimate [int]
                                        AS
                                        SET NOCOUNT ON;
                                        SET @ID = NEWID();  
                                        INSERT INTO Calculations
	                                        (Id, [Status], Input1, Operator, Input2, CreatedOn, ProcessEstimate)
                                        VALUES
	                                        (@ID, @Status, @Input1, @Operator, @Input2, @CreatedOn, @ProcessEstimate)
                                        GO";
            migrationBuilder.Sql(spInsertCalculation);

            var spUpdateCalculationResult = @"CREATE PROCEDURE [dbo].[UpdateCalculationResult]
                                        @ID UNIQUEIDENTIFIER,
                                        @Result [float],
                                        @Status [tinyint]
                                        AS
                                        SET NOCOUNT ON;
                                        
                                        UPDATE Calculations
	                                    SET
                                            Result = @Result,
                                            Status = @Status
                                        WHERE
                                            Id = @Id
                                        GO";
            migrationBuilder.Sql(spUpdateCalculationResult);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Calculations");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertCalculation]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateCalculationResult]");
        }
    }
}
