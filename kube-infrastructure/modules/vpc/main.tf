// ---------------------------------------------------------------------------------------------------------------------
// VPC
// ---------------------------------------------------------------------------------------------------------------------

resource "aws_vpc" "main" {
  cidr_block = "${ var.cidr }"

  enable_dns_hostnames = true
  enable_dns_support = true

  tags {
    Name = "kubernetes-${ var.name }"
    visibility = "private,public"
  }
}

resource "null_resource" "dummy_dependency" {
  depends_on = [
    "aws_vpc.main",
    "aws_nat_gateway.nat"
  ]
}

// ---------------------------------------------------------------------------------------------------------------------
// Private network
// ---------------------------------------------------------------------------------------------------------------------


resource "aws_eip" "nat" { vpc = true }

resource "aws_nat_gateway" "nat" {
  depends_on = [
    "aws_eip.nat",
    "aws_internet_gateway.main",
  ]

  allocation_id = "${ aws_eip.nat.id }"
  subnet_id = "${ aws_subnet.public.0.id }"
}

resource "aws_subnet" "private" {
  count = "${ length( split(",", var.azs) ) }"

  availability_zone = "${ element( split(",", var.azs), count.index ) }"
  cidr_block = "${ cidrsubnet(var.cidr, 8, count.index + 10) }"
  vpc_id = "${ aws_vpc.main.id }"

  tags {
    Name = "kubernetes-${ var.name }-private"
    visibility = "private"
  }
}

resource "aws_route_table" "private" {
  vpc_id = "${ aws_vpc.main.id }"

  route {
    cidr_block = "0.0.0.0/0"
    nat_gateway_id = "${ aws_nat_gateway.nat.id }"
  }

  tags {
    Name = "kubernetes-${ var.name }"
    visibility = "private"
  }
}

resource "aws_route_table_association" "private" {
  count = "${ length(split(",", var.azs)) }"

  route_table_id = "${ aws_route_table.private.id }"
  subnet_id = "${ element(aws_subnet.private.*.id, count.index) }"
}

// ---------------------------------------------------------------------------------------------------------------------
// Public network
// ---------------------------------------------------------------------------------------------------------------------

resource "aws_internet_gateway" "main" {
  vpc_id = "${ aws_vpc.main.id }"

  tags {
    Name = "kubernetes-${ var.name }"
  }
}

resource "aws_subnet" "public" {
  count = "${ length( split(",", var.azs) ) }"

  availability_zone = "${ element( split(",", var.azs), count.index ) }"
  cidr_block = "${ cidrsubnet(var.cidr, 8, count.index) }"
  vpc_id = "${ aws_vpc.main.id }"

  tags {
    Name = "kubernetes-${ var.name }-public"
    visibility = "public"
  }
}

resource "aws_route" "public" {
  route_table_id = "${ aws_vpc.main.main_route_table_id }"
  destination_cidr_block = "0.0.0.0/0"
  gateway_id = "${ aws_internet_gateway.main.id }"
}

resource "aws_route_table_association" "public" {
  count = "${ length(split(",", var.azs)) }"

  route_table_id = "${ aws_vpc.main.main_route_table_id }"
  subnet_id = "${ element(aws_subnet.public.*.id, count.index) }"
}
