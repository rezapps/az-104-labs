# Azure Administrator Associate (az-104) Labs


This repository is for az-104 course labs. To run some of these labs, Azure subscription is needed. On Linux systems or wsl2, You need to install dotnet-sdk, nodejs, Azure functions cli and azurite. 

>[!Note]
>Azure Functions Core Tools relies on the .NET SDK/Runtime, so even if your functions are written in js/ts or python, you need to install dotnet-sdk. 
>And to emulate azure storage locally, "azurite" package is needed that can be installed using npm/pnpm.


Installing dotnet-sdk on Fedora Linux:

```bash
# find the latest version of dotnet-sdk
dnf search dotnet-sdk

# install latest version
sudo dnf install dotnet-sdk-10.0
```

Install nodejs, pnpm, azurite, azure functions cli on Linux, mac, wsl2

```bash
# install a nodejs version manager, i use volta.sh
curl https://get.volta.sh | bash

# install latest stable version of Node
volta install node

# install pnpm
volta install pnpm


# install azure functions cli
curl -sSL https://aka.ms/func-cli/install.sh | bash -s -- --prerelease

# install azurite
pnpm add -g azurite
```

and to check if installation was succesful:

```bash
func --version
azurite --version
```


---


## Azure cli 

Azure CLI commands for managing Azure resources, such as creating and managing virtual machines, databases, and storage accounts. [Official MS Learn reference page](https://learn.microsoft.com/en-us/cli/azure/reference-docs-index?view=azure-cli-latest)


command group list:

| Command Group | Description |
|---|---|
| az account | Manage Azure account information and subscriptions. |
| az vm | Create and manage virtual machines. |
| az group | Manage resource groups, including creation and deletion. |
| az storage | Manage Azure storage accounts and data. |
| az network | Configure and manage networking resources. |
| az sql | Manage Azure SQL databases and servers. |
| az acr | Work with Azure Container Registries. |
| az aks | Manage Azure Kubernetes Service clusters. |

---


## Commands needed for Azure Administrator Associate (az-104)

---

### **1. Identity & Access (Azure AD + RBAC)**

- **Login**  
  `az login`

- **Set subscription**  
  `az account set --subscription <id>`

- **List role assignments**  
  `az role assignment list --assignee <principal> -o table`

- **Assign RBAC role**  
  `az role assignment create --assignee <principal> --role <role> --scope <resource-id>`

- **Create service principal**  
  `az ad sp create-for-rbac --name myapp --role Contributor --scopes <scope>`

- **List Azure AD users**  
  `az ad user list -o table`

---

### **2. Governance (Resource Groups, Tags, Locks)**

- **Create resource group**  
  `az group create --name myRG --location westeurope`

- **Delete resource group**  
  `az group delete --name myRG --yes`

- **Tag a resource**  
  `az resource tag --tags env=prod --resource-group myRG --name myVM --resource-type Microsoft.Compute/virtualMachines`

- **Create resource lock**  
  `az lock create --name Lock1 --resource-group myRG --resource-name myVM --resource-type Microsoft.Compute/virtualMachines --lock-type ReadOnly`

---

### **3. Networking (VNet, Subnets, NSG, Public IP)**

- **Create VNet**  
  `az network vnet create --resource-group myRG --name myVNet --address-prefix 10.0.0.0/16`

- **Create subnet**  
  `az network vnet subnet create --resource-group myRG --vnet-name myVNet --name mySubnet --address-prefix 10.0.1.0/24`

- **Create NSG**  
  `az network nsg create --resource-group myRG --name myNSG`

- **Add NSG rule**  
  `az network nsg rule create --resource-group myRG --nsg-name myNSG --name AllowSSH --protocol Tcp --direction Inbound --priority 1000 --destination-port-range 22`

- **Create public IP**  
  `az network public-ip create --resource-group myRG --name myPublicIP`

- **Associate NSG to subnet**  
  `az network vnet subnet update --resource-group myRG --vnet-name myVNet --name mySubnet --network-security-group myNSG`

---

### **4. Storage (Accounts, Containers, Blobs, Keys)**

- **Create storage account**  
  `az storage account create --name mystorage --resource-group myRG --location westeurope --sku Standard_LRS`

- **List storage keys**  
  `az storage account keys list --account-name mystorage -o table`

- **Create blob container**  
  `az storage container create --name mycontainer --account-name mystorage`

- **Upload blob**  
  `az storage blob upload --container-name mycontainer --file myfile.txt --name myfile.txt --account-name mystorage`

- **Assign Storage RBAC**  
  `az role assignment create --assignee <principal> --role "Storage Blob Data Contributor" --scope <storage-resource-id>`

---

### **5. Compute (VMs, Images, Extensions)**

- **Create VM**  
  `az vm create --resource-group myRG --name myVM --image Ubuntu2204 --admin-username azureuser --generate-ssh-keys`

- **List VMs**  
  `az vm list -o table`

- **Start VM**  
  `az vm start --resource-group myRG --name myVM`

- **Stop VM**  
  `az vm stop --resource-group myRG --name myVM`

- **Open port**  
  `az vm open-port --resource-group myRG --name myVM --port 80`

- **Add VM extension**  
  `az vm extension set --publisher Microsoft.Azure.Extensions --name CustomScript --resource-group myRG --vm-name myVM --settings script.json`

---

### **6. Monitoring (Metrics, Logs, Alerts)**

- **Create Log Analytics workspace**  
  `az monitor log-analytics workspace create --resource-group myRG --workspace-name myWorkspace`

- **Enable diagnostics**  
  `az monitor diagnostic-settings create --resource <resource-id> --name diag1 --workspace <workspace-id> --logs '[...]' --metrics '[...]'`

- **Create alert rule**  
  `az monitor metrics alert create --name cpuAlert --resource-group myRG --scopes <vm-id> --condition "avg Percentage CPU > 80"`

---

### **7. Backup & Recovery (Azure Backup)**

- **Enable VM backup**  
  `az backup protection enable-for-vm --resource-group myRG --vault-name myVault --vm myVM`

- **Trigger backup**  
  `az backup protection backup-now --resource-group myRG --vault-name myVault --item-name myVM`

---

### **8. Containers (ACR, AKS)**

- **Create ACR**  
  `az acr create --resource-group myRG --name myRegistry --sku Basic`

- **Login to ACR**  
  `az acr login --name myRegistry`

- **Create AKS cluster**  
  `az aks create --resource-group myRG --name myAKS --node-count 2 --generate-ssh-keys`

---

### **9. Automation (ARM, Bicep)**

- **Deploy ARM template**  
  `az deployment group create --resource-group myRG --template-file template.json`

- **Deploy Bicep**  
  `az deployment group create --resource-group myRG --template-file main.bicep`

---


## Azure Functions Cli commands

| Command | Description |
|---|---|
| `func init` | Initialize a new Azure Functions project. |
| `func new` | Create a new function from a template. |
| `func new --list` | List available templates for your stack (c#,python,js). |
| `func run` | Launch the Azure Functions host runtime locally. `func start` is a backward-compatible alias. |
| `func quickstart` | Browse and scaffold complete function apps from the quickstart template catalog. |
| `func profile` | Inspect and manage Azure Functions CLI profiles. |
| `func setup` | Prepare local Azure Functions CLI dependencies (host runtime, language workers, extension bundles). |
| `func workload` | Manage installed CLI workloads. |
