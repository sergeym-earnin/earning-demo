#!/bin/bash

# Publish .net application to folder 'published'

current_dir=$PWD
docker_repo="msorokovsky/dotnetcore-demoapp"
docker_user=sergeymelnyk
docker_password=Nicecti1!

echo $current_dir
echo $docker_repo

cd $current_dir/Earning.Demo
dotnet publish -c debug -o published

cd $current_dir/Earning.Demo.Api
dotnet publish -c debug -o published

cd $current_dir/Earning.Demo.Web
dotnet publish -c debug -o published

cd $current_dir

# Clean up docker from old staff

echo Stop containers ...
stop_list=$(docker ps -a | grep "$docker_repo" | awk '{print $1}')
if [ -z "$stop_list" ]; then
    echo 0 containers was stoped.
else
    num_stoped=$(echo "$stop_list" | wc -l);
    docker stop $stop_list
    echo $num_stoped containers was stoped.
fi

echo Delete containers ...
delete_list=$(docker ps -a | grep "$docker_repo" | awk '{print $1}')
if [ -z "$delete_list" ]; then
    echo 0 containers was deleted.
else
    num_deleted=$(echo "$delete_list" | wc -l);
    docker rm -f $delete_list
    echo $num_deleted containers was deleted.
fi

echo Delete images ...
delete_list=$(docker images -a | grep "$docker_repo" | awk '{print $3}')
if [ -z "$delete_list" ]; then
    echo 0 containers was deleted.
else
    num_deleted=$(echo "$delete_list" | wc -l);
    docker rmi -f $delete_list
    echo $num_deleted images was deleted.
fi

echo Delete all dangling images ...
docker image prune --force

#
## Compose docker files and publish to DH
#

docker-compose build

docker login -u $docker_user -p $docker_password
docker push $docker_repo

# docker-compose up