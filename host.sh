#!/bin/bash

. ./config.sh

FULLPATHCERTS=/etc/letsencrypt

./ngrok/ngrok authtoken $AUTHTOKEN

DIR=/workspace/letsencrypt/renewal

if test -d "$DIR"; then
    echo "Certs exist, copying"
    if [ ! -d $FULLPATHCERTS ]; then
        mkdir $FULLPATHCERTS
    fi
    cp -r ./letsencrypt /etc
else
    ./ngrok/ngrok http -host-header="$SUBDOMAIN.ngrok.io" -subdomain="$SUBDOMAIN" 80 > /dev/null &
    #wait for ngrok
    sleep 5s
    certbot certonly --config config.ini --standalone --preferred-challenges http
    cp -r $FULLPATHCERTS ./
    openssl pkcs12 -export \
        -out certificate.pfx \
        -inkey ./letsencrypt/archive/$SUBDOMAIN.ngrok.io/privkey1.pem \
        -in ./letsencrypt/archive/$SUBDOMAIN.ngrok.io/cert1.pem \
        -certfile ./letsencrypt/archive/$SUBDOMAIN.ngrok.io/chain1.pem \
        -passout pass:$CERTIFICATEPASSWORD
fi

killall ngrok

./ngrok/ngrok http -host-header="$SUBDOMAIN.ngrok.io" -subdomain="$SUBDOMAIN" 443 > /dev/null &

cp ./nginx.conf ./nginx.edited.conf

REDIRESCAPED=$(echo "${REDIRECT}" | sed -e 's/[\/&]/\\&/g' )

sed -i "s/domain/$SUBDOMAIN.ngrok.io/g" nginx.edited.conf
sed -i "s/redirectaddress/$REDIRESCAPED/g" nginx.edited.conf

cp ./nginx.edited.conf /etc/nginx/nginx.conf
kill $(ps aux | grep '[n]ginx' | awk '{print $2}')

nginx

echo "Updated config and restarted nginx"
