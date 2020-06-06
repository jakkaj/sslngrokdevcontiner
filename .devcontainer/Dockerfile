#-------------------------------------------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See https://go.microsoft.com/fwlink/?linkid=2090316 for license information.
#-------------------------------------------------------------------------------------------------------------
FROM ubuntu:bionic

# Configure apt
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get -y install --no-install-recommends apt-utils 2>&1

# Install git, process tools, lsb-release (common in install instructions for CLIs)
RUN apt-get -y install git procps lsb-release wget nano zsh nginx python-certbot-nginx psmisc

RUN wget https://github.com/robbyrussell/oh-my-zsh/raw/master/tools/install.sh -O - | zsh || true

# Save command line history 
RUN echo "export HISTFILE=/root/commandhistory/.zsh_history" >> "/root/.zshrc" \
    && echo "export PROMPT_COMMAND='history -a'" >> "/root/.zshrc" \
    && mkdir -p /root/commandhistory \
    && touch /root/commandhistory/.zsh_history

WORKDIR /workspace

RUN apt-get install -y software-properties-common
RUN add-apt-repository universe
RUN add-apt-repository ppa:certbot/certbot
RUN apt-get update

RUN apt-get install -y certbot python-certbot-nginx

ENV DEBIAN_FRONTEND=dialog

RUN chmod +x ./host.sh

RUN chsh -s /usr/bin/zsh
