#!/bin/bash

. ./config.sh

ngrok authtoken $AUTHTOKEN

FULLPATHCERTS=/etc/letsencrypt

DIR=/workspace/letsencrypt/renewal

if test -d "$DIR"; then
    echo "Certs exist, copying"
    if [ ! -d $FULLPATHCERTS ]; then
        mkdir $FULLPATHCERTS
    fi
    cp -r ./letsencrypt /etc
else
    ngrok http -host-header="$SUBDOMAIN.ngrok.io" -subdomain="$SUBDOMAIN" 80 > /dev/null &
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

ngrok start -all -config ngrok.yaml > /dev/null &
