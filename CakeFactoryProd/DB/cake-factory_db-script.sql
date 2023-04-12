
-------------------------------------------------------------
--- #1- Handle DATABASE
-------------------------------------------------------------

-- # assuming we already scafolded the .net Identity Entity Framework
--USE master;
--GO

--PRINT '#1- CREATE database ''CakeFactory''';
--IF DB_ID('CakeFactory') IS NOT NULL 
--BEGIN
--	DROP DATABASE CakeFactory;
--	PRINT '  CakeFactory DATABASE already exists and it''s been dropped and create again';
--END
--GO

--CREATE DATABASE CakeFactory;
--GO

USE CakeFactory;
GO


-- DROP ALL TABLES AND REMOVE ALL ASP.NET USERS/ROLES
DROP TABLE IF EXISTS CakeHasToppings;
DROP TABLE IF EXISTS Topping;
DROP TABLE IF EXISTS OrderHasCakes;
DROP TABLE IF EXISTS Cake;
DROP TABLE IF EXISTS Filling;
DROP TABLE IF EXISTS Size;
DROP TABLE IF EXISTS Shape;
DROP TABLE IF EXISTS IPN;
DROP TABLE IF EXISTS [Order];
DROP TABLE IF EXISTS [User];
DELETE from AspNetRoles;
DELETE from AspNetUsers;
DELETE from AspNetUserRoles;


-------------------------------------------------------------
--- #2- Handle TABLES
-------------------------------------------------------------
-- Handle User table
PRINT CHAR(10) + '#2.1- CREATE table ''User''';
CREATE TABLE [User]
(
    id	INT PRIMARY KEY IDENTITY (1, 1),
    email VARCHAR(100) NOT NULL UNIQUE,
    [name] VARCHAR(60) NOT NULL,
	preferredName VARCHAR(60),
	phoneNumber VARCHAR(12),
	isActive BIT DEFAULT 1 NOT NULL
);
GO
IF OBJECT_ID('User') IS NOT NULL 
	PRINT '   OK: User table created successfully. :)';
ELSE
	PRINT '   ERROR: User table creation failed! :/';


-- Handle Shape table
PRINT CHAR(10) + '#2.2- CREATE table ''Shape''';
CREATE TABLE Shape
(
    id	INT PRIMARY KEY IDENTITY (1, 1),
    [value] VARCHAR(50) NOT NULL,
	priceFactor FLOAT NOT NULL,
	[description] VARCHAR(100),
	isActive BIT DEFAULT 1 NOT NULL
);
GO
IF OBJECT_ID('Shape') IS NOT NULL 
	PRINT '   OK: Shape table created successfully. :)';
ELSE
	PRINT '   ERROR: Shape table creation failed! :/';


-- Handle Size table
PRINT CHAR(10) + '#2.3- CREATE table ''Size''';
CREATE TABLE [Size]
(
    id	INT PRIMARY KEY IDENTITY (1, 1),
    [value] VARCHAR(50) NOT NULL,
	[description] VARCHAR(100),
	isActive BIT DEFAULT 1 NOT NULL,
	cakeBasicPrice MONEY NOT NULL
);
GO
IF OBJECT_ID('Size') IS NOT NULL 
	PRINT '   OK: Size table created successfully. :)';
ELSE
	PRINT '   ERROR: Size table creation failed! :/';


-- Handle Shape table
PRINT CHAR(10) + '#2.4- CREATE table ''Filling''';
CREATE TABLE Filling
(
    id	INT PRIMARY KEY IDENTITY (1, 1),
    flavor VARCHAR(50) NOT NULL,
	priceFactor FLOAT NOT NULL,
	[description] VARCHAR(100),
	isActive BIT DEFAULT 1 NOT NULL
);
GO
IF OBJECT_ID('Filling') IS NOT NULL 
	PRINT '   OK: Filling table created successfully. :)';
ELSE
	PRINT '   ERROR: Filling table creation failed! :/';


-- Handle Topping table
PRINT CHAR(10) + '#2.5- CREATE table ''Topping''';
CREATE TABLE Topping
(
    id	INT PRIMARY KEY IDENTITY (1, 1),
    flavor VARCHAR(50) NOT NULL,
	priceFactor FLOAT NOT NULL,
	[description] VARCHAR(100),
	isActive BIT DEFAULT 1 NOT NULL
);
GO
IF OBJECT_ID('Topping') IS NOT NULL 
	PRINT '   OK: Topping table created successfully. :)';
