#!/bin/bash
# Usage:
#   ./docker-publish.sh 100.0.0 Earning.Demo.Api
#   ./docker-publish.sh 100.0.0
#   ./docker-publish.sh

ARG_VERSION_NUMBER=$1
ARG_PROJECT_DIR=$2

repo_pattern="msorokovsky/dotnetcore-demoapp"
docker_user=sergeymelnyk
docker_password=Nicecti1!

docker login -u $docker_user -p $docker_password

if [ ! -z "$ARG_VERSION_NUMBER" ]; then
    tag_pattern="${ARG_VERSION_NUMBER}"
    if [ ! -z "$ARG_PROJECT_DIR" ]; then
        service_name="$(echo ${ARG_PROJECT_DIR} | awk '{print tolower($0)}')"
        tag_pattern="$tag_pattern-$service_name"
    fi
fi

echo [publish] containers with pattern $repo_pattern:$tag_pattern to DH...
docker images -a | grep "$repo_pattern" | grep "$tag_pattern" | awk '{printf "%s:%s\n", $1, $2}' | while read -r image ; do
    docker push $image
done