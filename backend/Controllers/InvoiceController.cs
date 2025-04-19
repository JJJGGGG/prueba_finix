using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_finix.Models;
using test_finix.Services;
using test_finix.ViewModels;

namespace test_finix.Controllers;

[ApiController]
[Route("Invoice")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IInvoiceValidator _invoiceValidator;
    public InvoiceController(
        ILogger<InvoiceController> logger,
        ApplicationDbContext context,
        IInvoiceValidator invoiceValidator
    )
    {
        _logger = logger;
        _context = context;
        _invoiceValidator = invoiceValidator;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Invoice>> Get(string? payment_status, string? invoice_status )
    {
        if(!_invoiceValidator.validatePaymentStatus(payment_status, true)) {
            return BadRequest("The payment status in the query is invalid");
        }
        if(!_invoiceValidator.validateInvoiceStatus(invoice_status, true)) {
            return BadRequest("The payment status in the query is invalid");
        }
        IEnumerable<Invoice> invoices = _context.invoices;

        if(payment_status != null) {
            invoices = invoices.Where((inv) => inv.payment_status == payment_status);
        }

        if(invoice_status != null) {
            invoices = invoices.Where((inv) => inv.invoice_status == invoice_status);
        }

        return Ok(invoices);
    }
    [HttpGet("{invoiceNumber}")]
    public ActionResult<Invoice> GetInvoice(int invoiceNumber)
    {
        Invoice? invoice = _context.invoices
        .Where((inv) => inv.invoice_number == invoiceNumber)
        .Include((inv) => inv.invoice_credit_note)
        .Include((inv) => inv.invoice_detail)
        .Include((inv) => inv.customer)
        .Include((inv) => inv.invoice_payment)
        .FirstOrDefault();
        if(invoice == null) {
            return NotFound();
        }
        return Ok(invoice);
    }
}
