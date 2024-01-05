--create database QuickBite;

use QuickBite;

--drop table Client;

create table Client (
	Id int primary key IDENTITY(1,1),
	Uid uniqueidentifier not null,
	Name varchar(200) null,
	Document varchar(20) null,
	Email varchar(200) null,
	CreationDate datetime null
);

select *
from Client with(nolock);