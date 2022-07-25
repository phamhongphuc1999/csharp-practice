#! /bin/bash

Green='\033[0;32m'
NC='\033[0m'

IMAGE_NAME="c_sharp_tutorial"
CONTAINER_NAME="c_sharp_container"

printf "${Green}Step1: Untag old docker image${NC}\n"
if [ "$(docker images -q $IMAGE_NAME:v1)" ]
then
  docker image tag $IMAGE_NAME:v1 $IMAGE_NAME-old:v1
  docker rmi $IMAGE_NAME:v1
fi

printf "${Green}Step2: Build new image${NC}\n"
docker build . -t $IMAGE_NAME:v1

printf "${Green}Step3: Stop and remove old container${NC}\n"
if [ "$(docker ps -aq -f status=running -f name=$CONTAINER_NAME)" ]
then
  docker stop $CONTAINER_NAME
fi

if [ "$(docker ps -aq -f status=exited -f name=$CONTAINER_NAME)" ]
then
  docker rm $CONTAINER_NAME
fi

printf "${Green}Step4: Remove old image${NC}\n"
if [ "$(docker images -q $IMAGE_NAME-old:v1)" ]
then
  docker rmi $IMAGE_NAME-old:v1
fi

docker run -d --name $CONTAINER_NAME $IMAGE_NAME:v1
