import { Link, useParams } from "react-router-dom";
import { Fragment } from "react/jsx-runtime";
import useInvoice from "~/hooks/useInvoice";
import { formatDate, formatInvoiceStatus, formatPaymentStatus } from "~/tools/utils";

export default function Invoice() {
    const {invoice_number} = useParams()

    const [invoice] = useInvoice(invoice_number)

    return (
        <div>
            <div className="flex justify-between"><div className="text-2xl">Factura N° {invoice?.invoice_number}</div> <Link to="/" className="default-link">Volver</Link></div>
            <div className="mt-4 text-xl">Información general</div>
            <div className="px-4 grid grid-cols-2 lg:w-1/3">
                <div className="font-bold">Fecha</div> <div>{formatDate(invoice?.invoice_date)}</div>
                <div className="font-bold">Fecha vencimiento</div> <div>{formatDate(invoice?.payment_due_date || "")}</div>
                <div className="font-bold">Días hasta vencimiento</div> <div>{invoice?.days_to_due}</div>
                <div className="font-bold">Estado de la factura</div> <div>{formatInvoiceStatus(invoice?.invoice_status)}</div>
                <div className="font-bold">Estado del pago</div> <div>{formatPaymentStatus(invoice?.payment_status)}</div>
                <div className="font-bold">Total de la factura</div> <div>{invoice?.total_amount}</div>
            </div>
            <div className="mt-4 text-xl">Cliente</div>
            <div className="px-4 grid grid-cols-2 lg:w-1/3">
                <div className="font-bold">Nombre</div> <div>{invoice?.customer.customer_name}</div>
                <div className="font-bold">Email</div> <div>{invoice?.customer.customer_email}</div>
                <div className="font-bold">RUN</div> <div>{invoice?.customer.customer_run}</div>
            </div>
            <div className="mt-4 text-xl">Pago</div>
            <div className="px-4 grid grid-cols-2 lg:w-1/3">
            {
                invoice?.invoice_payment?.payment_method && (
                    <>
                        <div className="font-bold">Nombre</div> <div>{formatDate(invoice?.invoice_payment?.payment_date)}</div>
                        <div className="font-bold">Email</div> <div>{invoice?.invoice_payment?.payment_method}</div>
                    </>
                )
            }
            {!invoice?.invoice_payment?.payment_method && (
                <div className="col-span-2">No hay pagos a la fecha</div>
            )}
            </div>
            <div className="mt-4 text-xl">Productos</div>
            <div className="grid grid-cols-4">
                <div className="font-bold">Nombre</div>
                <div className="font-bold">Cantidad</div>
                <div className="font-bold">Precio por unidad</div>
                <div className="font-bold">Subtotal</div>
                {invoice?.invoice_detail.map((prod) => (
                    <Fragment key={prod.id}>
                        <div>{prod.product_name}</div>
                        <div>{prod.quantity}</div>
                        <div>{prod.unit_price}</div>
                        <div>{prod.subtotal}</div>
                    </Fragment>
                ))}
                {!invoice?.invoice_detail.length && (
                    <div className="col-span-4">No hay productos</div>
                )}
            </div>
            <div className="mt-4 text-xl">Notas de crédito</div>
            <div className="grid grid-cols-3">
                <div className="font-bold">Nota de cŕedito N°</div>
                <div className="font-bold">Fecha</div>
                <div className="font-bold">Monto</div>
                {invoice?.invoice_credit_note.map((cn) => (
                    <Fragment key={cn.credit_note_number}>
                        <div>{cn.credit_note_number}</div>
                        <div>{formatDate(cn.credit_note_date)}</div>
                        <div>{cn.credit_note_amount}</div>
                    </Fragment>
                ))}
                {!invoice?.invoice_credit_note.length && (
                    <div className="col-span-3">No hay notas de crédito</div>
                )}
                <Link to={`/invoices/${invoice_number}/create_cn`} className="default-link">Crear nueva nota de crédito</Link>
            </div>
        </div>
    )
}