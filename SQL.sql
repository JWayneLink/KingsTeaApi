use KTA

/*
--ACCOUNT TABLE
--create table ACCOUNT
(
	Id int identity(1,1),
	Account varchar(100) not null default '',
	Name varchar(100) not null default '',
	Pwd varchar(100) not null default '',
	Email varchar(100) not null default '',
	Phone varchar(100)  not null default '',
	Cdt datetime not null default getdate(),
	Udt datetime not null default getdate()
)

--PRODUCT TABLE
--create table PRODUCT
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
create table CUSTOMER
(
	Id int identity(1,1),
	CustId varchar(100) not null default '',
	Name varchar(100) not null default '',
	Title varchar(100) not null default '',
	Address varchar(100) not null default '',
	Phone varchar(100) not null default '',
	Cdt datetime not null default getdate(),
	Udt datetime not null default getdate()
)

*/



select * from ACCOUNT (nolock)
select * from PRODUCT (nolock)
select * from SALESORDER (nolock)
select * from CUSTOMER (nolock)





/*
INSERT ACCOUNT
insert into ACCOUNT select 'KTA0221','Wayne','kta0221','kingsteakingstea@gmail.com','0912123123',getdate(),getdate()
insert into ACCOUNT select 'KTA-0001','Bret','Bret','Sincere@april.biz','02-12345678',getdate(),getdate()
insert into ACCOUNT select 'KTA-0002','Antonette','Antonette','Shanna@melissa.tv','0233456789',getdate(),getdate()
insert into ACCOUNT select 'KTA-0003','Samantha','Samantha','Nathan@yesenia.net','23456789',getdate(),getdate()
insert into ACCOUNT select 'KTA-0004','Karianne','Karianne','Julianne.OConner@kory.org','02-23456789#1234',getdate(),getdate()
insert into ACCOUNT select 'KTA-0005','Kamren','Kamren','Lucio_Hettinger@annie.ca','23456789#1234',getdate(),getdate()
insert into ACCOUNT select 'KTA-0006','Corkery','Corkery','Karley_Dach@jasper.info','0233456789#1234',getdate(),getdate()
insert into ACCOUNT select 'KTA-0007','Elwyn','Elwyn','Telly.Hoeger@billy.biz','0912-123-456',getdate(),getdate()
insert into ACCOUNT select 'KTA-0008','Maxime','Maxime','Sherwood@rosamond.me','0912-123456',getdate(),getdate()
insert into ACCOUNT select 'KTA-0009','Delphine','Delphine','Chaim_McDermott@dana.io','0912000123',getdate(),getdate()
insert into ACCOUNT select 'KTA-0010','Clementina','Clementina','Rey.Padberg@karina.biz','0912-234567',getdate(),getdate()

*/




/*
INSERT CUSTOMER
insert into CUSTOMER select 'CX-001','Leanne Graham','Mr.','Gwenborough, Kulas Ligh Street, Apt. 556','02-12345678',getdate(),getdate()
insert into CUSTOMER select 'CX-002','Ervin Howell','Mrs.','Wisokyburgh, Victor Plains, Suite 879','0233456789',getdate(),getdate()
insert into CUSTOMER select 'CX-003','Clementine Bauch','Miss.','McKenziehaven, Douglas Extension, Suite 879','23456789',getdate(),getdate()
insert into CUSTOMER select 'CX-004','Patricia Lebsack','Mr.','South Elvis, Hoeger Mall, Apt. 692','02-23456789#1234',getdate(),getdate()
insert into CUSTOMER select 'CX-005','Chelsey Dietrich','Mrs.','Roscoeview, Skiles Walks, Suite 351','23456789#1234',getdate(),getdate()
insert into CUSTOMER select 'CX-006','Dennis Schulist','Miss.','South Christy, Norberto Crossing, Apt. 950','0233456789#1234',getdate(),getdate()
insert into CUSTOMER select 'CX-007','Kurtis Weissnat','Ms.','Howemouth, Rex Trail, Suite 280','0912-123-456',getdate(),getdate()
insert into CUSTOMER select 'CX-008','Nicholas Runolfsdottir V','Mr.','Aliyaview, Ellsworth Summit, Suite 729','0912-123456',getdate(),getdate()
insert into CUSTOMER select 'CX-009','Glenna Reichert','Mrs.','Bartholomebury, Dayna Park, Suite 449','0912000123',getdate(),getdate()
insert into CUSTOMER select 'CX-010','Clementina DuBuque','Miss.','Lebsackbury, Kattie Turnpike, Suite 198','0912-234567',getdate(),getdate()
*/


/*
truncate table ACCOUNT
truncate table PRODUCT
truncate table CUSTOMER
truncate table SALESORDER
*/
