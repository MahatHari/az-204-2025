# Configure managed Identities

## System-assigned managed idenity

- your account needs the Virtual Machine Contributor role assignment

### Enable System assigned managed identity during creation of a azure virtual machine using CLI

```
az vm create --resource-group vms-managed-identity \
--name myVM --image win2016datacenter \
--generate-ssh-keys \
--assign-identity \
--role contributor \
--scope mySubscription \
--admin-username azureuser \
--admin-password myPassword1234
```

### Enable System assigned managed idenity on existing Azure virtaul machine

`az vm identity assign -g myResourcegroup -n myVm`

## User - assigned managed identity

### Assign user-asigned managed identiry during creation of virtual machine

```
az vm create \
--resource-group <Resource Group> \
--name <VM Name> \
--image Ubuntu2024
--admin-username
--admin-password
--assign-identy<User Assigned IDentity Name>
--role <Role>
--scope <Subscription>
```

### User assigned managed identity on an existing vm

`az vm idendity assign -g <Resource Group> -n <VM Name> --identites <USER aasigned IDENTITY>`

### Creating user-assigned identity

`az identity create --resource-group myResourceGroup --name myUserAssignedIdentity`

### Assignin the new user-assigned identity to myTestAppConfigStore store

```
az appconfig identity assign --name myTestAppConfigStore \
--resource-group myResourceGroup \
--identites /subscriptions/[subscription id]/resourcegroups/myResourceGroup/providers/Microsoft.ManagedIdentity/userAssignedIdentities/myuserAssignedIdentity
```
