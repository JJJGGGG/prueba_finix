namespace test_finix.ViewModels;

public interface IInvoiceValidator
{
    public bool validatePaymentStatus(string? paymentStatus, bool allow_null);
    public bool validateInvoiceStatus(string? invoiceStatus, bool allow_null);
}

public class InvoiceValidator : IInvoiceValidator
{
    public bool validatePaymentStatus(string? payment_status, bool allow_null=false) {
        if(payment_status == null && allow_null) {
            return true;
        }
        else if (
            payment_status == "pending" ||
            payment_status == "overdue" ||
            payment_status == "partial"
        ) {
            return true;
        }
        return false;
    }

    public bool validateInvoiceStatus(string? invoice_status, bool allow_null=false) {
        if(invoice_status == null && allow_null) {
            return true;
        }
        else if (
            invoice_status == "issued" ||
            invoice_status == "cancelled" ||
            invoice_status == "paid"
        ) {
            return true;
        }
        return false;

    }
}