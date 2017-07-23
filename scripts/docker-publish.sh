#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''
case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -f ./src/Passenger.Api/Dockerfile.$DOCKER_ENV -t passenger.api:$DOCKER_TAG ./src/Passenger.Api
docker tag passenger.api:$DOCKER_TAG $DOCKER_USERNAME/passenger.api:$DOCKER_TAG
docker push $DOCKER_USERNAME/passenger.api:$DOCKER_TAG