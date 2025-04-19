using Microsoft.EntityFrameworkCore;
using test_finix.Models;
using test_finix.Services;

namespace test_finix.Tools;

public interface INewCreditNoteValidator 
{
    public bool ValidateNewCreditNote(InvoiceCreditNote note);
}

public class NewCreditNoteValidator : INewCreditNoteValidator {
    private readonly ILogger<NewCreditNoteValidator> _logger;
    private readonly ApplicationDbContext _context;
    public NewCreditNoteValidator(ILogger<NewCreditNoteValidator> logger, ApplicationDbContext context) {
        _logger = logger;
        _context = context;
    }
    public bool ValidateNewCreditNote(InvoiceCreditNote note) {
        int invoiceId = note.invoiceId;
        Invoice? invoice = _context.invoices
            .Where((inv) => inv.invoice_number == invoiceId)
            .Include((inv) => inv.invoice_credit_note)
            .FirstOrDefault();
        if(invoice == null) {
            return false;
        }
        int cnumber = invoice.invoice_credit_note.Sum((cn) => cn.credit_note_amount);

        if(cnumber + note.credit_note_amount > invoice.total_amount) {
            return false;
        }

        return true;

    }

}