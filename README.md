# tradeplatform-daera-certificate-store-service

> a.k.a `Defra.Trade.API.CertificatesStore`

# Setup

To run this webapp, you will need a `.\src\Defra.Trade.API.CertificatesStore\appsettings.Development.json` file. The file will need the following settings:

```jsonc 
{
  "ConnectionStrings": {
    "sql_db": "<secret>",
    "sql_db_ef": "<secret>"
  },
  "CommonSql": {
    "UseAzureAccessToken": false
  },
  "DetailedErrors": true,
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Secrets reference can be found here: https://dev.azure.com/defragovuk/DEFRA-TRADE-APIS/_wiki/wikis/DEFRA-TRADE-APIS.wiki/26086
