#!/bin/bash

# Publish .net application to folder 'published'

ARG_TAG_NAME=$1
ARG_PROJECT_DIR=$2
ARG_AB=$2

cd ..
current_dir=$PWD
docker_repo="msorokovsky/dotnetcore-demoapp"
docker_user=sergeymelnyk
docker_password=Nicecti1!

docker login -u $docker_user -p $docker_password

if [ -z "$ARG_PROJECT_DIR" ]; then
    echo publishing solution from current directory ...
    dotnet publish -c debug -o published
    docker-compose build
    docker push $docker_repo
else
    echo publishing $current_dir/$ARG_PROJECT_DIR directory ...
    cd $current_dir/$ARG_PROJECT_DIR
    dotnet publish -c debug -o published
    if [ -z "$ARG_AB" ]; then
        docker-compose build ${ARG_PROJECT_DIR//./-}
        docker push $docker_repo:${ARG_TAG_NAME}-${ARG_PROJECT_DIR//./-}
    else
        docker-compose build ${ARG_PROJECT_DIR//./-}-ab
        docker push $docker_repo:${ARG_TAG_NAME}-${ARG_PROJECT_DIR//./-}-ab
    fi
fi