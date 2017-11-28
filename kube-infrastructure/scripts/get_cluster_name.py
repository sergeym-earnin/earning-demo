#!/usr/bin/python3
import re

from config import Configuration, normilize_cluster_name

with Configuration() as configuration:
    if not configuration.cluster_name:
        print('ERROR: Cluster name is not specified')
        exit(1)

    cluster_name = normilize_cluster_name(configuration.cluster_name)
    print(cluster_name, end='')