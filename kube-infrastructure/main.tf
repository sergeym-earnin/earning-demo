terraform {
  backend "s3" {
  }
}

provider "aws" {
  region = "${var.region}"
}

// ---------------------------------------------------------------------------------------------------------------------
// Networking
// ---------------------------------------------------------------------------------------------------------------------

module "main_network" {
  source = "./modules/vpc"
  cidr = "${var.vpc_cidr}"
  azs = "${var.availability_zones}"
  name = "${var.cluster_name}"
  region = "${var.region}"
}