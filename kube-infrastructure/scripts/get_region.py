#!/usr/bin/python3
from config import Configuration

with Configuration() as configuration:
    if not configuration.region:
        print("ERROR: Region is not specified")
        exit(1)

    print(configuration.region, end='')
