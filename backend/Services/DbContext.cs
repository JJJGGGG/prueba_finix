using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using test_finix.Models;
using test_finix.Services;

namespace test_finix.Services;


public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Customer> customers { get; set; }
    public virtual DbSet<Invoice> invoices { get; set; }
    public virtual DbSet<InvoiceCreditNote> invoice_credit_notes { get; set; }
    public virtual DbSet<InvoicePayment> invoice_payments { get; set; }
    public virtual DbSet<ProductDetail> product_details { get; set; }
    string SQL_FILE_PATH = "../data/data.db";
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + SQL_FILE_PATH);
    }
}