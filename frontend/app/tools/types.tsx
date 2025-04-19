export type Invoice = {
    invoice_number: number,
    invoice_date: string,
    invoice_status: "issued" | "partial" | "cancelled",
    total_amount: number,
    days_to_due: number,
    payment_due_date: string,
    payment_status: "overdue" | "pending" | "paid",
    invoice_detail: ProductDetail[],
    invoice_payment?: InvoicePayment,
    invoice_credit_note: CreditNote[],
    customer: Customer,
    customerId: string
}

export type ProductDetail = {
    id: number,
    product_name: string,
    unit_price: number,
    quantity: number,
    subtotal: number,
    invoiceId: number
}

export type InvoicePayment = {
    id: number,
    payment_method?: string,
    payment_date?: string,
    invoiceId: number
}

export type CreditNote = {
    credit_note_number: number,
    credit_note_date: string,
    credit_note_amount: number,
    invoiceId: number,
}

export type Customer = {
    customer_run: string,
    customer_email: string,
    customer_name: string
}