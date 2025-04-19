import { Link } from "react-router-dom";
import useInvoices from "~/hooks/useInvoices";
import type { Invoice } from "~/tools/types";
import { formatDate, formatInvoiceStatus, formatPaymentStatus } from "~/tools/utils";

function DisplayInvoice({invoice}: {invoice: Invoice}) {
    return (
        <div className="border rounded px-4 py-2 w-80">
            <div>Factura N° {invoice.invoice_number} 
                <span className="bg-gray-300 px-2 rounded mr-2 ml-2">{formatInvoiceStatus(invoice.invoice_status)}</span>
                <span className="bg-gray-300 px-2 rounded">{formatPaymentStatus(invoice.payment_status)}</span>
            </div>
            <div>
                Fecha: {formatDate(invoice.invoice_date)}
            </div>
            <div>
                Cliente: {invoice.customerId}
            </div>
            <div>
                <Link to={`/invoices/${invoice.invoice_number}`} className="default-link">Ver Detalles</Link>
            </div>
        </div>
    )
}

export default function Invoices() {
    const [invoices] = useInvoices();
    return (
        <div className="flex flex-col gap-4">
            {invoices?.map((invoice) => (
                <DisplayInvoice invoice={invoice} />
            ))}
        </div>
    );
}