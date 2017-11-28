#!/usr/bin/python3
import os
import argparse
import boto3

from config import Configuration, AWS_DEFAULT_REGION

AWS_REGION = os.environ.get(AWS_DEFAULT_REGION, 'us-west-2')
AVAILABILITY_ZONES_TO_USE = 3
DOMAIN_NAME = 'mikledemo.info'
CLUSTER_NAME = 'democluster'

def get_availability_zones(region):
    ec2 = boto3.client('ec2', region_name=region)
    zone_names = [zone['ZoneName'] for zone in ec2.describe_availability_zones(
        )['AvailabilityZones'][:AVAILABILITY_ZONES_TO_USE] if zone['State'] == 'available']
    return ','.join(zone_names)


def update_configuration(region, availability_zones, cluster_name, domain_name, file_name='config.json'):
    print('Updating configuration file {}.'.format(os.path.abspath(file_name)))

    with Configuration(file_name=file_name, save_on_exit=True) as configuration:
        configuration.region = region or AWS_REGION
        configuration.azs = availability_zones or get_availability_zones(configuration.region)
        configuration.cluster_name = cluster_name or CLUSTER_NAME
        configuration.domain_name = domain_name or DOMAIN_NAME

    print('Configuration values: ')
    print('\tRegion: {}'.format(configuration.region))
    print('\tAvailability Zones: {}'.format(configuration.azs))
    print('\tCluster Name: {}'.format(configuration.cluster_name))
    print('\tDomain Name: {}'.format(configuration.domain_name))

parser = argparse.ArgumentParser('Set configuration values to the config.json')
parser.add_argument('--region', help='AWS Region to use. DEFAULT: us-west-2', default='us-west-2')
parser.add_argument('--azs', help='Availability zones to use. By DEFAULT will take first 3 available zones for account & region')
parser.add_argument('--domain-name', help='Domain name on which domain will be deployed', default=DOMAIN_NAME)
parser.add_argument('--cluster-name', help='Cluster name', default=CLUSTER_NAME)

args = parser.parse_args()

update_configuration(args.region, args.azs, args.cluster_name, args.domain_name)
