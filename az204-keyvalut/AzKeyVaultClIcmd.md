# Setting and retrieving a secret from Azure key vault using Azure CLI

## Creating Key Vault

### create variables

`myKeyValut=az204vault-$RANDOM
myLocation=northeurope`

### Create a resource group

`az group create --name az204-keyvault-rg --location $myLocation`

### Create Key Vault using az keyvault create

`az keyvault create --name $myKeyVault --resource-group az204-keyvault-rg --location $myLocation

## Add and Retrieve a secret

### create a secret, lets save a password that could be used up by an app examplePassword and Value is hIvweKx3FEfs

```
az keyvault secret set \
--vault-name $myKeyVault \
--name "examplePassword" \
--value "hIvweKx3FEfs"
```

### Us az keyvault secret show to retrieve secret

`az keyvault show --name "examplePassword" --vault-name $myKeyVault`

### Clean up resources

`az group delete --name az204-keyvault-rg --no-wait`
