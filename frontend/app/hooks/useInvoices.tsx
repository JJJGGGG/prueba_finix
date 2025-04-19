import { useEffect, useState } from "react";
import type { Invoice } from "~/tools/types";

export default function useInvoices(
    invoice_status?: Invoice["invoice_status"],
    payment_status?: Invoice["payment_status"]
): [Invoice[]] {
    const [invoices, setInvoices] = useState<Invoice[]>([]);

    async function fetchInvoices(invoice_status?: Invoice["invoice_status"], payment_status?: Invoice["payment_status"]) {
        let url = `${import.meta.env.VITE_BACKEND_URL}/Invoice`;
        const urldict: Record<string, string> = {}
        if(invoice_status) { 
            urldict["invoice_status"] = invoice_status;
        }
        if(payment_status) {
            urldict["payment_status"] = payment_status;
        }
        if(Object.keys(urldict).length) {
            const urlparams = new URLSearchParams(urldict).toString()
            url = `${url}?${urlparams}`
        }
        const invoices = await fetch(url).then((res) => res.json());

        console.log(invoices);

        setInvoices(invoices);

    }

    useEffect(() => {
        fetchInvoices(invoice_status, payment_status);
    }, [invoice_status, payment_status])

    return [invoices];
}