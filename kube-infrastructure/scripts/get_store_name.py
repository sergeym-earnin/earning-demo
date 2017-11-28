#!/usr/bin/python3
from config import Configuration, get_bucket_name

with Configuration() as configuration:
    if not configuration.domain_name:
        print('ERROR: Cluster domain name is not specified')
        exit(1)

    bucket_name = get_bucket_name(configuration.domain_name)
    print(bucket_name, end='')