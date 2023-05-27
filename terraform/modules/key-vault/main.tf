resource "azurerm_key_vault" "public" {
  name                        = var.key_vault_name
  location                    = var.key_vault_location
  resource_group_name         = var.key_vault_resource_group_name
  enabled_for_disk_encryption = false
  tenant_id                   = var.key_vault_tenant_id
  soft_delete_retention_days  = var.key_vault_soft_delete_retention_days
  purge_protection_enabled    = false

  sku_name = var.key_vault_sku_name

  access_policy {
    tenant_id = var.key_vault_tenant_id
    object_id = var.key_vault_object_id

    key_permissions = [
      "Create",
      "Get",
      "List"
    ]

    secret_permissions = [
      "Set",
      "Get",
      "List",
      "Delete",
      "Purge",
      "Recover"
    ]
  }
}