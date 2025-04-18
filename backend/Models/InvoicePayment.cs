using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace test_finix.Models;

public class InvoicePayment
{
    [Key]
    public int Id { get; set; }
    public string? payment_method { get; set; }
    public DateTime? payment_date { get; set; }
    public int invoiceId { get; set; }
    public required Invoice invoice { get; set; }
}