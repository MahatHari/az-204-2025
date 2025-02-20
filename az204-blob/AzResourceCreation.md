# Azure CLI

## Sign into Azure

- `az login`

## Find the location name (if confused)

- `az account list-locations -o table`

## create resource group in europe north

- `az group create --location northeurope --name az204-2025-blob-rg`

## create a storage account

-` az storage account create --resource-group az204-2025-blob-rg --name blobstorageaccount1989 --location northeurope --sku Standard_LRS`

# Resource Clean up

`az group delete --name az204-2025-blob-rg --no-wait`
