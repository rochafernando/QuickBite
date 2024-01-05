--create database QuickBite;

use QuickBite;

--drop table Client;

create table Client (
	Id uniqueidentifier primary key,
	Name varchar(200) null,
	Email varchar(200) null,
	CreationDate datetime null
);

select *
from Client with(nolock);