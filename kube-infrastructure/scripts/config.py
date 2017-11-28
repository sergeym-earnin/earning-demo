import json
import re
from pathlib import Path

CLUSTER_REGION_KEY = 'region'
AZS_KEY = 'availability_zones'
CLUSTER_NAME_KEY = 'cluster_name'
CLUSTER_DOMAIN_NAME_KEY = 'domain_name'

AWS_KEY = 'AWS_ACCESS_KEY_ID'
AWS_SECRET_ACCESS_KEY = 'AWS_SECRET_ACCESS_KEY'
AWS_DEFAULT_REGION = 'AWS_DEFAULT_REGION'


class Configuration:

    def __init__(self, file_name='config.json', save_on_exit=False, save_on_error=False):
        self.file_name = file_name
        self.save_on_exit = save_on_exit
        self.save_on_error = save_on_error
        if Path(file_name).exists():
            with open(self.file_name, 'r') as file:
                self._data = json.loads(file.read() or '{}')
        else:
            self._data = {}

    def __enter__(self):
        return self

    def __exit__(self, tp, value, traceback):
        if self.save_on_exit:
            if value is None or self.save_on_error:
                with open(self.file_name, 'w+') as file:
                    json.dump(self._data, file, sort_keys=True,
                              indent=4, separators=(',', ': '))

    @property
    def region(self):
        return self._data[CLUSTER_REGION_KEY] if CLUSTER_REGION_KEY in self._data else None

    @region.setter
    def region(self, value):
        self._data[CLUSTER_REGION_KEY] = value

    @property
    def azs(self):
        return self._data[AZS_KEY] if AZS_KEY in self._data else None

    @azs.setter
    def azs(self, value):
        self._data[AZS_KEY] = value

    @property
    def cluster_name(self):
        return self._data[CLUSTER_NAME_KEY] if CLUSTER_NAME_KEY in self._data else None

    @cluster_name.setter
    def cluster_name(self, value):
        self._data[CLUSTER_NAME_KEY] = value

    @property
    def domain_name(self):
        return self._data[CLUSTER_DOMAIN_NAME_KEY] if CLUSTER_DOMAIN_NAME_KEY in self._data else None

    @domain_name.setter
    def domain_name(self, value):
        self._data[CLUSTER_DOMAIN_NAME_KEY] = value


def get_bucket_name(domain_name):
    return "{0}-state".format(
        re.sub(r"[\W\d]+", "-", domain_name.lower().strip()))


def normilize_cluster_name(cluster_name):
    return re.sub(r"[\W\d]+", "-", cluster_name.lower().strip())
