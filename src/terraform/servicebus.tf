resource "azurerm_servicebus_namespace" "sb" {
  location            = azurerm_resource_group.rg1.location
  resource_group_name = azurerm_resource_group.rg1.name
  name                = "asbeusmtaeda1"
  sku                 = "Standard"
}

resource "azurerm_servicebus_topic" "ingress" {
  name         = "ingresstopic"
  namespace_id = azurerm_servicebus_namespace.sb.id
}

resource "azurerm_servicebus_topic" "egress" {
  name         = "egresstopic"
  namespace_id = azurerm_servicebus_namespace.sb.id
}

resource "azurerm_servicebus_topic_authorization_rule" "ingress" {
  listen = false
  send = true
  manage = false
  name = "ingressRule"
  topic_id = azurerm_servicebus_topic.ingress.id
}

resource "azurerm_servicebus_topic_authorization_rule" "egress" {
  listen = false
  send = true
  manage = false
  name = "egressRule"
  topic_id = azurerm_servicebus_topic.egress.id
}