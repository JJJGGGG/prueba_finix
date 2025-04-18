import logging
from src.data_loader import DataLoader
from src.database_manager import DatabaseManager

logging.getLogger().setLevel(logging.INFO)

DATA_FILE_PATH = "./bd_exam.json"
DATABASE_PATH = "../data/data.db"


def main():
    loader = DataLoader(DATA_FILE_PATH)

    loader.build()

    data = loader.get_data()

    with DatabaseManager(DATABASE_PATH) as dm:
        # dm.drop_values()
        dm.save_customers(data["customers"])
        dm.save_invoices(data["invoices"])
        dm.save_details(data["details"])
        dm.save_payments(data["payments"])
        dm.save_credit_notes(data["credit_notes"])


if __name__ == "__main__":
    main()
