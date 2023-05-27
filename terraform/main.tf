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