#!/bin/bash
set -e

CLUSTER_NAME=$(scripts/get_cluster_name.py)
CLUSTER_FOLDER=kubernetes-$(scripts/get_cluster_name.py)
STATE_URI="s3://$(scripts/get_store_name.py)"

echo ----DELETE: Removing cluster infrastructure
cd $CLUSTER_FOLDER
terraform apply cluster-destroy.plan
cd ..

echo ----DELETE: Removinig cluster dynamic resources
kops delete cluster --yes --name=$CLUSTER_NAME --state=$STATE_URI

sleep 5
echo ----DELETE: Removing cluster VPC
terraform apply destroy.plan