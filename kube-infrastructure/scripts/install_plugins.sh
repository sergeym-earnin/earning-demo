#!/bin/bash

# Version of the dashboard supported by kops image are in the 6dc75162dce25b5a94aa500ebba923e8223e5cfd commit
kubectl apply -f https://github.com/kubernetes/dashboard/raw/6dc75162dce25b5a94aa500ebba923e8223e5cfd/src/deploy/recommended/kubernetes-dashboard.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/kops/master/addons/monitoring-standalone/v1.7.0.yaml

kubectl apply -f plugins/externalDns.yaml
