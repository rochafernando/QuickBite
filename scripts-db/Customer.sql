--create database QuickBite;

use QuickBite;

--drop table Client;

create table Custormers (
	Id uniqueidentifier primary key,
	Name varchar(200) null,
	Email varchar(200) null,
	CreationDate datetime null
);

select *
from Custormers with(nolock);