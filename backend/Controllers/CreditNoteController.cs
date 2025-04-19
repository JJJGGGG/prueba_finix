using Microsoft.AspNetCore.Mvc;
using test_finix.Models;
using test_finix.Services;
using test_finix.Tools;
using test_finix.ViewModels;

namespace test_finix.Controllers;

[ApiController]
[Route("Invoice/{invoiceNumber}/CreditNote")]
public class CreditNoteController : ControllerBase
{
    private readonly ILogger<CreditNoteController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly INewCreditNoteValidator _creditNoteValidator;
    public CreditNoteController(
        ILogger<CreditNoteController> logger, 
        ApplicationDbContext context,
        INewCreditNoteValidator creditNoteValidator
    )
    {
        _logger = logger;
        _context = context;
        _creditNoteValidator = creditNoteValidator;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<InvoiceCreditNote>> Get(int invoiceNumber)
    {
        IEnumerable<InvoiceCreditNote> cns = _context.invoice_credit_notes.Where((cn) => cn.invoiceId == invoiceNumber);

        return Ok(cns);
    }
    
    [HttpPost]
    public ActionResult<InvoiceCreditNote> Post(int invoiceNumber, CreditNoteViewModel creditNote)
    {
        InvoiceCreditNote note = new InvoiceCreditNote {
            invoiceId = invoiceNumber,
            credit_note_date = DateTime.Now,
            credit_note_amount = creditNote.credit_note_amount,
        };

        if (!_creditNoteValidator.ValidateNewCreditNote(note)) {
            return BadRequest();
        }

        _context.invoice_credit_notes.Add(note);
        _context.SaveChanges();

        return Ok(note);

    }
}