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
    openssl pkcs12 -export \
        -out certificate.pfx \
        -inkey ./letsencrypt/archive/$SUBDOMAIN/privkey1.pem \
        -in ./letsencrypt/archive/$SUBDOMAIN/cert1.pem \
        -certfile ./letsencrypt/archive/$SUBDOMAIN/chain1.pem \
        -passout pass:$CERTIFICATEPASSWORD
fi