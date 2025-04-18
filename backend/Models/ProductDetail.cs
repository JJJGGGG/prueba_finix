using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace test_finix.Models;

public class ProductDetail
{
    [Key]
    public int Id { get; set; }
    public required string product_name { get; set; }
    public int unit_price { get; set; }
    public int quantity { get; set; }
    public int subtotal { get; set; }
    public int invoiceId { get; set; }
    public required Invoice invoice { get; set; }
}