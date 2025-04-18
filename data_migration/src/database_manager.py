import logging
import sqlite3


class DatabaseManager:
    def __init__(self, dbpath):
        self.dbpath = dbpath
        self.con = None

    def __enter__(self):
        self.con = sqlite3.connect(self.dbpath)
        self.cur = self.con.cursor()
        return self
    
    def __exit__(self, type, value, traceback):
        self.con.commit()
        self.con.close()
        logging.info("Datos guardados correctamente en la base de datos!")

    def drop_values(self):
        self.cur.execute("DELETE FROM customers")
        self.cur.execute("DELETE FROM invoices")
        self.cur.execute("DELETE FROM product_details")
        self.cur.execute("DELETE FROM invoice_credit_notes")
        self.cur.execute("DELETE FROM invoice_payments")

    def save_customers(self, customers):
        logging.info("Guardando customers...")
        data = [
            (
                customer["customer_run"],
                customer["customer_name"],
                customer["customer_email"]
            )
            for customer in customers
        ]
        self.cur.executemany(
            "INSERT INTO customers "
            "("
            "customer_run, "
            "customer_name, "
            "customer_email)"
            " VALUES(?, ?, ?)",
            data
        )
        logging.info("Guardados customers!")

    def save_invoices(self, invoices):
        logging.info("Guardando invoices...")
        data = [
            (
                invoice["invoice_number"],
                invoice["invoice_date"],
                invoice["invoice_status"],
                invoice["total_amount"],
                invoice["days_to_due"],
                invoice["payment_due_date"],
                invoice["payment_status"],
                invoice["customerId"],
            )
            for invoice in invoices
        ]
        self.cur.executemany(
            "INSERT INTO invoices "
            "("
            "invoice_number, "
            "invoice_date, "
            "invoice_status,"
            "total_amount,"
            "days_to_due,"
            "payment_due_date,"
            "payment_status,"
            "customerId"
            ")"
            " VALUES(?, ?, ?, ?, ?, ?, ?, ?)",
            data
        )

        logging.info("Guardados invoices!")

    def save_details(self, details):
        logging.info("Guardando details...")
        data = [
            (
                detail["product_name"],
                detail["unit_price"],
                detail["quantity"],
                detail["subtotal"],
                detail["invoiceId"],
            )
            for detail in details
        ]
        self.cur.executemany(
            "INSERT INTO product_details "
            "("
            "product_name, "
            "unit_price, "
            "quantity,"
            "subtotal,"
            "invoiceId"
            ")"
            " VALUES(?, ?, ?, ?, ?)",
            data
        )

        logging.info("Guardados details!")
        pass

    def save_credit_notes(self, credit_notes):
        logging.info("Guardando credit notes...")
        data = [
            (
                note["credit_note_number"],
                note["credit_note_date"],
                note["credit_note_amount"],
                note["invoiceId"],
            )
            for note in credit_notes
        ]
        self.cur.executemany(
            "INSERT INTO invoice_credit_notes "
            "("
            "credit_note_number, "
            "credit_note_date, "
            "credit_note_amount,"
            "invoiceId"
            ")"
            " VALUES(?, ?, ?, ?)",
            data
        )

        logging.info("Guardados credit notes!")
        pass

    def save_payments(self, payments):
        logging.info("Guardando payments...")
        data = [
            (
                payment["payment_method"],
                payment["payment_date"],
                payment["invoiceId"],
            )
            for payment in payments
        ]
        self.cur.executemany(
            "INSERT INTO invoice_payments "
            "("
            "payment_method, "
            "payment_date, "
            "invoiceId"
            ")"
            " VALUES(?, ?, ?)",
            data
        )
        logging.info("Guardados payments!")
        pass
