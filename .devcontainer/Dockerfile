#-------------------------------------------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See https://go.microsoft.com/fwlink/?linkid=2090316 for license information.
#-------------------------------------------------------------------------------------------------------------
FROM ubuntu:bionic

# Configure apt
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get -y install --no-install-recommends apt-utils 2>&1

RUN apt-get install -y software-properties-common \
    && add-apt-repository universe \
    && add-apt-repository ppa:certbot/certbot \
    && apt-get update

# Install git, process tools, lsb-release (common in install instructions for CLIs)
RUN apt-get -y install software-properties-common git procps lsb-release wget nano zsh certbot python-certbot-nginx psmisc

RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb 
RUN apt-get update; \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y dotnet-sdk-3.1

RUN wget https://github.com/robbyrussell/oh-my-zsh/raw/master/tools/install.sh -O - | zsh || true

# Save command line history 
RUN echo "export HISTFILE=/root/commandhistory/.zsh_history" >> "/root/.zshrc" \
    && echo "export PROMPT_COMMAND='history -a'" >> "/root/.zshrc" \
    && mkdir -p /root/commandhistory \
    && touch /root/commandhistory/.zsh_history

WORKDIR /workspace

ENV PATH="/workspace/ngrok/:$PATH"
ENV DEBIAN_FRONTEND=dialog

RUN chsh -s /usr/bin/zsh
