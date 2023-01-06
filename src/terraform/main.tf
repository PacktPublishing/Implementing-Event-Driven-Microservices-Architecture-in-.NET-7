/*
Primary needs -- resource group, storage, and key vault
*/
data "azurerm_client_config" "current" {

}

resource "azurerm_resource_group" "rg1" {
  name     = var.rg_name
  location = var.location
}

resource "azurerm_key_vault" "akv" {
  resource_group_name = azurerm_resource_group.rg1.name
  location            = azurerm_resource_group.rg1.location
  name                = "kveusmtaeda-example"
  sku_name            = "premium"
  tenant_id           = data.azurerm_client_config.current.tenant_id
}

resource "azurerm_container_registry" "reg" {
  resource_group_name       = var.rg_name
  location                  = var.location
  name                      = "acreusmtaeda01"
  quarantine_policy_enabled = true
  sku                       = "Premium"
}

