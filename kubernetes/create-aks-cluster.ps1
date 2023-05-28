# connect to Azure via Azure CLI
az login

# Create resource group
az group create --location "northeurope" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --name "aks-k8s-rg"

# create AKS cluster
az aks create --generate-ssh-keys --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --node-count 3 --resource-group "aks-k8s-rg" --name "aks-k8s" --tier free

# connect to cluster
az aks get-credentials --resource-group "aks-k8s-rg" --name "aks-k8s" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

# get access to Dashboard
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Open Dashboard
az aks browse --resource-group "aks-k8s-rg" --name "aks-k8s" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" 