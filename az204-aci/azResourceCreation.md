# Sign in to azure and create resource group

## create resource group

`az group create --name az204-aci-rg --location northeurope`

## Create Container

`DNS_NAME_LABEL =aci-example-$RANDOM
`

```
az container create --resource-group az204-aci-rg \
--name acicontainer2025
--image mcr.microsoft.com/azuredocs/aci-helloworld\
--ports 80 --os-type Linux --cpu 1 --memory 1 \
--dns-name-lable $DNS_NAME_LABEL --location northeurope
```

## verify the container is running

```
az container show --resource-group az204-aci-rg \
 --name mycontainer \
 --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" \
 --out table
```

## add restart policy while creating container group, default is always

```
az container create --resource-group az204-aci-rg \
--name acicontainer2025
--image mcr.microsoft.com/azuredocs/aci-helloworld\
--restart-policy OnFailure \
--ports 80 --os-type Linux --cpu 1 --memory 1 \
--dns-name-lable $DNS_NAME_LABEL --location northeurope
```

## set environmnet variable in container instances, below example we set 2

```
az container create --resource-group az204-aci-rg \
--name acicontainer2025
--image mcr.microsoft.com/azuredocs/aci-helloworld\
--restart-policy OnFailure \
--ports 80 --os-type Linux --cpu 1 --memory 1 \
--dns-name-lable $DNS_NAME_LABEL --location northeurope
--environmnet-variables 'NumberWords'='5' 'MinLength'='8'
```

## command to deploy container group with YAML file

```
az container create --resource-group az204-aci-rg \
--file secure-env.yml
```

### Content of secure-env.yml file

```
apiVersion: 2018-10-01
location: eastus
name: securetest
properties:
  containers:
  - name: mycontainer
    properties:
      environmentVariables:
        - name: 'NOTSECRET'
          value: 'my-exposed-value'
        - name: 'SECRET'
          secureValue: 'my-secret-value'
      image: nginx
      ports: []
      resources:
        requests:
          cpu: 1.0
          memoryInGB: 1.5
  osType: Linux
  restartPolicy: Always
tags: null
type: Microsoft.ContainerInstance/containerGroups
```

Secure environment variable are set by specifying 'secureValue' property instead of regular 'value' for the variable's type

## deploy container and mount volume

$ ACI_PERS_RESOURCE_GROUP= aaz204-aci-rg

```
az container create \
--resource-group $ACI_PERS_RESOURCE_GROUP \
-- name hellofiles
--image mcr.microsoft.com/azuredocs/aci/-hellopfiles \
--dns-name-lable aci-demo-2024
--ports 80
--azure-file-volume-account-name $ACI_PERS_STORAGE_ACCOUNT_NAME
--azure-file-volume-account-key $STORAGE_KEY
--azure-file-volume-share-name $ACI_PERS_SHARE_NAME
--azure-file-volume-mount-path /aci/logs/

```

## Deploy container and mount volume YAML

```
apiVersion: '2019-12-01'
location: eastus
name: file-share-demo
properties:
  containers:
  - name: hellofiles
    properties:
      environmentVariables: []
      image: mcr.microsoft.com/azuredocs/aci-hellofiles
      ports:
      - port: 80
      resources:
        requests:
          cpu: 1.0
          memoryInGB: 1.5
      volumeMounts:
      - mountPath: /aci/logs/
        name: filesharevolume
  osType: Linux
  restartPolicy: Always
  ipAddress:
    type: Public
    ports:
      - port: 80
    dnsNameLabel: aci-demo
  volumes:
  - name: filesharevolume
    azureFile:
      sharename: acishare
      storageAccountName: <Storage account name>
      storageAccountKey: <Storage account key>
tags: {}
type: Microsoft.ContainerInstance/containerGroups
```
