# PDP

The poupose of this project is to give an easy and quick interface order to manage different stock portfolios, if you have an account in multiple brokers it get's hard to have our whole portfolio in one interface, without having to add any personal data.

It consumes :
Alphavantage api : in order to syncronize current stock prices 
  * https://www.alphavantage.co/
  
Currency api : To give flexibility and allow calculation among different currencies.
  * https://currencyapi.com/
 

PortfolioAnalyzer currently composed of two applications

First an API to allow management of stock portfolio and bank accounts, you can manually manage your current portfolio and generate a report with previous generated data

![image](https://user-images.githubusercontent.com/33600155/215721827-fd26a8d6-3be7-4e30-a2ab-b763aafaf910.png)

Also a simple console application in order to syncronize your portfolio and get current state in your favourite currency
![image](https://user-images.githubusercontent.com/33600155/215723393-a0b04fc9-214a-4ad0-b1e9-20300ff31bc4.png)


How to use?

- First use the API to register your portoflios and bank accounts
- Then run the console app in order to syncronize current stock and bank data.
- Call ​/api​/PortfolioHistory​/GetLatestInfo to get your report with your favourite currency

![image](https://user-images.githubusercontent.com/33600155/215724408-d7f4c5d2-5672-496d-ba61-86036ea3b9a6.png)
