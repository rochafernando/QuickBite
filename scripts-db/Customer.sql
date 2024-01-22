--create database QuickBite;
--create database QuickBiteStaging;
--create database QuickBiteDevelopment;

use QuickBiteStaging;

--drop table Customer;

--create table Customer (
--	Id integer IDENTITY(1,1) PRIMARY KEY,
--	Uid uniqueidentifier not null,
--	Name varchar(200) null,
--	Document varchar(20) null,
--	Email varchar(200) null,
--	CreationDate datetime null
--);

select *
from Customer with(nolock);