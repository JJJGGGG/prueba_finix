import json
import logging


class DataLoader:
    def __init__(self, path):
        self.path = path
        self.customers = []
        self.invoices = []
        self.inv_details = []
        self.credit_notes = []
        self.payments = []
        self.load_data()

    def verify_total_amount(self, details, total_amount):
        total = sum(map(lambda detail: detail["subtotal"], details))

        return total == total_amount

    def verify_credit_notes(self, ncs, total_amount):
        total = sum(map(lambda nc: nc["credit_note_amount"], ncs))

        return total <= total_amount

    def load_data(self):
        with open(self.path) as file:
            self.data = json.load(file)

    def get_invoice_details(self, inv):
        return [
            {
                **product,
                "invoiceId": inv["invoice_number"]
            } for product in inv["invoice_detail"]
        ]

    def get_invoice_credit_notes(self, inv):
        return [
            {
                **cn,
                "invoiceId": inv["invoice_number"]
            } for cn in inv["invoice_credit_note"]
        ]

    def get_invoice_payments(self, inv):
        return {
            **inv["invoice_payment"],
            "invoiceId": inv["invoice_number"]
        }

    def build_customers(self):
        customers = dict()
        for invoice in self.data["invoices"]:
            customer = invoice["customer"]
            if customer["customer_run"] in customers:
                logging.error(
                    f"Cliente {customer['customer_run']} fue descartado"
                    " porque su rut ya fue encontrado previamente")
            customers[customer["customer_run"]] = customer

        self.customers = list(customers.values())

    def build_invoices(self):
        for inv in self.data["invoices"]:
            invoice = {
                "customerId": inv["customer"]["customer_run"],
                "invoice_number": inv["invoice_number"],
                "invoice_date": inv["invoice_date"],
                "invoice_status": inv["invoice_status"],
                "total_amount": inv["total_amount"],
                "days_to_due": inv["days_to_due"],
                "payment_due_date": inv["payment_due_date"],
                "payment_status": inv["payment_status"],
            }
            invoice_details = self.get_invoice_details(inv)

            if not self.verify_total_amount(
                    invoice_details,
                    invoice["total_amount"]):
                logging.error(
                    f"Factura {invoice['invoice_number']} fue descartada"
                    " por inconsistencia entre subtotales y total_amount"
                )
                continue  # No agregamos esta factura

            credit_notes = self.get_invoice_credit_notes(inv)

            if not self.verify_credit_notes(
                    credit_notes,
                    invoice["total_amount"]):
                logging.error(
                    f"Factura {invoice['invoice_number']} fue descartada"
                    " por inconsistencia entre notas de credito y total_amount"
                )
                continue  # No agregamos esta factura
            payment = self.get_invoice_payments(inv)

            logging.info(
                f"Validados datos para factura {invoice['invoice_number']}!"
            )

            self.invoices.append(invoice)
            self.inv_details.extend(invoice_details)
            self.credit_notes.extend(credit_notes)
            self.payments.append(payment)

    def build(self):
        self.build_customers()
        self.build_invoices()

        logging.info("Terminados de validar los datos del archivo.")

    def get_data(self):
        return {
            "customers": self.customers,
            "invoices": self.invoices,
            "details": self.inv_details,
            "credit_notes": self.credit_notes,
            "payments": self.payments,
        }
