using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace test_finix.Models;

public class InvoiceCreditNote
{
    [Key]
    public int credit_note_number { get; set; }
    public DateTime credit_note_date { get; set; }
    public int credit_note_amount { get; set; }
    public int invoiceId { get; set; }
    //public Invoice invoice { get; set; }
}