ELSE
	PRINT '   ERROR: Topping table creation failed! :/';


-- Handle Cake table
PRINT CHAR(10) + '#2.6- CREATE table ''Cake''';
CREATE TABLE Cake
(
    id INT PRIMARY KEY IDENTITY (1, 1),
    [name] VARCHAR(100) NOT NULL,
	price MONEY NOT NULL,
	[description] VARCHAR(500),
	isActive BIT DEFAULT 1 NOT NULL,
	imageName VARCHAR(MAX),
	imageCake varbinary(MAX), 
	isPredefined BIT DEFAULT 1 NOT NULL,
	fillingId INT NOT NULL,
    FOREIGN KEY (fillingId) REFERENCES Filling(id),
	sizeId INT NOT NULL,
    FOREIGN KEY (sizeId) REFERENCES Size(id),
	shapeId INT NOT NULL,
    FOREIGN KEY (shapeId) REFERENCES Shape(id),
);
GO
IF OBJECT_ID('Cake') IS NOT NULL 
	PRINT '   OK: Cake table created successfully. :)';
ELSE
	PRINT '   ERROR: Cake table creation failed! :/';



-- Handle CakeHasToppings table
PRINT CHAR(10) + '#2.7- CREATE table ''CakeHasToppings''';
CREATE TABLE CakeHasToppings
(
    id INT PRIMARY KEY IDENTITY (1, 1),
	cakeId int NOT NULL,
    FOREIGN KEY (cakeId) REFERENCES Cake(id),
	toppingId INT NOT NULL,
    FOREIGN KEY (toppingId) REFERENCES Topping(id),
); 
GO
IF OBJECT_ID('CakeHasToppings') IS NOT NULL 
	PRINT '   OK: CakeHasToppings table created successfully. :)';
ELSE
	PRINT '   ERROR: CakeHasToppings table creation failed! :/';



-- Handle Order table
PRINT CHAR(10) + '#2.8- CREATE table ''Order''';
CREATE TABLE [Order]
(
    id INT PRIMARY KEY IDENTITY (1, 1),
	purchaseDate DATE,
	pickupDate DATE,
	isPicked BIT DEFAULT 0 NOT NULL,
	totalAmount MONEY NOT NULL,
	openOrder BIT DEFAULT 0 NOT NULL,
	openOrderDate DATE,
	currency VARCHAR(3),
	paypalId VARCHAR(30),

	userId int NOT NULL,
    FOREIGN KEY (userId) REFERENCES [User](id)
);
GO
IF OBJECT_ID('Order') IS NOT NULL 
	PRINT '   OK: Order table created successfully. :)';
ELSE
	PRINT '   ERROR: Order table creation failed! :/';



-- Handle OrderHasCakes table
PRINT CHAR(10) + '#2.9- CREATE table ''OrderHasCakes''';
CREATE TABLE OrderHasCakes
(
	quantity INT NOT NULL,
	cost MONEY NOT NULL,
	orderId int NOT NULL,
    FOREIGN KEY (orderId) REFERENCES [Order](id),
	cakeId INT NOT NULL,
    FOREIGN KEY (cakeId) REFERENCES Cake(id),
);
GO
IF OBJECT_ID('OrderHasCakes') IS NOT NULL 
	PRINT '   OK: OrderHasCakes table created successfully. :)';
ELSE
	PRINT '   ERROR: OrderHasCakes table creation failed! :/';



-- Handle IPN table
PRINT CHAR(10) + '#2.10- CREATE table ''IPN - Instant Payment Notification''';
CREATE TABLE IPN
(
    id INT PRIMARY KEY IDENTITY (1, 1),

	[custom] VARCHAR(50) DEFAULT '',
	paymentID VARCHAR(30),
	cart VARCHAR(20) DEFAULT '',
	createTime VARCHAR(25) DEFAULT '',
	payerID VARCHAR(20) DEFAULT '',
	payerFirstName VARCHAR(20) DEFAULT '',
	payerLastName VARCHAR(20) DEFAULT '',
	payerMiddleName VARCHAR(20) DEFAULT '',
	payerEmail VARCHAR(100) DEFAULT '',
	payerCountryCode VARCHAR(3) DEFAULT '',
	payerStatus VARCHAR(20) DEFAULT '',
	amount VARCHAR(20) DEFAULT '',
	currency VARCHAR(3) DEFAULT '',
	intent VARCHAR(15) DEFAULT '',
	paymentMethod VARCHAR(20) DEFAULT '',
	paymentState VARCHAR(20) DEFAULT '',

	--orderId INT NOT NULL,
	orderId INT,
    FOREIGN KEY (orderId) REFERENCES [Order](id),
); 
GO
IF OBJECT_ID('IPN') IS NOT NULL 
	PRINT '   OK: IPN table created successfully. :)';
