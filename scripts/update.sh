#!/bin/sh

sudo service stocky.net stop

cd /home/$USER/stocky
sudo git pull origin master

cd /home/$USER/stocky/src/Stocky.Net
sudo dotnet publish -c Release -o /var/dotnetcore/Stocky.Net

sudo service stocky.net start
