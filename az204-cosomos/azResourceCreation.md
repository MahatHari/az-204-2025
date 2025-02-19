## Connect to Azure

`az login`

## Create resources in Azure

`az group create --location northeurope --name az204-comos-rg`

## Create Azure Cosmos DB account

`az cosmosdb create --name testcosmosdb2025 --resource-group az204-cosmos-rg`

## Retrive the primary key for the account

`az cosmosdb keys list --name testcosmosdb2025 --resource-group az204-comos-rg`

## remove resources

`az group delete --name az204-cosmos-rg --no-wait`
answer yes to the prompt
