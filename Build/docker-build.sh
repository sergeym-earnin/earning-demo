#!/bin/bash
# Usage:
#   ./docker-build.sh 100.0.0 true Earning.Demo.Api
#   ./docker-build.sh 100.0.0 false
#   ./docker-build.sh 100.0.0
#   ./docker-build.sh
# Publish .net application to folder 'published'

export ARG_VERSION_NUMBER=${1:-"1.0.0"}
export ARG_AB=${2:-"false"}
ARG_PROJECT_DIR=$3

build_dir=$PWD
cd ..
root_dir=$PWD

if [ -z "$ARG_PROJECT_DIR" ]; then
    echo [publish] solution from $root_dir directory ...

    dotnet publish -c debug -o published
    cd $build_dir
    echo [compose] inside $build_dir directory ...

    docker-compose build
else
    cd $root_dir/$ARG_PROJECT_DIR
    echo [publish] inside $PWD directory ...

    dotnet publish -c debug -o published
    cd $build_dir
    echo [compose] inside $build_dir directory ...

    service_name="$(echo ${ARG_PROJECT_DIR//./-} | awk '{print tolower($0)}')"
    echo [compose] $service_name service ...

    docker-compose build $service_name
fi