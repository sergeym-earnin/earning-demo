#!/usr/bin/python3
import yaml
import json
from argparse import ArgumentParser
import collections

class long_str(str):
    pass

def long_str_dumper(dumper, data):
    return dumper.represent_scalar('tag:yaml.org,2002:str', data, style='|')

yaml.add_representer(long_str, long_str_dumper)

parser = ArgumentParser()
parser.add_argument('yaml_file')
parser.add_argument('path_to_modify')
parser.add_argument('--json_file')
parser.add_argument('--value', help='json value like: {"hello": "HI"}')
parser.add_argument('--as_json', help='adds value as json to last node in path_to_modify ', default=False)

arguments = parser.parse_args()

value = None
if arguments.json_file:
    with open(arguments.json_file, 'r') as file:
        value = json.loads(file.read() or '{}')
else:
    value = json.loads(arguments.value)

with open(arguments.yaml_file, 'r') as file:
    data = yaml.load(file)

path = str(arguments.path_to_modify)

paths = path.split('\\')

def update(d, u):
    for k, v in u.items():
        if isinstance(v, collections.Mapping):
            d[k] = update(d.get(k, {}), v)
        elif isinstance(v, list):
            prev_value = d.get(k, None)
            if isinstance(prev_value, list):
                d[k] = list(set(prev_value) | set(v))
            else:
                d[k] = v
        else:
            d[k] = v
    return d

current_node = data
previous_node = data
for p in paths:
    previous_node = current_node
    if p in current_node:
        current_node = current_node[p]
        continue
    else:
        new_node = {}
        current_node[p] = new_node
        current_node=new_node

if arguments.as_json:
    previous_node[p] = long_str(json.dumps(value))
else:
    update(current_node, value)

with open(arguments.yaml_file, 'w+') as file:
    yaml.dump(data, file, default_flow_style=False)
