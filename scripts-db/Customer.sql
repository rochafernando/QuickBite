--drop database QuickBiteStaging;

--create database QuickBite;
create database QuickBiteStaging;
--create database QuickBiteDevelopment;

--use QuickBiteDevelopment;
use QuickBiteStaging;

--drop table Customer;
--drop table Category;
--drop table Product;
--drop table Orders;
--drop table MoneyOrder;


--CREATE TABLES
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

create table MoneyOrder (
	Id integer IDENTITY(1,1) PRIMARY KEY,
	Uid uniqueidentifier unique,
	OrderUid uniqueidentifier FOREIGN KEY REFERENCES Orders(Uid),
	TxId varchar(100) null,
	QRCode varchar(max) null,
	Status integer null,
	Value numeric(10,2) null,
	CreatedAt datetime null,
	UpdatedAt datetime null,
);

--INSERTS
insert into Customer(Uid, Name, Document, Email, CreatedAt)
values ('43c019c6-b7df-453c-8b2d-982c886c6b80', 'Naruto Uzumaki', '32145698710', 'naruto@teste.com', GETDATE());

insert into Category(Uid, Name, Description, CreatedAt)
values ('bf9dfa9a-da2e-423c-b145-b84b62497909', 'Lanches', 'Os mais deliciosos lanches da regi�o.', GETDATE());
insert into Category(Uid, Name, Description, CreatedAt)
values ('7dd939b6-4b65-4474-bc04-b0c867597726', 'Bebidas', 'Diversas bebidas para acompanhar seu lanche.', GETDATE());
insert into Category(Uid, Name, Description, CreatedAt)
values ('44266b4d-2614-468d-9789-56e2bd9e6d23', 'Acompanhamentos', 'Diversos acompanhamentos para seu lanche.', GETDATE());
insert into Category(Uid, Name, Description, CreatedAt)
values ('96153509-b6ce-46f8-aac1-d135d2a2c37a', 'Sobremesas', 'Para finalizar sua experi�ncia.', GETDATE());

insert into Product(Uid, Name, Description, CategoryUid, Enable, Sellable, UnitPrice, ComboPrice, CreatedAt)
values ('792a80f0-bbc3-48f6-b9f9-8fb08df717b2', 'Big Mac', 'O tradicional lanche.', 'bf9dfa9a-da2e-423c-b145-b84b62497909', 1, 1, 9.00, 6.50, GETDATE());
insert into Product(Uid, Name, Description, CategoryUid, Enable, Sellable, UnitPrice, ComboPrice, CreatedAt)
values ('9c652bba-9cc0-44a9-b3e6-3eec7d43139f', 'Coca Cola', 'O tradicional bebida.', '7dd939b6-4b65-4474-bc04-b0c867597726', 1, 1, 9.00, 5.50, GETDATE());

--QUERIES
select *

from Customer with(nolock);

select *
from Category with(nolock);

select *
from Product with(nolock);

select *
from Orders with(nolock);

select *
from MoneyOrder with(nolock);