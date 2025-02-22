# Send and Receive message from a service bus queue by using .NET

## create a service bus namesapce, and queue using cli

## create a .net console application to send and receive message from the queue

### create variable in cli

myLocation=northeurope
myRG= az204-svcbus-rg
myNameSapceName=az204svcbus$RANDOM
myQueName= az204-queue

### create resouce group to hold the azure resources you are creating

`az group create --name $myRG --location $myLocatio`

### create Service Bus messaging namespace.

`az servicebus namespace create \
--resource-group $myRG \
--name $myNameSpaceName \
--location $myLocation \`

### create a service bus queue

`az servicebus queue create \
--resource-group $myRG \
--namespace-name $myNameSpaceName \
--name $myQueName`

## Retrieving connection string for the service bus Name space

Open Azure portal => navigate to az204-svcbus-rg resource => then select resource you created az204svcbus$RANDOM => then Shared access policies => Slect RootManageSharedAccessKey policy => then copy the primary connection string
