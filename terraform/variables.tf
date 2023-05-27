variable "prefix" {
  type        = string
  description = "Resources name prefix"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "resource_group_location" {
  type        = string
  description = "Location of the resource group."
}

variable "acr_name" {
  type        = string
  description = "Azure Container Registry name"
}

variable "acr_sku" {
  type        = string
  description = "Azure Container Registry SKU"
}

variable "key_vault_name" {
  description = "The name of the key vault"
  type        = string
}

variable "key_vault_sku_name" {
  description = "The sku name of the key vault"
  type        = string
}

variable "storage_account_name" {
  type        = string
  description = "Messenger storage account name"
}

variable "storage_container_name" {
  type        = string
  description = "Messenger storage container name"
}

variable "storage_account_tier" {
  type        = string
  #  default     = "Standard"
  description = "Messenger storage account tier"
}

variable "storage_account_replication" {
  type        = string
  #  default = "LRS"
  description = "Messenger storage account replication strategy"
}