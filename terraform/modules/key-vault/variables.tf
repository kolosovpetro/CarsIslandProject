variable "key_vault_name" {
  description = "The name of the key vault"
  type        = string
}

variable "key_vault_location" {
  description = "The location of the key vault"
  type        = string
}

variable "key_vault_resource_group_name" {
  description = "The name of the resource group in which the key vault will be created"
  type        = string
}

variable "key_vault_tenant_id" {
  description = "The tenant id of the key vault"
  type        = string
}

variable "key_vault_sku_name" {
  description = "The sku name of the key vault"
  type        = string
}

variable "key_vault_soft_delete_retention_days" {
  description = "The soft delete retention days of the key vault"
  type        = number
}

variable "key_vault_object_id" {
  description = "The object id of the key vault"
  type        = string
}