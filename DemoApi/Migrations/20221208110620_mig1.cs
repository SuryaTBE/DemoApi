using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApi.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminTbls",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "101, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminTbls", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "MovieTbls",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "201, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTbls", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "UserTbls",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "301, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTbls", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BookingTbl",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "401, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    On = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NoOfTickets = table.Column<int>(type: "int", nullable: false),
                    SeatNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTbl", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookingTbl_MovieTbls_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieTbls",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTbl_UserTbls_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTbls",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderMasterTbls",
                columns: table => new
                {
                    OrderMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "501, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardNo = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasterTbls", x => x.OrderMasterId);
                    table.ForeignKey(
                        name: "FK_OrderMasterTbls_UserTbls_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTbls",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "601, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    OrderMasterId = table.Column<int>(type: "int", nullable: false),
                    MovieDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfTickets = table.Column<int>(type: "int", nullable: false),
                    SeatNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    usertblUserId = table.Column<int>(type: "int", nullable: true),
                    bookingtblBookingId = table.Column<int>(type: "int", nullable: true),
                    OrderMasterTblOrderMasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_BookingTbl_bookingtblBookingId",
                        column: x => x.bookingtblBookingId,
                        principalTable: "BookingTbl",
                        principalColumn: "BookingId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderMasterTbls_OrderMasterTblOrderMasterId",
                        column: x => x.OrderMasterTblOrderMasterId,
                        principalTable: "OrderMasterTbls",
                        principalColumn: "OrderMasterId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_UserTbls_usertblUserId",
                        column: x => x.usertblUserId,
                        principalTable: "UserTbls",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTbl_MovieId",
                table: "BookingTbl",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTbl_UserId",
                table: "BookingTbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_bookingtblBookingId",
                table: "OrderDetails",
                column: "bookingtblBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderMasterTblOrderMasterId",
                table: "OrderDetails",
                column: "OrderMasterTblOrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_usertblUserId",
                table: "OrderDetails",
                column: "usertblUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasterTbls_UserId",
                table: "OrderMasterTbls",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminTbls");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "BookingTbl");

            migrationBuilder.DropTable(
                name: "OrderMasterTbls");

            migrationBuilder.DropTable(
                name: "MovieTbls");

            migrationBuilder.DropTable(
                name: "UserTbls");
        }
    }
}