ELSE
	PRINT '   ERROR: IPN table creation failed! :/';



-------------------------------------------------------------
--- #3- Handle INSERT data
-------------------------------------------------------------
PRINT CHAR(10) + '#3- INSERT data';
PRINT CHAR(10) + '#3.1- INSERT data INTO ''User''';
INSERT INTO [User] VALUES ('admin@cakefactory.ca', 'First System Admin', 'Admin1', '', 1);

INSERT INTO AspNetUsers VALUES ('1', 'First System Admin', 'First System Admin', 'admin@cakefactory.ca',
'', '', '', '', '', '', '', '', '', '', '');


INSERT INTO AspNetRoles VALUES ('1', 'Admin', 'ADMIN', '');
INSERT INTO AspNetRoles VALUES ('2', 'Manager', 'MANAGER', '');
INSERT INTO AspNetRoles VALUES ('3', 'Customer', 'CUSTOMER', '');

INSERT INTO AspNetUserRoles VALUES ('1', '1');


PRINT CHAR(10) + '#3.2- INSERT data INTO ''Shape''';
INSERT INTO Shape VALUES ('Rectangle', 0 , '', 1);
INSERT INTO Shape VALUES ('Square', 2, 'regular square shape', 1);
INSERT INTO Shape VALUES ('Oval', 5, 'it is an oval format', 1);
INSERT INTO Shape VALUES ('Triangle', 6 , 'equilateral triangle', 1);
INSERT INTO Shape VALUES ('Diamond', 7 , '', 1);

PRINT CHAR(10) + '#3.3- INSERT data INTO ''Size''';
INSERT INTO Size VALUES ('Small', '20cm X 15cm', 1, 14.99);
INSERT INTO Size VALUES ('Medium', '25cm X 20cm', 1, 22.99);
INSERT INTO Size VALUES ('Large', '30cm X 25cm' , 1, 32.99);

PRINT CHAR(10) + '#3.4- INSERT data INTO ''Filling''';
INSERT INTO Filling VALUES ('Chocolate', 0, 'Delicious Chocolate', 1);
INSERT INTO Filling VALUES ('Carrot', 0, 'Delicious Carrot', 1);
INSERT INTO Filling VALUES ('Vanilla', 0, 'Vanilla Super delicious', 1);
INSERT INTO Filling VALUES ('Strawberry', 0, 'Wonderfull Straberry', 1);
INSERT INTO Filling VALUES ('Aniversary', 3, 'Combination of Chocolate, Vanilla and Strawberry', 1);
INSERT INTO Filling VALUES ('Belgium Chocolate', 0, 'Exceptional Belgium Chocolate', 1);
INSERT INTO Filling VALUES ('Special', 3, 'Combination of Belgium Chocolate, Vanilla and Strawberry', 1);

PRINT CHAR(10) + '#3.5- INSERT data INTO ''Topping''';
INSERT INTO Topping VALUES ('Chocolate', 0, 'Delicious Chocolate', 1);
INSERT INTO Topping VALUES ('Vanilla', 0, 'Super Vanilla', 1);
INSERT INTO Topping VALUES ('Mix', 2, 'Chocolate and Vanilla', 1);
INSERT INTO Topping VALUES ('Multicolor', 3, 'Chocolate, Vanilla and Strawberry', 1);
INSERT INTO Topping VALUES ('Caramel', 0, 'Sweet Caramel', 1);

