# TransactionsTestTask

Create a .NET Core API service for managing transactions. It allows users to import a list of transactions from an Excel file, update the status of transactions, and export a filtered list of transactions to a CSV file.

## Functionality

- Users can access the service after authorization. The type and sequence of actions are determined by the executor at their discretion.
- All methods must be protected from unauthorized access. Only authorized users can access the API.
- When a .net file is uploaded, the content is processed, and the data is added to the database based on the transaction_id present in the Excel file. If a record with the same transaction_id does not exist in the database, it is added. If a record with the same transaction_id exists, the transaction status is updated.
- When exporting to CSV, the API will provide a file with basic transaction information (columns selected by the executor) based on the chosen filters (transaction type, status).
- The list of transactions endpoint allows users to filter transactions by type (multiple types may be selected simultaneously), status (only one status can be selected), and perform a text search for clients based on their name.
- Users can update the transaction status using the transaction id.

## General Requirements for .NET Core:

- Avoid using automappers.
- Use either services or CQS/CQRS (e.g., through Mediatr).
- Create an informational page using Swagger for API testing and documentation.
- Utilize Entity Framework for database migrations.
- Avoid using UnitOfWork and/or Repository patterns.
- Upload the project to GitHub.
- Use ORM (e.g., Entity Framework) for approximately 20% of database queries and 80% of queries using raw SQL.
- All comments, code, and general information in the project should be in English.
