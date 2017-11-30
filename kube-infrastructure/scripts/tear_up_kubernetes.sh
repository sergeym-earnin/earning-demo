#!/bin/bash
set -e

REGION=$(scripts/get_region.py)
BUCKET=$(scripts/get_store_name.py)
KEY=kubernetes-$(scripts/get_cluster_name.py).tfstate
CLUSTER_FOLDER=kubernetes-$(scripts/get_cluster_name.py)

cp cluster.tf.template $CLUSTER_FOLDER/terraform.tf

cd $CLUSTER_FOLDER

terraform init -backend-config="region=$REGION" \
               -backend-config="bucket=$BUCKET" \
               -backend-config="key=$KEY"

terraform plan -out=cluster.plan

terraform plan -destroy -out=cluster-destroy.plan

terraform apply cluster.plan
