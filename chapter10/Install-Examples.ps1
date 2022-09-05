# This script is designed to install the chapter's prerequisites:
#   - Helm
#   - Helm chart repo for KEDA
#   - Terraform (if desired)
#   - AKS (if desired)
# It is assumed that you have the Azure CLI installed, kubectl installed, and the cluster you wish to use is the current
# context in your kube.config. 
param (
    [Boolean]$InstallTerraform = $false,
    [Boolean]$InstallAks = $false
)

Write-Output "Installing Helm..."
# Grab the system OS to determine how best to install
# Please ensure the appropriate package manager is installed on your system!
switch($env:OS) {
    case "Darwin":
        brew install helm
        break;
    case "Windows_NT":
        choco install kubernetes-helm
        break;
    default:            #Linux
        sudo snap install helm --classic
        break;
}

Write-Output "Generating list of App Insights metric metadata..."
az monitor app-insights metrics get-metadata | Out-File ai-metadata.txt

if($InstallAks) {
    Write-Output "Initializing terraform and creating AKS Cluster..."
    Write-Output "`nLooking up your Azure account information..."
    az account show
    $input = Read-Host "Is this configuration correct? (Y/n)"
    switch($input.ToString().ToLower()) {
        case "y": 
            cd terraform
            terraform init
            terraform plan -o output.plan 
            terraform apply
            Write-Output "AKS cluster created, retrieving kube.config..."
            az aks get-credentials -g rg-mtaeda-example -n aks-mtaeda-1
            break;
        case "n": 
            Write-Warning "Please adjust your Azure CLI account settings and re-run this script."
            exit 0;
    }
}

Write-Output "Adding Helm chart repo and installing Keda..."
helm repo add kedacore https://kedacore.github.io/charts
helm repo update
kubectl create ns keda
helm install keda kedacore/keda --namespace keda
#
#  Optional -- install the infrastructure for the application, if using Kafka (from Helm)
# kubectl create ns mtaeda-infra
# helm install kafka bitnami/kafka --namespace mtaeda-infra