#!/bin/sh
docker-compose -f JobAssistantWebApi.yaml -H tcp://10.224.197.242:2375 build
