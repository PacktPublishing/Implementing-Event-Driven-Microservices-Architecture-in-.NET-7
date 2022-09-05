
resource azurerm_kubernetes_cluster "aks-mtaeda-1" {
    name = "aks-mtaeda-1"
    role_based_access_control_enabled = true
    resource_group_name = azurerm_resource_group.rg1.name
    location = azurerm_resource_group.rg1.location
    dns_prefix = "aks-mtaeda-1"
    identity {
      type = "SystemAssigned"
    }
    key_vault_secrets_provider {
      secret_rotation_enabled = true
    }
    default_node_pool {
      name = "defaultpool"
      enable_auto_scaling = true
      max_pods = 100
      min_count = 1
      max_count = 8
      vm_size = "standard_b4ms"
    }  
}

resource azurerm_kubernetes_cluster_node_pool "app-pool" {
  enable_auto_scaling = true
  name = "appcompute"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks-mtaeda-1.id
  vm_size = "standard_D8ads_v5"
  min_count = 0
  max_count = 10
  max_pods = 100
}