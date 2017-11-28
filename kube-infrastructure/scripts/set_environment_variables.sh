#!/bin/bash
set -e
# Sets the AWS authentication parmeters and prepare environment to run kubernetes provisioning scripts

if [ "$1" == "" ] || [ "$2" == "" ]
then
    echo Usage: prepare_environment.sh AWS_KEY AWS_SECRET [AWS_REGION == us-west-2]
    exit 1
fi

AWS_KEY=$1
AWS_SECRET=$2
AWS_REGION="us-west-2"

if [ "$3" != "" ]
then
    AWS_REGION=$3
fi

echo Setting AWS environment key, secret. Default region: $AWS_REGION

export AWS_ACCESS_KEY_ID=$AWS_KEY
export AWS_SECRET_ACCESS_KEY=$AWS_SECRET
export AWS_DEFAULT_REGION=$AWS_REGION

