#!/bin/bash

kubectl autoscale deployment earning-demo --min=2 --max=8 --cpu-percent=80