#!/bin/bash
set -e

terraform plan -out=vpc.plan -var-file=config.json
terraform plan -destroy -out=destroy.plan -var-file=config.json
terraform apply vpc.plan