PRINT CHAR(10) + '#3.6- INSERT data INTO ''Cake''';
--INSERT INTO Cake VALUES ('Chocolate Cake', 22.99, 
--'A rich, decadent chocolate cake with a moist, fluffy texture. Layers of velvety chocolate cake are sandwiched between creamy chocolate frosting, and the entire cake is coated with a smooth, glossy chocolate ganache. The cake is finished off with  truffle balls of chocolate , adding a final touch of indulgence to this classic dessert.',
--1, 'chocolateCake.jpg', 1, 1, 2, 1);
--INSERT INTO Cake VALUES ('Carrot Cake', 22.99, 
--'A deliciously moist carrot cake with a tender crumb and warm spices. The cake is made with freshly grated carrots and a blend of cinnamon, nutmeg, and ginger, which give it a cozy, autumnal flavor. The layers are filled and topped with a luscious cream cheese frosting that complements the sweetness of the carrots perfectly.',
--1, 'carrotCake.jpg', 1, 2, 2, 1);
INSERT INTO Cake VALUES ('Chocolate Cake', 22.99, 
'A rich, decadent chocolate cake with a moist, fluffy texture. Layers of velvety chocolate cake are sandwiched between creamy chocolate frosting, and the entire cake is coated with a smooth, glossy chocolate ganache. The cake is finished off with  truffle balls of chocolate , adding a final touch of indulgence to this classic dessert.',
1,'chocolateCake.jpg', (SELECT * FROM OPENROWSET 
	(BULK N'K:\internal-project\CakeFactory\CakeFactoryProd\wwwroot\images\chocolateCake.jpg', SINGLE_BLOB) AS imageCake), 
1, 1, 2, 1);
INSERT INTO Cake VALUES ('Carrot Cake', 22.99, 
'A deliciously moist carrot cake with a tender crumb and warm spices. The cake is made with freshly grated carrots and a blend of cinnamon, nutmeg, and ginger, which give it a cozy, autumnal flavor. The layers are filled and topped with a luscious cream cheese frosting that complements the sweetness of the carrots perfectly.',
1,'carrotCake.jpg', (SELECT * FROM OPENROWSET 
	(BULK N'K:\internal-project\CakeFactory\CakeFactoryProd\wwwroot\images\carrotCake.jpg', SINGLE_BLOB) AS imageCake), 1, 2, 2, 1);

PRINT CHAR(10) + '#3.7- INSERT data INTO ''CakeHasToppings''';
INSERT INTO CakeHasToppings VALUES (1, 1);
INSERT INTO CakeHasToppings VALUES (2, 1);

PRINT CHAR(10) + '#3.8- INSERT data INTO ''Order''';
INSERT INTO [Order] VALUES (null, null, 0, 8.99, 1, '2022-11-20', 'CAD', 'PAYPAL-ID#00001', 1);

PRINT CHAR(10) + '#3.9- INSERT data INTO ''OrderHasCakes''';
INSERT INTO OrderHasCakes VALUES (1, 8.99, 1, 1);


--IPN test
--insert into IPN values ('custom1', '1','','','','','','','','','','','','','','',1);
--insert into IPN values ('custom2', '1','','','','','','','','','','','','','','',2);
--insert into IPN values ('custom3', '1','','','','','','','','','','','','','','',3);


-------------------------------------------------------------
--- #4- Display data - SELECT
-------------------------------------------------------------
/*
PRINT CHAR(10) + '#4- SELECT data';
PRINT CHAR(10) + '#4.1- SELECT data FROM ''User''';
SELECT * FROM [User];

PRINT CHAR(10) + '#4.2- SELECT data FROM ''Shape''';
SELECT * FROM Shape;

PRINT CHAR(10) + '#4.3- SELECT data FROM ''Size''';
SELECT * FROM Size;

PRINT CHAR(10) + '#4.4- SELECT data FROM ''Filling''';
SELECT * FROM Filling;

PRINT CHAR(10) + '#4.5- SELECT data FROM ''Topping''';
SELECT * FROM Topping;

PRINT CHAR(10) + '#4.6- SELECT data FROM ''Cake''';
SELECT * FROM Cake;

PRINT CHAR(10) + '#4.7- SELECT data FROM ''CakeHasToppings''';
SELECT * FROM CakeHasToppings;

PRINT CHAR(10) + '#4.8- SELECT data FROM ''Order''';
SELECT * FROM [Order];

PRINT CHAR(10) + '#4.9- SELECT data FROM ''OrderHasCakes''';
SELECT * FROM OrderHasCakes;
*/




/*
-------------------------------------------------------------
--- #5- Delete data
-------------------------------------------------------------
DELETE FROM CakeHasToppings;
DELETE FROM Topping;
DELETE FROM OrderHasCakes;
DELETE FROM Cake;
DELETE FROM Filling;
DELETE FROM Size;
DELETE FROM Shape;
DELETE FROM Order;
DELETE FROM [User];

select * from AspNetRoles
select * from AspNetUsers
select * from AspNetUserRoles
*/
