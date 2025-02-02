variable "location" {
  default = "East US"
}

variable "resource_group_name" {
  default = "weather-dashboard-rg"
}

variable "subscription_id" {}

variable "weather_api_key" {
  type = string
}