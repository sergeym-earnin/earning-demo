#!/bin/bash

export DEMO_AB="true"
export DEMO_ENVIRONMENT="LocalDevelopment"
export ASPNETCORE_URLS="http://+:8082"
export HOSTNAME="ab-api-harcoded-fake-id"
export NODE_NAME="VSNode"

dotnet Earning.Demo.Api.dll