# MSSQLApp
Interview Task for C#/WPF Developer

Simple WPF Application which fetches data from DataBase and Displays it.

App requirements:
- Fetch all **int columns** and display them
- Ability to **type** login and password
- Ability to **test** if there is connection


DataBase Engine: **SQL Server 2019**

DataBase Client/Editor: **SSM 18**


Nuget Packages:
- MVVM Community Toolkit by Microsoft
- WPF Controls Extender for optional / future usage of BusyIndicator for asynchronouse data fetch

What can be done differently?
- instead of getting union, you / we can create Tabs which will contains only data from single Table. So creating Tabs and asynchronous download of data. Or synchronouse cuz there is no much data to begin with. It depends.
- TextBox for insterting server address. Just a possibility cuz for now you can only connect to localhost dbs.

DataBase schema:
```
CREATE DATABASE [DevData];
GO
CREATE TABLE [DevData].dbo.Table_A (Col_A1 int, Col_A2 varchar(10), Col_A3 date);
CREATE TABLE [DevData].dbo.Table_B (Col_B1 int, Col_B2 nchar(10), Col_B3 int);
CREATE TABLE [DevData].dbo.Table_C (Col_C1 varchar (10), Col_C2 timestamp, Col_C3 int, Col_C4 char(10));
```
