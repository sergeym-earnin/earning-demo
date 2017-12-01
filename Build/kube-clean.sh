#!/bin/bash
# Usage:
#   ./kube-clean.sh earning-demo minikube
#   ./kube-clean.sh earning-demo
#   ./kube-clean.sh

ARG_APP_PREFIX=${1:-"earning-demo"}
ARG_CONTEXT=${2:-"minikube"}

kubectl config use-context $ARG_CONTEXT

echo [delete] deployments with prefix $ARG_APP_PREFIX ...
kubectl get deployment | grep "$ARG_APP_PREFIX" | awk '{print $1}' | while read -r name ; do
    kubectl delete deployment $name
done

echo [delete] services with prefix $ARG_APP_PREFIX ...
kubectl get service | grep "$ARG_APP_PREFIX" | awk '{print $1}' | while read -r name ; do
    kubectl delete service $name
done