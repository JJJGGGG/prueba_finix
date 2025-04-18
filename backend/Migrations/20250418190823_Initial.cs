using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test_finix.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_run = table.Column<string>(type: "TEXT", nullable: false),
                    customer_email = table.Column<string>(type: "TEXT", nullable: false),
                    customer_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_run);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    invoice_number = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    invoice_status = table.Column<string>(type: "TEXT", nullable: false),
                    total_amount = table.Column<int>(type: "INTEGER", nullable: false),
                    days_to_due = table.Column<int>(type: "INTEGER", nullable: false),
                    payment_due_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    payment_status = table.Column<string>(type: "TEXT", nullable: false),
                    customerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.invoice_number);
                    table.ForeignKey(
                        name: "FK_invoices_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "customer_run",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_credit_notes",
                columns: table => new
                {
                    credit_note_number = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    credit_note_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    credit_note_amount = table.Column<int>(type: "INTEGER", nullable: false),
                    invoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_credit_notes", x => x.credit_note_number);
                    table.ForeignKey(
                        name: "FK_invoice_credit_notes_invoices_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "invoices",
                        principalColumn: "invoice_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    payment_method = table.Column<string>(type: "TEXT", nullable: true),
                    payment_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    invoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_payments_invoices_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "invoices",
                        principalColumn: "invoice_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    product_name = table.Column<string>(type: "TEXT", nullable: false),
                    unit_price = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    subtotal = table.Column<int>(type: "INTEGER", nullable: false),
                    invoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_details_invoices_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "invoices",
                        principalColumn: "invoice_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_credit_notes_invoiceId",
                table: "invoice_credit_notes",
                column: "invoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_payments_invoiceId",
                table: "invoice_payments",
                column: "invoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_customerId",
                table: "invoices",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_product_details_invoiceId",
                table: "product_details",
                column: "invoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoice_credit_notes");

            migrationBuilder.DropTable(
                name: "invoice_payments");

            migrationBuilder.DropTable(
                name: "product_details");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
