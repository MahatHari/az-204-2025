# Prepare Environment

# install the azure container apps extensions for the cli

`az extension add --name containerapp --upgrade `

# register Microsoft.App namespace

`az provider register --namespace Microsoft.App`

# register Microsoft.OperationalInsights provider for the Azure Monitor Log Analytics workspace

`az provider register --namespace Microsoft.OperationalInsights`

# set environment variables to be used later

myRg=az2o4-appcont-rg (--resource-group)
myLocation=northeurope (--location)
myAppContEnv=az204-env-$RANDOM

# create the resource group for your container app

```
az group create \
--name $myRg
--location $myLocation
```

# Create Environment

```
az containerapp env create \
--name $myAppContEnv
--resource-group $myRg
--location $myLocation
```

# Create container app

1. Deploy sample app contianer image by using containerapp create

```
az containerapp create \
--name my-container-app
--resource-group $myRg
--environment $myAppContEnv \
--image mcr.microsoft.com/azuredocs/containerapps-helloworld:laterst \
--target-port 80
--ingress'external'
--query properties.configuration.ingress.fqdn

```

by setting ingress to external you the container app available to public request, --query returns link to access your app

# clean up resources

`az group delete $myRg `
