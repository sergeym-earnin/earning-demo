#!/bin/bash
set -e
#pip & dependencies
apt install -y python3-pip
pip3 install -r requirements.txt

#AWS CLI
pip3 install awscli --upgrade --user
export PATH=~/.local/bin:$PATH
chown -R $USER ~/.local

#terraform
apt install -y unzip

wget -O /tmp/terraform.zip https://releases.hashicorp.com/terraform/0.11.0/terraform_0.11.0_linux_amd64.zip?_ga=2.154807480.1861735120.1511719805-1403153550.1510794470

unzip /tmp/terraform.zip -d /tmp/
chmod +x /tmp/terraform
mv /tmp/terraform /usr/bin
rm /tmp/terraform.zip

#kubectl
wget -O /tmp/kubectl https://storage.googleapis.com/kubernetes-release/release/$(curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt)/bin/linux/amd64/kubectl

chmod +x /tmp/kubectl
mv /tmp/kubectl /usr/bin

#kops
wget -O /tmp/kops https://github.com/kubernetes/kops/releases/download/$(curl -s https://api.github.com/repos/kubernetes/kops/releases/latest | grep tag_name | cut -d '"' -f 4)/kops-linux-amd64
chmod +x /tmp/kops
mv /tmp/kops /usr/bin

