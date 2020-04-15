# Ngrok Forwarding

For this example, I'm using my ngrok reserved domain of jodogrok. Go get your own!

This will connect ngrok, set up SSL and save it to `/letsencrypt` and configure nginx.

Please note, there is a limit on how many times you can do letsencrypt (like maybe 5 a week!) so save your `letsencrypt` folder.

If the `letsencrypt` folder exists, it will use these certs instead (will copy them to the right place in the container). If you change your ngrok domain name, you will have to delete this folder first as the certs will not work.

- Go to [ngrok](https://dashboard.ngrok.com/get-started) and login. You will need a pro plan for this
- Reserve your name (I did jordogrok)
- Edit `config.ini` and replace with your email and your domain name (`jordogrok.ngrok.io` was mine. Note, the example on ngrok site has "au" in it - leave this out)
- Edit `config.sh` and replace
  - SUBDOMAIN=jodogrok
  - AUTHTOKEN=get from ngrok dash under (3) Connect your account
  - REDIRECT=the address you want to redirect to on your local LAN

Run './host.sh' and you're off to the races! Access your domain to see the site that you're redirecting to.

Make sure your browser tells you the cert is working.

You may need to change the host networking type in `.devcontainer/docker-compose.yaml` if you are not seeing results of the forwarding. 