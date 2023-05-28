# connect to Azure via Azure CLI
az login

# Create resource group
az group create --location "northeurope" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --name "aks-k8s-rg"

# create AKS cluster
az aks create --generate-ssh-keys --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --node-count 3 --resource-group "aks-k8s-rg" --name "aks-k8s" --tier free
az aks create --generate-ssh-keys --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --node-count 3 --resource-group "cars-island-d01" --name "cars-aks-k8s-d01" --tier free

az aks update -n "aks-k8s" -g "aks-k8s-rg" --attach-acr "acrcarsislandd01"
az aks update -n "cars-aks-k8s-d01" -g "cars-island-d01" --attach-acr "acrcarsislandd01"

# connect to cluster
az aks get-credentials --resource-group "aks-k8s-rg" --name "aks-k8s" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks get-credentials --resource-group "cars-island-d01" --name "cars-aks-k8s-d01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

# get access to Dashboard
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Open Dashboard
az aks browse --resource-group "aks-k8s-rg" --name "aks-k8s" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks browse --resource-group "cars-island-d01" --name "cars-aks-k8s-d01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

# Create config map
kubectl create configmap "cars-configmap" --from-file=cars-configmap.yaml
kubectl get configmaps
kubectl describe configmap "cars-configmap"
kubectl apply -f ./api-deployment.azure.yaml
kubectl get pods
kubectl delete -f cars-configmap.yaml -n default
kubectl delete -f ./api-deployment.azure.yaml -n default
kubectl create -f ./cars-configmap.yaml
kubectl create -f ./api-deployment.azure.yaml
kubectl logs cars-api-deployment

curl -v http://20.191.53.75/api/Car/all