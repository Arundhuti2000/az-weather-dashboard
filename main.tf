provider "azurerm" {
  features {}
  resource_provider_registrations = "none"
  subscription_id = var.subscription_id
}

# Resource Group
resource "azurerm_resource_group" "weather_dashboard_rg" {
  name     = var.resource_group_name
  location = var.location
}

# Service Plan
resource "azurerm_service_plan" "app_plan" {
  name                = "weather-app-plan"
  resource_group_name = azurerm_resource_group.weather_dashboard_rg.name
  location            = azurerm_resource_group.weather_dashboard_rg.location
  os_type             = "Windows"
  sku_name            = "B1"
}

# Windows Web App for API
resource "azurerm_windows_web_app" "api" {
  name                = "weather-dashboard-api"
  resource_group_name = azurerm_resource_group.weather_dashboard_rg.name
  location            = azurerm_resource_group.weather_dashboard_rg.location
  service_plan_id     = azurerm_service_plan.app_plan.id

  site_config {
    application_stack {
      dotnet_version = "v7.0"
    }
  }
}

# Windows Web App for Web
resource "azurerm_windows_web_app" "web" {
  name                = "weather-dashboard-web"
  resource_group_name = azurerm_resource_group.weather_dashboard_rg.name
  location            = azurerm_resource_group.weather_dashboard_rg.location
  service_plan_id     = azurerm_service_plan.app_plan.id

  site_config {
    application_stack {
      dotnet_version = "v7.0"
    }
  }
}

# Key Vault
resource "azurerm_key_vault" "kv" {
  name                = "weather-kv-${random_string.unique_id.result}"
  location            = azurerm_resource_group.weather_dashboard_rg.location
  resource_group_name = azurerm_resource_group.weather_dashboard_rg.name
  tenant_id           = data.azurerm_client_config.current.tenant_id
  sku_name            = "standard"
}

resource "random_string" "unique_id" {
  length  = 4
  special = false
}
# Data Block for Client Config
data "azurerm_client_config" "current" {}
