#!/bin/bash
AZS=$(terraform output main_network_azs | tr -d '\n')
VPC=$(terraform output main_network_id | tr -d '\n')
CIDR=$(terraform output main_network_cidr_block | tr -d '\n')
FQDN=$(scripts/get_fqdn.py)
CLUSTER_NAME=$(scripts/get_cluster_name.py)
CERTIFICATE_NAME=$CLUSTER_NAME-certificate
STATE_URI="s3://$(scripts/get_store_name.py)"

echo "Creating cluster script with following parameters:"
echo "  "$AZS
echo "  "$VPC
echo "  "$CIDR
echo "  "$FQDN
echo "  "$STATE_URI
echo "  "$CLUSTER_NAME
echo "  "$CERTIFICATE_NAME

kops create cluster --zones=$AZS --vpc=$VPC --network-cidr=$CIDR --name=$FQDN --state=$STATE_URI \
    --ssh-public-key="keys/$CERTIFICATE_NAME.pub" --target="terraform" --out=kubernetes-$CLUSTER_NAME \
    --master-count=3 --master-size=m4.large \
    --node-count=6 --node-size=m4.large
