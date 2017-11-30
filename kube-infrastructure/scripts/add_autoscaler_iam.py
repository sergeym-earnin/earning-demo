#!/usr/bin/python3
import argparse
import json

parser = argparse.ArgumentParser('Adds IAM policy to IAM_ROLE files')
parser.add_argument('files', help='Files to add IAM policies to', nargs='+')
parser.add_argument('iam_policy_file', help='IAM Policy json to be added to files')

arguments = parser.parse_args()

with open(arguments.iam_policy_file, 'r+') as source:
    new_iam_policy = json.loads(source.read() or '{}')

if not new_iam_policy:
    print('Nothing to append')
    exit()

for file_name in arguments.files:
    with open(file_name) as file:
        file_content = json.loads(source.read() or '{}')
    
    file_content["Statement"].append(new_iam_policy)

    with open(file_name + '_1', 'w+') as file:
        json.dump(file_content, file, indent=4, separators=(',', ': '))
