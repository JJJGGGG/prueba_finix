import { useEffect, useState } from "react";
import type { Invoice } from "~/tools/types";

export default function useInvoice(invoice_number?: string): [Invoice | undefined] {
    const [invoice, setInvoice] = useState<Invoice>();

    async function getInvoice(invoice_number: string) {
        const url = `${import.meta.env.VITE_BACKEND_URL}/Invoice/${invoice_number}`

        const invoice = await fetch(url).then((res) => res.json());

        setInvoice(invoice);
    }

    useEffect(() => {
        if(invoice_number) {
            getInvoice(invoice_number)
        }
    }, [invoice_number]);

    return [invoice]
}