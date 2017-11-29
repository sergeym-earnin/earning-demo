#!/bin/bash
set -e

#./scripts/create_store_bucket.py

REGION=$(scripts/get_region.py)
BUCKET=$(scripts/get_store_name.py)
KEY=$(scripts/get_cluster_name.py).tfstate

terraform init -backend-config="region=$REGION" \
               -backend-config="bucket=$BUCKET" \
               -backend-config="key=$KEY" \
               -var-file=config.json