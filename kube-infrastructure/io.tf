// ---------------------------------------------------------------------------------------------------------------------
// Outputs
// ---------------------------------------------------------------------------------------------------------------------

output "main_network_id"            { value = "${module.main_network.id}" }
output "main_network_cidr_block"    { value = "${module.main_network.cidr_block}" }
output "main_network_azs"           { value = "${var.availability_zones}" }

// ---------------------------------------------------------------------------------------------------------------------
// Inputs
// ---------------------------------------------------------------------------------------------------------------------

variable "cluster_name" {
  description = "the name of this cluster"
}

variable "region" {
  description = "the AWS region to provision the cluster into"
}

variable "domain_name" {
  description = "the base domain name"
}

variable "vpc_cidr" {
  description = "CIDR block for the VPC"
  default     = "10.10.0.0/16"
}

variable "internal_subnets" {
  type        = "list"
  description = "list of subnet CIDR blocks that are not publicly acceessibly"
  default     = ["10.10.160.0/20", "10.10.176.0/20", "10.10.192.0/20", "10.10.208.0/20"]
}

variable "external_subnets" {
  type        = "list"
  description = "list of subnet CIDR blocks that are publicly acceessibly"
  default     = ["10.10.144.0/20", "10.10.128.0/20", "10.10.112.0/20", "10.10.96.0/20"]
}

variable "availability_zones" {
  description = "a comma separated list of EC2 availability zones"
}
