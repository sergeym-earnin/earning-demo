#!/bin/bash
# Usage:
#   ./kube-publish.sh 1.0.0 earning-demo false minikube
#   ./kube-publish.sh 1.0.0 earning-demo false
#   ./kube-publish.sh 1.0.0 earning-demo 
#   ./kube-publish.sh 1.0.0 
#   ./kube-publish.sh

ARG_VERSION_TAG=${1:-"1.0.0"}
ARG_APP_PREFIX=${2:-"earning-demo"}
ARG_AB=${3:-"false"}
ARG_CONTEXT=${4:-"minikube"}

if [ "$ARG_AB" == "true" ]; then
    ARG_AB_PREFIX="-ab"
else
    ARG_AB_PREFIX=""
fi

kubectl config use-context $ARG_CONTEXT

if [ -z "$(kubectl get secret earning-dh-secret)" ]; then
    echo [secret] is not exist. Creating new secret ...
    kubectl create secret docker-registry earning-dh-secret \
        --docker-username=sergeymelnyk \
        --docker-password=Nicecti1! \
        --docker-email=sergey.melnyk@earnin.com
else
    echo [secret] is alredy exist.
fi

echo [copy] files to auto generated folder
rm -rf AutoGenerated
mkdir -p AutoGenerated
ls  *$ARG_APP_PREFIX*.yaml | while read -r name ; do
    echo [copy] file $name.
    cp $name ./AutoGenerated
done
cd AutoGenerated

ls | while read -r file ; do
    echo [replace] placeholders inside $file files
    sed -i -e 's/ARGVERSIONTAG/'$ARG_VERSION_TAG'/g' $file
    sed -i -e 's/ARGDEMOAB/'$ARG_AB'/g' $file
    sed -i -e 's/ARGABPREFIX/'$ARG_AB_PREFIX'/g' $file
    # cat $file
done
rm -r *.yaml-e


file_list=$(ls | awk '{printf "%s,", $1}')
echo [publish] $file_list containers to Kube
kubectl create -f ${file_list%?}

if [ "$ARG_CONTEXT" == "minikube" ]; then
    echo [open]  earning-demo-web sercvice.
    #open $(minikube service -n default --url earning-demo-web)
fi

echo [delete] auto generated folder
cd ..
rm -rf AutoGenerated