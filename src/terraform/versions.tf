terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0.2"
    }
  }

  required_version = ">= 1.1.0"
  backend "azurerm" {
    storage_account_name = "saeusmtaedatf"
    container_name       = "tfstate"
    key                  = "state.tfstate"
    sas_token            = "sp=rwl&st=2023-01-02T16:49:31Z&se=2023-01-03T00:49:31Z&spr=https&sv=2021-06-08&sr=c&sig=UZ0w7fvsEiESPBgICXi09HMnFMBGWp0PDkT3m74ka18%3D"
  }
}

provider "azurerm" {
  features {}
}
