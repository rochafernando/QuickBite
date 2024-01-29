--create database QuickBite;
--create database QuickBiteStaging;
--create database QuickBiteDevelopment;

use QuickBiteDevelopment;

--drop table Customer;
--drop table Category;
--drop table Product;
--drop table Orders;

create table Customer (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	Name varchar(200) null,
	Document varchar(20) null,
	Email varchar(200) null,
	CreatedAt datetime null,
	UpdatedAt datetime null
);

create table Category (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	Name varchar(200) null,
	Description varchar(200) null,
	CreatedAt datetime null,
	UpdatedAt datetime null,
);


create table Product (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
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

create table Orders (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	Customer varchar(max) null,
	Items varchar(max) null,
	Status integer null,
	Value numeric(10,2) null,
	CreatedAt datetime null,
	UpdatedAt datetime null,
);

select *
from Customer with(nolock);

select *
from Category with(nolock);

select *
from Product with(nolock);

select *
from Orders with(nolock);