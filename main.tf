provider "azurerm" {
  features {}
  resource_provider_registrations = "none"
  subscription_id = var.subscription_id
}
resource "azurerm_resource_group" "weather_dashboard_rg" {
  name     = "weather-dashboard-rg"
  location = "East US"
}