using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labs.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "Latin1_General_CI_AS"),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "TrainType",
                columns: table => new
                {
                    TrainTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainType", x => x.TrainTypeId);
                });

            migrationBuilder.CreateTable(
                name: "WagonType",
                columns: table => new
                {
                    WagonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WagonTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    Surcharge = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WagonType", x => x.WagonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, collation: "Latin1_General_CI_AS"),
                    TrainTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.TrainId);
                    table.ForeignKey(
                        name: "FK_Train_TrainType_TrainTypeId",
                        column: x => x.TrainTypeId,
                        principalTable: "TrainType",
                        principalColumn: "TrainTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wagon",
                columns: table => new
                {
                    WagonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WagonNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, collation: "Latin1_General_CI_AS"),
                    WagonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wagon", x => x.WagonId);
                    table.ForeignKey(
                        name: "FK_Wagon_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wagon_WagonType_WagonTypeId",
                        column: x => x.WagonTypeId,
                        principalTable: "WagonType",
                        principalColumn: "WagonTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WagonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrgencySurcharge = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Train_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Train",
                        principalColumn: "TrainId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Wagon_WagonId",
                        column: x => x.WagonId,
                        principalTable: "Wagon",
                        principalColumn: "WagonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Destination",
                columns: new[] { "DestinationId", "BasePrice", "DestinationName", "Distance" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), 0m, "Київ", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 450m, "Львів", 540 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 380m, "Одеса", 440 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 400m, "Харків", 480 }
                });

            migrationBuilder.InsertData(
                table: "Passenger",
                columns: new[] { "PassengerId", "Address", "FirstName", "LastName", "MiddleName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "вул. Independece, 20, Київ", "Андрій", "Мельник", "Сергійович", "+380955678901" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "вул. Хрещатик, 1, Київ", "Іван", "Петренко", "Васильович", "+380501234567" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "вул. Шевченка, 5, Львів", "Марія", "Коваленко", "Олександрівна", "+380672345678" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "вул. Дерибасівська, 10, Одеса", "Петро", "Сидоренко", "Іванович", "+380633456789" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "вул. Сумська, 15, Харків", "Ольга", "Тимошенко", "Михайлівна", "+380994567890" }
                });

            migrationBuilder.InsertData(
                table: "TrainType",
                columns: new[] { "TrainTypeId", "TypeName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Швидкісний" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Інтерсіті" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Пасажирський" }
                });

            migrationBuilder.InsertData(
                table: "WagonType",
                columns: new[] { "WagonTypeId", "Surcharge", "WagonTypeName" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), 0m, "Загальний" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 50m, "Плацкартний" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 150m, "Купе" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 300m, "Люкс" }
                });

            migrationBuilder.InsertData(
                table: "Train",
                columns: new[] { "TrainId", "TrainNumber", "TrainTypeId" },
                values: new object[,]
                {
                    { new Guid("20202020-2020-2020-2020-202020202020"), "001IC", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("30303030-3030-3030-3030-303030303030"), "123", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("40404040-4040-4040-4040-404040404040"), "777", new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Wagon",
                columns: new[] { "WagonId", "TrainId", "WagonNumber", "WagonTypeId" },
                values: new object[,]
                {
                    { new Guid("50505050-5050-5050-5050-505050505050"), new Guid("20202020-2020-2020-2020-202020202020"), "1", new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("60606060-6060-6060-6060-606060606060"), new Guid("20202020-2020-2020-2020-202020202020"), "2", new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("70707070-7070-7070-7070-707070707070"), new Guid("30303030-3030-3030-3030-303030303030"), "1", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("80808080-8080-8080-8080-808080808080"), new Guid("30303030-3030-3030-3030-303030303030"), "2", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("90909090-9090-9090-9090-909090909090"), new Guid("40404040-4040-4040-4040-404040404040"), "1", new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "TicketId", "ArrivalDateTime", "DepartureDateTime", "DestinationId", "PassengerId", "TotalPrice", "TrainId", "UrgencySurcharge", "WagonId" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 620m, new Guid("20202020-2020-2020-2020-202020202020"), 20m, new Guid("50505050-5050-5050-5050-505050505050") },
                    { new Guid("a2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 8, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 430m, new Guid("30303030-3030-3030-3030-303030303030"), 0m, new Guid("70707070-7070-7070-7070-707070707070") },
                    { new Guid("b1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 3, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 3, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 600m, new Guid("40404040-4040-4040-4040-404040404040"), 50m, new Guid("90909090-9090-9090-9090-909090909090") },
                    { new Guid("b2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 750m, new Guid("20202020-2020-2020-2020-202020202020"), 0m, new Guid("60606060-6060-6060-6060-606060606060") },
                    { new Guid("c1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 4, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 400m, new Guid("30303030-3030-3030-3030-303030303030"), 20m, new Guid("80808080-8080-8080-8080-808080808080") },
                    { new Guid("c2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 21, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 550m, new Guid("40404040-4040-4040-4040-404040404040"), 0m, new Guid("90909090-9090-9090-9090-909090909090") },
                    { new Guid("d1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 6, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 620m, new Guid("20202020-2020-2020-2020-202020202020"), 20m, new Guid("50505050-5050-5050-5050-505050505050") },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 26, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 450m, new Guid("30303030-3030-3030-3030-303030303030"), 0m, new Guid("70707070-7070-7070-7070-707070707070") },
                    { new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("10101010-1010-1010-1010-101010101010"), 530m, new Guid("40404040-4040-4040-4040-404040404040"), 20m, new Guid("90909090-9090-9090-9090-909090909090") },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 31, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 31, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("10101010-1010-1010-1010-101010101010"), 750m, new Guid("20202020-2020-2020-2020-202020202020"), 0m, new Guid("60606060-6060-6060-6060-606060606060") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destination_DestinationName",
                table: "Destination",
                column: "DestinationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destination_Distance",
                table: "Destination",
                column: "Distance");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_LastName_FirstName",
                table: "Passenger",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DepartureDateTime",
                table: "Ticket",
                column: "DepartureDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DestinationId",
                table: "Ticket",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PassengerId",
                table: "Ticket",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TrainId",
                table: "Ticket",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TrainId_DepartureDateTime",
                table: "Ticket",
                columns: new[] { "TrainId", "DepartureDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_WagonId",
                table: "Ticket",
                column: "WagonId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_TrainNumber",
                table: "Train",
                column: "TrainNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Train_TrainTypeId",
                table: "Train",
                column: "TrainTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainType_TypeName",
                table: "TrainType",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wagon_TrainId_WagonNumber",
                table: "Wagon",
                columns: new[] { "TrainId", "WagonNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wagon_WagonTypeId",
                table: "Wagon",
                column: "WagonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WagonType_WagonTypeName",
                table: "WagonType",
                column: "WagonTypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Wagon");

            migrationBuilder.DropTable(
                name: "Train");

            migrationBuilder.DropTable(
                name: "WagonType");

            migrationBuilder.DropTable(
                name: "TrainType");
        }
    }
}
