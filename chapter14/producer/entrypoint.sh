#!/usr/bin/env bash

IP=`ip addr | grep -E 'eth0.*state UP' -A2 | tail -n 1 | awk '{print $2}' | cut -f1 -d '/'`
NAME="$2"

read -r -d '' MSG << EOM
{
  "id" : "$NAME-$IP",
  "name": "$NAME",
  "address": "$IP",
  "port": $PORT,
  "check": {
     "http": "http://$IP:$PORT",
     "interval": "5s"
  }
}
EOM

curl -v -XPUT -d "$MSG" http://consul-client:8500/v1/agent/service/register && dotnet producer.dll "$@"