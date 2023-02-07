#!/usr/bin/env bash
IP=`ip addr | grep -E 'eth0.*state UP' -A2 | tail -n 1 | awk '{print $2}' | cut -f1 -d '/'`
read -r -d '' MSG << EOM
{
  "id" : "producer-$IP",
  "name": "producer",
  "address": "$IP",
  "port": 80,
  "check": {
     "http": "http://$IP:80/healthz",
     "interval": "5s"
  }
}
EOM

curl -v -XPUT -d "$MSG" http://consul-client:8500/v1/agent/service/register && dotnet producer.dll "$@"