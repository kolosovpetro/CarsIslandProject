output "acr_name" {
  value = azurerm_container_registry.public.name
}

output "acr_url" {
  value = "${azurerm_container_registry.public.name}.azurecr.io"
}