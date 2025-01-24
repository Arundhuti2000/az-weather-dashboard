provider "azurerm" {
  features {}
  resource_provider_registrations = "none"
  subscription_id = var.subscription_id
}
resource "azurerm_resource_group" "weather_dashboard_rg" {
  name     = "weather-dashboard-rg"
  location = "East US"
}
resource "azurerm_service_plan" "app_plan" {
  name                = "weather-app-plan"
  resource_group_name = azurerm_resource_group.rg.name
  location           = azurerm_resource_group.rg.location
  os_type            = "Windows"
  sku_name           = "B1"
}

resource "azurerm_windows_web_app" "api" {
  name                = "weather-dashboard-api"
  resource_group_name = azurerm_resource_group.rg.name
  location           = azurerm_resource_group.rg.location
  service_plan_id     = azurerm_service_plan.app_plan.id
 site_config {
    application_stack {
      dotnet_version = "v7.0"
    }
  }
}

resource "azurerm_windows_web_app" "web" {
  name                = "weather-dashboard-web"
  resource_group_name = azurerm_resource_group.rg.name
  location           = azurerm_resource_group.rg.location
  service_plan_id     = azurerm_service_plan.app_plan.id

  site_config {
    application_stack {
      dotnet_version = "v7.0"
    }
  }
}
resource "azurerm_key_vault" "kv" {
  name                = "weather-dashboard-kv"
  location           = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  tenant_id          = data.azurerm_client_config.current.tenant_id
  sku_name           = "standard"
}