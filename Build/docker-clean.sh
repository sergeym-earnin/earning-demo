#!/bin/bash
# Usage: 
#   ./docker-clean.sh 100.0.0 Earning.Demo.Api
#   ./docker-clean.sh 100.0.0
#   ./docker-clean.sh
# Clean up docker from old staff

ARG_VERSION_NUMBER=$1
ARG_PROJECT_DIR=$2

search_pattern="msorokovsky/dotnetcore-demoapp"

if [ ! -z "$ARG_VERSION_NUMBER" ]; then
    search_pattern="${search_pattern}:${ARG_VERSION_NUMBER}"
    if [ ! -z "$ARG_PROJECT_DIR" ]; then
        service_name="$(echo ${ARG_PROJECT_DIR//./-} | awk '{print tolower($0)}')"
        search_pattern="$search_pattern-$service_name"
    fi
fi

echo [stop] containers with pattern $search_pattern ...
stop_list=$(docker ps -a | grep "$search_pattern" | awk '{print $1}')
if [ -z "$stop_list" ]; then
    echo 0 containers was stoped.
else
    num_stoped=$(echo "$stop_list" | wc -l);
    docker stop $stop_list
    echo $num_stoped containers was stoped.
fi

echo [delete] containers with pattern $search_pattern ...
delete_list=$(docker ps -a | grep "$search_pattern" | awk '{print $1}')
if [ -z "$delete_list" ]; then
    echo 0 containers was deleted.
else
    num_deleted=$(echo "$delete_list" | wc -l);
    docker rm -f $delete_list
    echo $num_deleted containers was deleted.
fi

echo [delete] images with pattern $search_pattern ...
delete_list=$(docker images -a | grep "$search_pattern" | awk '{print $3}')
if [ -z "$delete_list" ]; then
    echo 0 containers was deleted.
else
    num_deleted=$(echo "$delete_list" | wc -l);
    docker rmi -f $delete_list
    echo $num_deleted images was deleted.
fi

echo Delete all dangling images ...
docker image prune --force
