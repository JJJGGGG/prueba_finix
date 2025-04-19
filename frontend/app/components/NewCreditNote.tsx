import { useState, type ChangeEvent } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import useInvoice from "~/hooks/useInvoice";
import { getMaxCreditNoteAmount } from "~/tools/utils";

export default function NewCreditNote() {
    const {invoice_number} = useParams();
    const [creditNoteAmount, setCreditNoteAmount] = useState<number>();
    const [error, setError] = useState("");
    const [invoice] = useInvoice(invoice_number);

    const navigate = useNavigate()

    function changeInvoiceAmount(e: ChangeEvent<HTMLInputElement>) {
        const val = e.target.value;
        if(val === "") {
            setCreditNoteAmount(undefined);
        }
        if(!isNaN(Number(val)) && !isNaN(parseInt(val))) {
            setCreditNoteAmount(Number(val))
        }
    }

    async function addCreditNote() {
        const url = `${import.meta.env.VITE_BACKEND_URL}/Invoice/${invoice_number}/CreditNote`
        const req = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                credit_note_amount: creditNoteAmount
            })
        })

        if(req.status == 400) {
            setError("El monto supera el monto máximo")
        } else {
            navigate(`/invoices/${invoice_number}`)
        }
    }
    return (
    <div>
        <div className="flex justify-between">
            <div className="text-2xl">Nueva nota de crédito para factura N° {invoice_number}</div>
            <div><Link to={`/invoices/${invoice_number}`} className="default-link">Volver</Link></div>
        </div>
        <div className="text-xl mt-4">Datos</div>
        <div className="grid grid-cols-2 lg:w-1/3">
            <div>Monto (máx {getMaxCreditNoteAmount(invoice)})</div>
            <div><input className="input" value={creditNoteAmount ?? ""} onChange={changeInvoiceAmount}/> <div className="text-red-600">{error}</div></div>
        </div>
        <button className="default-link mt-2" onClick={addCreditNote}>Agregar</button>
    </div>
    )
}