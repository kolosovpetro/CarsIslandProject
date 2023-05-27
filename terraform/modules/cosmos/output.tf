output "cosmos_connection_string" {
  value = "AccountEndpoint=${azurerm_cosmosdb_account.public.endpoint};AccountKey=${azurerm_cosmosdb_account.public.primary_key};"
  sensitive   = true
}