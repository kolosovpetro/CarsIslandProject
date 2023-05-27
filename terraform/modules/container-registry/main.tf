resource "azurerm_container_registry" "public" {
  name                = var.acr_name
  resource_group_name = var.acr_resource_group_name
  location            = var.acr_location
  sku                 = var.acr_sku
  admin_enabled       = true
}