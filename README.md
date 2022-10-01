# Repository for Cars Island rental solution

## FAQ

- How to publish APIM developer portal? On portal page
  select: `Portal overview (left pane) -> Publish portal, Enable CORS`
- What is APIM
  product? `It Contains one or more APIs, a usage quota, and the terms of use, defines are how APIs are surfaced to developers.`

## 1. Car Island API

#### Required Nuget Packages

- `Azure.Cosmos`
- `Azure.Storage.Blobs`
- `Microsoft.Extensions.Logging.Abstractions`
- `Microsoft.Extensions.Options`

#### Infrastructure provisioning

- **Configure subscription**
    - `az login`
    - `az login --use-device-code`
    - `az account subscription list`
    - `az account set --subscription "name or id"`
    - List all subscriptions showing default: `az account list -o table`
    - `az logout`

- **Create Resource Group**
    - `az group create --name "rg-car-rental-solution" --location "westus"`

- **Create Azure App Service**
    - `az appservice plan create --name "carislandplan" --resource-group "rg-car-rental-solution" --sku "F1"`
    - `az webapp list-runtimes`
    - `az webapp create --resource-group "rg-car-rental-solution" --name "app-car-rental-webapi" --plan "carislandplan" --runtime "dotnet:6"`
    - `az webapp create --resource-group "rg-car-rental-solution" --name "app-car-rental-webapp" --plan "carislandplan" --runtime "DOTNETCORE:3.1"`

- **Create Azure Cosmos DB**
    - `az cosmosdb create --name "cosmos-acc-car-island" --resource-group "rg-car-rental-solution"`
    - `az cosmosdb sql database create --account-name "cosmos-acc-car-island" --resource-group "rg-car-rental-solution" --name "azuredevtemplatesdb"`
    - `az cosmosdb sql container create -g "rg-car-rental-solution" -a "cosmos-acc-car-island" -d "azuredevtemplatesdb" -n "products" --partition-key-path "/id"`

- **Create Blob Container**
    - `az storage account create --name "carislandstorage1" --resource-group "rg-car-rental-solution" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
    - `az storage container create --name "cars-images-container" --account-name "carislandstorage1" --public-access "blob"`
    - `az storage account show-connection-string --name "carislandstorage1" --resource-group "rg-car-rental-solution" --subscription "Azure for Students"`
    - Upload images to the
      blob: `azcopy copy "./images/*" "https://carislandstorage1.blob.core.windows.net/cars-images-container?[SAS]" --recursive=true`

- **Create Analytics Workspace**
    - `az monitor log-analytics workspace create --resource-group "rg-car-rental-solution" --workspace-name "car-rental-workspace"`

#### Deploy apps

- API
    - `dotnet publish --configuration Release --output .\bin\publish`
    - `Compress-Archive .\bin\publish\* .\api.zip -Force`
    - `az webapp deployment source config-zip --resource-group "rg-car-rental-solution" --src "api.zip" --name "app-car-rental-webapi"`
- Web App
    - `dotnet publish --configuration Release --output .\bin\publish`
    - `Compress-Archive .\bin\publish\* .\app.zip -Force`
    - `az webapp deployment source config-zip --resource-group "rg-car-rental-solution" --src "app.zip" --name "app-car-rental-webapp"`

#### Create API Management

![application-overview.PNG](images/application-overview.PNG)

![architecture.png](images/architecture.png)
