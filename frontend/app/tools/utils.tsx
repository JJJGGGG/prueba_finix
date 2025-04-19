import type { Invoice } from "./types"

export function formatDate(date: string | undefined) {
    if (date === undefined) {
        return "No Date"
    }
    return new Date(date).toLocaleDateString("es-cl")
}

export function formatPaymentStatus(status?: Invoice["payment_status"]) {
    if(status == "paid") {
        return "Pagada"
    } else if (status == "overdue") {
        return "Vencida"
    } else if (status == "pending") {
        return "Pendiente"
    }
    return "No hay datos";
}

export function formatInvoiceStatus(status?: Invoice["invoice_status"]){
    if(status == "cancelled") {
        return "Cancelada"
    } else if (status == "issued") {
        return "Emitida"
    } else if (status == "partial") {
        return "Parcial"
    }
    return "No hay datos";
}

export function getMaxCreditNoteAmount(invoice?: Invoice) {
    if(!invoice) {
        return 0
    }
    const total_amount = invoice.total_amount;
    const already_paid = invoice.invoice_credit_note.map((cn) => cn.credit_note_amount).reduce((a, b) => a+b, 0)

    return total_amount - already_paid;
}