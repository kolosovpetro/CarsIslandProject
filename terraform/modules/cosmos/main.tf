resource "azurerm_cosmosdb_account" "public" {
  name                = var.cosmos_account_name
  location            = var.cosmos_location
  resource_group_name = var.cosmos_resource_group_name
  offer_type          = var.cosmos_offer_type
  kind                = var.cosmos_kind

  enable_automatic_failover = false
  enable_free_tier          = true

  consistency_policy {
    consistency_level       = var.cosmos_consistency_level
    max_interval_in_seconds = 300
    max_staleness_prefix    = 100000
  }

  geo_location {
    location          = "eastus"
    failover_priority = 1
  }

  geo_location {
    location          = "westus"
    failover_priority = 0
  }
}