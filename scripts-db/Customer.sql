--create database QuickBite;
--create database QuickBiteStaging;
--create database QuickBiteDevelopment;

use QuickBiteDevelopment;

--drop table Customer;
--drop table Category;
--drop table Product;

create table Customer (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	Name varchar(200) null,
	Document varchar(20) null,
	Email varchar(200) null,
	CreationDate datetime null
);

create table Category (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	Name varchar(200) null,
	Description varchar(200) null,
	CreationDate datetime null
);


create table Product (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier not null,
	CategoryUid uniqueidentifier FOREIGN KEY REFERENCES Category(Uid),
	Name varchar(200) null,
	Description varchar(200) null,
	Sellable bit null,
	Enable bit null,
	UnitPrice numeric(10,2),
	ComboPrice numeric(10,2),
	CreatedAt datetime null,
	UpdatedAt datetime null,

);

select *
from Customer with(nolock);

select *
from Category with(nolock);

select *
from Product with(nolock);