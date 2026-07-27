# azBlobStorage lab


Link to lab: [azure-storage/01-blob-storage-resources-dotnet](https://microsoftlearning.github.io/mslearn-azure-developer/instructions/azure-storage/01-blob-storage-resources-dotnet.html)


## Create resource group

```bash
az group create --location northeurope --name azDotnetRG
```

## Create azure storage account

First create the variables in terminal:

```bash
resourceGroup=azDotnetRG
location=northeurope
accountName=storageacct$RANDOM
```

Now use the variables in the command:

```bash
az storage account create --name $accountName --resource-group $resourceGroup --location $location --sku Standard_LRS 

echo $accountName
```


## Assign a role to your Microsoft Entra user name

To create and assign the role to storage blob data owner, you need to have id for resourceGroup and userPrincipalName for you account and save them in variables.


```bash
resourceID=$(az storage account show --name $accountName \
    --resource-group $resourceGroup \
    --query id --output tsv)


userPrincipal=$(az rest --method GET --url https://graph.microsoft.com/v1.0/me \
    --headers 'Content-Type=application/json' \
    --query userPrincipalName --output tsv)

```

Now use both variables in the command:

```bash
az role assignment create --assignee $userPrincipal \
    --role "Storage Blob Data Owner" \
    --scope $resourceID
```

In `appsettings.json`, add your storage account name:

```
{
  "StorageAccountName": "storageAccountName"
}

```

run the console app:

```bash
dotnet build
dotnet run
```
