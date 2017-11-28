#!/usr/bin/python3
import boto3
import botocore
from config import Configuration, get_bucket_name

with Configuration() as configuration:
    if not configuration.region:
        print('ERROR: Region is not specified')
        exit(1)

    if not configuration.domain_name:
        print('ERROR: Cluster domain name is not specified')
        exit(1)

    bucket_name = get_bucket_name(configuration.domain_name)

    s3resource = boto3.resource('s3')
    bucket_resource = s3resource.Bucket(bucket_name)

    try:
        s3resource.meta.client.head_bucket(Bucket=bucket_name)
        bucket_exists = True
    except botocore.client.ClientError as e:
        error_code = int(e.response['Error']['Code'])
        if error_code == 404:
            bucket_exists = False
        else:
            raise

    if (not bucket_exists):
        s3 = boto3.client('s3', region_name=configuration.region)

        s3.create_bucket(Bucket=bucket_name)

        bucket = s3.create_bucket(Bucket=bucket_name, ACL='private',
                                CreateBucketConfiguration=dict(LocationConstraint=configuration.region))
        print('S3 Bucket created: {}'.format(bucket_name))
    else:
        print('S3 Bucket already exists: {}'.format(bucket_name))
