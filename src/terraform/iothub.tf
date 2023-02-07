resource "azurerm_iothub" "iothub" {
  resource_group_name = azurerm_resource_group.rg1.name
  location            = azurerm_resource_group.rg1.location
  name                = "mtaeda-iothub-1"
  sku {
    name     = "S1"
    capacity = 2
  }
}

resource "azurerm_iothub_endpoint_servicebus_topic" "ingress" {
  iothub_id           = azurerm_iothub.iothub.id
  name                = "equipment-ingress"
  connection_string   = azurerm_servicebus_topic_authorization_rule.ingress.primary_connection_string
  resource_group_name = azurerm_resource_group.rg1.name
}

resource "azurerm_iothub_endpoint_servicebus_topic" "egress" {
  iothub_id           = azurerm_iothub.iothub.id
  name                = "equipment-egress"
  connection_string   = azurerm_servicebus_topic_authorization_rule.egress.primary_connection_string
  resource_group_name = azurerm_resource_group.rg1.name
}

resource "azurerm_iothub_route" "ingress" {
  source              = "DeviceJobLifecycleEvents"
  endpoint_names      = [azurerm_iothub_endpoint_servicebus_topic.ingress.name]
  resource_group_name = azurerm_resource_group.rg1.name
  name                = "ingress"
  iothub_name         = azurerm_iothub.iothub.name
  enabled             = true
}

resource "azurerm_iothub_route" "egress" {
  source              = "DeviceJobLifecycleEvents"
  endpoint_names      = [azurerm_iothub_endpoint_servicebus_topic.egress.name]
  resource_group_name = azurerm_resource_group.rg1.name
  name                = "egress"
  iothub_name         = azurerm_iothub.iothub.name
  enabled             = true
}