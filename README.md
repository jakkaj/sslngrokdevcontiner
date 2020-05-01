# Ngrok Forwarding

## Requirements

- Follow [VS Code remote containers installation guide](https://code.visualstudio.com/docs/remote/containers#_installation)
- [Make sure C drive is shared in docker](https://github.com/docker/for-win/issues/3174#issuecomment-477417558)
- [Run the project in a VS Code container](https://code.visualstudio.com/docs/remote/containers#_quick-start-open-an-existing-folder-in-a-container)

## Instructions

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
  - CERTIFICATEPASSWORD=password used when saving certificate.pfx
- Edit `ngrok.yaml` and replace SUBDOMAIN with your subdomain.

Open a VSCode Terminal, run './host.sh' and you're off to the races! Access your domain to see the site that you're redirecting to.

Make sure your browser tells you the cert is working.

You may need to change the host networking type in `.devcontainer/docker-compose.yaml` if you are not seeing results of the forwarding. 

## Troubleshooting

### Issues related to bash commands

Make sure line endings are in unix format. Use `dos2unix` if Windows `git` checked out files in with incompatible line endings.