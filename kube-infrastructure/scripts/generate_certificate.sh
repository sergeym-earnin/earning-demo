#!/bin/bash
set -e
CERTIFICATE_NAME=$(scripts/get_cluster_name.py)-certificate

mkdir -p keys

ssh-keygen -t rsa -b 4096 -N '' -C "$CERTIFICATE_NAME" -f "keys/$CERTIFICATE_NAME"

mkdir -p ~/.ssh
mv keys/$CERTIFICATE_NAME ~/.ssh/$CERTIFICATE_NAME
chmod 600 ~/.ssh/$CERTIFICATE_NAME