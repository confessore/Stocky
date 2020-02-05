#!/bin/sh

sudo systemctl stop stocky.net.service

sudo systemctl disable stocky.net.service

read -p "Stocky SQL Server Address: " sqlServer
read -p "Stocky SQL Username: " sqlUserId
read -p "Stocky SQL Password: " sqlPassword
read -p "Stocky SQL Database: " sqlDatabase

read -p "Stocky SMTP Address: " smtpAddress
read -p "Stocky SMTP Port: " smtpPort
read -p "Stocky SMTP Display Name: " smtpName
read -p "Stocky SMTP Email Address: " smtpEmail
read -p "Stocky SMTP Username: " smtpUserId
read -p "Stocky SMTP Password: " smtpPassword

read -p "Stocky Discord Application Id: " discordId
read -p "Stocky Discord Application Secret: " discordSecret
read -p "Stocky Discord Application Token: " discordToken

sudo cp ./services/stocky.net.service ./services/stocky.net.service.backup

sudo sed -i '/StockySQLServer=/s/$/'"$sqlServer"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySQLUserId=/s/$/'"$sqlUserId"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySQLPassword=/s/$/'"$sqlPassword"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySQLDatabase=/s/$/'"$sqlDatabase"'/' ./services/stocky.net.service.backup

sudo sed -i '/StockySMTPAddress=/s/$/'"$smtpAddress"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySMTPPort=/s/$/'"$smtpPort"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySMTPName=/s/$/'"$smtpName"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySMTPEmail=/s/$/'"$smtpEmail"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySMTPUserId=/s/$/'"$smtpUserId"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockySMTPPassword/s/$/'"$smtpPassword"'/' ./services/stocky.net.service.backup

sudo sed -i '/StockyDiscordId=/s/$/'"$discordId"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockyDiscordSecret=/s/$/'"$discordSecret"'/' ./services/stocky.net.service.backup
sudo sed -i '/StockyDiscordToken=/s/$/'"$discordToken"'/' ./services/stocky.net.service.backup

sudo mv ./services/stocky.net.service.backup /etc/systemd/system/stocky.net.service

sudo systemctl enable stocky.net.service

sudo systemctl start stocky.net.service
