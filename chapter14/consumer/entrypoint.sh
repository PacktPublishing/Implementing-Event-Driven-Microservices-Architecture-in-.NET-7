#!/usr/bin/env bash
IP=`ip addr | grep -E 'eth0.*state UP' -A2 | tail -n 1 | awk '{print $2}' | cut -f1 -d '/'`
read -r -d '' MSG << EOM
{
  "id" : "consumer-$IP",
  "name": "consumer",
  "address": "$IP",
  "port": 123,
  "check": {
     "tcp": "$IP:123",
     "interval": "5s"
  }
}
EOM
curl -v -XPUT -d "$MSG" http://consul-client:8500/v1/agent/service/register && dotnet consumer.dll "$@"