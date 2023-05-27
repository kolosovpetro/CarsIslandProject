data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "public" {
  location = var.resource_group_location
  name     = "${var.resource_group_name}-${var.prefix}"
}

module "acr" {
  source                  = "./modules/container-registry"
  acr_location            = azurerm_resource_group.public.location
  acr_name                = "${var.acr_name}${var.prefix}"
  acr_resource_group_name = azurerm_resource_group.public.name
  acr_sku                 = var.acr_sku
}

module "key_vault" {
  source                               = "./modules/key-vault"
  key_vault_location                   = azurerm_resource_group.public.location
  key_vault_name                       = "${var.key_vault_name}${var.prefix}"
  key_vault_object_id                  = data.azurerm_client_config.current.object_id
  key_vault_resource_group_name        = azurerm_resource_group.public.name
  key_vault_sku_name                   = var.key_vault_sku_name
  key_vault_soft_delete_retention_days = 7
  key_vault_tenant_id                  = data.azurerm_client_config.current.tenant_id
}

module "storage" {
  source                      = "./modules/storage"
  storage_account_name        = "${var.storage_account_name}${var.prefix}"
  storage_container_name      = var.storage_container_name
  storage_location            = azurerm_resource_group.public.location
  storage_resource_group_name = azurerm_resource_group.public.name
  storage_account_replication = var.storage_account_replication
  storage_account_tier        = var.storage_account_tier
}

module "cosmos" {
  source                     = "./modules/cosmos"
  cosmos_account_name        = "${var.cosmos_account_name}${var.prefix}"
  cosmos_consistency_level   = var.cosmos_consistency_level
  cosmos_kind                = var.cosmos_kind
  cosmos_location            = azurerm_resource_group.public.location
  cosmos_offer_type          = var.cosmos_offer_type
  cosmos_resource_group_name = azurerm_resource_group.public.name
}

module "keyvault-secrets" {
  source                    = "./modules/keyvault-secrets"
  cosmos_connection_string  = module.cosmos.cosmos_connection_string
  keyvault_id               = module.key_vault.key_vault_id
  storage_account_name      = module.storage.storage_account_name
  storage_connection_string = module.storage.storage_connection_string
  storage_container_name    = module.storage.storage_container_name
  storage_primary_key       = module.storage.storage_primary_key

  depends_on = [
    module.cosmos.cosmos_connection_string,
    module.key_vault.key_vault_id,
    module.storage.storage_account_name,
    module.storage.storage_connection_string,
    module.storage.storage_container_name,
    module.storage.storage_primary_key,
  ]
}