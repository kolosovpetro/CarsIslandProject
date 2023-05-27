resource "azurerm_key_vault_secret" "cosmos_connection_string" {
  name         = "CosmosConnectionString"
  value        = var.cosmos_connection_string
  key_vault_id = var.keyvault_id
}

resource "azurerm_key_vault_secret" "storage_connection_string" {
  name         = "BlobConnectionString"
  value        = var.storage_connection_string
  key_vault_id = var.keyvault_id
}

resource "azurerm_key_vault_secret" "storage_account_name" {
  name         = "BlobAccountName"
  value        = var.storage_account_name
  key_vault_id = var.keyvault_id
}

resource "azurerm_key_vault_secret" "storage_primary_key" {
  name         = "BlobKey"
  value        = var.storage_primary_key
  key_vault_id = var.keyvault_id
}

resource "azurerm_key_vault_secret" "storage_container_name" {
  name         = "BlobContainerName"
  value        = var.storage_container_name
  key_vault_id = var.keyvault_id
}

