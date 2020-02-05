#!/bin/sh

sudo systemctl stop stocky.net.service

sudo systemctl disable stocky.net.service

sudo rm /etc/systemd/system/stocky.net.service
