#!/bin/bash

kompose down
# kompose up

#kubectl create secret docker-registry earning-dh-secret \
# --docker-username=sergeymelnyk \
# --docker-password=Nicecti1! \
# --docker-email=sergey.melnyk@earnin.com

kubectl create -f earning-demo-service.yaml,earning-demo-api-service.yaml,earning-demo-api-v2-service.yaml,earning-demo-web-service.yaml,earning-demo-deployment.yaml,earning-demo-api-v2-deployment.yaml,earning-demo-api-deployment.yaml,earning-demo-web-deployment.yaml

open $(minikube service -n default --url earning-demo-web)