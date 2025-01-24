output "resource_group_name" {
  value = azurerm_resource_group.weather_dashboard_rg.name
}
output "api_url" {
  value = azurerm_windows_web_app.api.default_hostname
}

output "web_url" {
  value = azurerm_windows_web_app.web.default_hostname
}
