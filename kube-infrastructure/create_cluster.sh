#!/bin/bash
set -e

if [ "$1" == "" ] || [ "$2" == "" ]
then
    echo Usage: generate_config.sh AWS_KEY AWS_SECRET AWS_REGION CLUSTER_NAME DOMAIN_NAME
    exit 1
fi

REGION=${3-us-west-2}

source scripts/set_environment_variables.sh $1 $2 $REGION
CLUSTER_NAME=${4-democluster}
DOMAIN_NAME=${5-mikledemo.info}

./scripts/generate_config.py --region=$REGION --cluster-name=$CLUSTER_NAME --domain-name=$DOMAIN_NAME
./scripts/init_terraform.sh
./scripts/create_vpc.sh

./scripts/generate_certificate.sh
./scripts/generate_create_cluster_terraform.sh

./scripts/tear_up_kubernetes.sh
