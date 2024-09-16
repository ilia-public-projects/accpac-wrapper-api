# ACCPAC Wrapper API

## Overview

**ACCPAC Wrapper API** is a .NET 6 project that simplifies integration with the Sage 300 (ACCPAC) ERP system. This API serves as a bridge between Sage 300 and external systems, providing a wrapper for common operations such as creating and updating records, as well as querying data from Sage 300.

This project demonstrates how **ACCPAC COMAPI** can be used to create transactions in Sage 300 using ACCPAC views. The following operations are currently supported:

- **AP Invoice Batch Creation**
- **CB Nominal Cashbook Batch Creation**
- **CB Accounts Payable Cashbook Batch Creation**
- Additional HTTP GET endpoints to retrieve various master data directly from the ACCPAC database.

Additionally, the API includes examples of how to validate master data, either using **ACCPAC COMAPI** or directly querying the ACCPAC database:

- COMAPI Example: **ICommonServicesValidator.ValidateFiscalDate** validates whether a fiscal period exists and is open using the **Accpac Fiscal Calendar**.
- Database Example: **ICommonServicesValidator** has methods to validate currencies and optional fields directly against the database.

---

## Features

- **Simplified Sage 300 Integration**: A wrapper for ACCPAC COMAPI operations like invoice creation, customer management, and general ledger entry.
- **RESTful API**: Exposes Sage 300 functionality through a modern, easy-to-use REST API.
- **Security**: Uses a security key stored in `appsettings.json` to validate each HTTP request. This can be extended to use token-based validation or other authentication mechanisms of your choice.
- **Extensible**: The modular architecture allows for easy addition of new endpoints and Sage 300 operations.
- **Error Handling**: Comprehensive error reporting and logging to assist in troubleshooting.

---

## Technology Stack

- **Backend**:
  - .NET 6
  - ASP.NET Core
  - Sage 300 ERP SDK
  - Entity Framework Core
- **Database**:
  - SQL Server (for application data)
- **Authentication**:
  - Token-based authentication (JWT)

---

## Installation and Setup

### Note:
- The necessary ACCPAC DLLs are included in the project under **Wrapper.Accpac**.
- This solution has been tested on an IIS server with ACCPAC installed. Ensure IIS has access to the ACCPAC folder.
- **Important**: HTTP POST requests use 1 **LAN pack** during execution, as they require an ACCPAC session. However, HTTP GET requests use raw SQL queries and do not consume a LAN pack. To avoid using a LAN pack, send an HTTP header (`X-OperationType: SQL`) with the request.

### Prerequisites

Make sure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Sage 300 ERP SDK](https://www.sage.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (or other compatible databases)

### Steps to Run Locally

1. **Clone the repository**:
    ```bash
    git clone https://github.com/ilia-public-projects/accpac-wrapper-api.git
    cd accpac-wrapper-api
    ```

2. **Configure Sage 300 Connection**:  
   Update the `appsettings.json` file to include the connection settings for your Sage 300 instance.

   Example:

   ```json
   {
     "AppSettings": {
       "ApiAuthSecret": "",        // Communication secret; every request is validated against this value.
       "AccpacUsername": "",       // Accpac username (e.g., Admin)
       "AccpacPassword": "",       // Accpac password (e.g., Admin)
       "AccpacVersion": "",        // Accpac version
       "AccpacCompany": "",        // Accpac company name
       "AccpacDbConnectionString": "",  // Accpac database connection string (required for HTTP GET endpoints)
       "NotificationSystemApiUri": "",  // Optional endpoint to communicate progress of HTTP POST requests
       "NotificationSystemApiAuthSecret": ""  // Optional secret for the notification system endpoint
     }
   }
