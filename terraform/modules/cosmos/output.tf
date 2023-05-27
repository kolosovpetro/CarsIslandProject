output "cosmos_connection_string" {
  value     = azurerm_cosmosdb_account.public.primary_readonly_sql_connection_string
  sensitive = true
}