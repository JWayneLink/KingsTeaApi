use KTA


--ACCOUNT TABLE
--create table ACCOUNT
--(
--	Id int identity(1,1),
--	Account varchar(100) not null default '',
--	Name varchar(100) not null default '',
--	Pwd varchar(100) not null default '',
--	Email varchar(100) not null default '',
--	Phone varchar(100)  not null default '',
--	Cdt datetime not null default getdate(),
--	Udt datetime not null default getdate()
--)

--PRODUCT TABLE
create table PRODUCT
(
	Id int identity(1,1),
	Pn varchar(100) not null default '',
	Name varchar(100) not null default '',
	Category varchar(100) not null default '',
	Size varchar(100) not null default '',
	Sugar varchar(100) not null default '',
	Ice varchar(100) not null default '',
	Price money not null default 0,
	Cdt datetime not null default getdate(),
	Udt datetime not null default getdate()
)



--SALESORDER TABLE
create table SALESORDER
(
	Id int identity(1,1),
	SO varchar(100) not null default '',
	Pn varchar(100) not null default '',
	CustId varchar(100) not null default '',
	Qty int not null default 0,
	Status varchar(100) not null default '',
	Creator varchar(100) not null default  '',
	Cdt datetime not null default getdate(),
	Udt datetime not null default getdate()
)

--CUSTOMER TABLE
--create table CUSTOMER
--(
--	Id int identity(1,1),
--	CustId varchar(100) not null default '',
--	Name varchar(100) not null default '',
--	Title varchar(100) not null default '',
--	Address varchar(100) not null default '',
--	Phone varchar(100) not null default '',
--	Cdt datetime not null default getdate(),
--	Udt datetime not null default getdate()
--)


select * from ACCOUNT (nolock)
select * from PRODUCT (nolock)
select * from SALESORDER (nolock)
select * from CUSTOMER (nolock)