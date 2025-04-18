using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace test_finix.Models;

public class Customer
{
    [Key]
    public required string customer_run { get; set; }
    public required string customer_email { get; set; }
    public required string customer_name { get; set; }
    public List<Invoice> invoices { get; set;} = new();

}