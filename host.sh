#!/bin/bash

. ./config.sh

FULLPATHCERTS=/etc/letsencrypt

DIR=/workspace/letsencrypt/renewal

if test -d "$DIR"; then
    echo "Certs exist, copying"
    if [ ! -d $FULLPATHCERTS ]; then
        mkdir $FULLPATHCERTS
    fi
    cp -r ./letsencrypt /etc
else
    certbot certonly --config config.ini --standalone --preferred-challenges http
    cp -r $FULLPATHCERTS ./    
fi



cp ./nginx.conf ./nginx.edited.conf

REDIRESCAPED=$(echo "${REDIRECT}" | sed -e 's/[\/&]/\\&/g' )

sed -i "s/domain/$DOMAIN/g" nginx.edited.conf
sed -i "s/redirectaddress/$REDIRESCAPED/g" nginx.edited.conf

cp ./nginx.edited.conf /etc/nginx/nginx.conf
kill $(ps aux | grep '[n]ginx' | awk '{print $2}')

nginx

echo "Updated config and restarted nginx"

tail -f /var/log/nginx/access.log