using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace test_finix.Models;

public class Invoice
{
    [Key]
    public int invoice_number { get; set; }
    public DateTime invoice_date { get; set; }
    public required string invoice_status { get; set; }
    public int total_amount { get; set; }
    public int days_to_due { get; set; }
    public DateTime payment_due_date { get; set; }
    public required string payment_status { get; set; }
    public List<ProductDetail> invoice_detail { get; set; } = new();
    public InvoicePayment invoice_payment { get; set; }
    public List<InvoiceCreditNote> invoice_credit_note { get; set; } = new();
    public Customer customer { get; set; }
    public required string customerId { get; set; }

}