using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPayment.Migrations
{
    public partial class paymentStateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentState",
                columns: new[] { "PaymentStateId", "PaymentStateDescription" },
                values: new object[] { 1, "pending" });

            migrationBuilder.InsertData(
                table: "PaymentState",
                columns: new[] { "PaymentStateId", "PaymentStateDescription" },
                values: new object[] { 2, "processed" });

            migrationBuilder.InsertData(
                table: "PaymentState",
                columns: new[] { "PaymentStateId", "PaymentStateDescription" },
                values: new object[] { 3, "failed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentState",
                keyColumn: "PaymentStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentState",
                keyColumn: "PaymentStateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentState",
                keyColumn: "PaymentStateId",
                keyValue: 3);
        }
    }
}
