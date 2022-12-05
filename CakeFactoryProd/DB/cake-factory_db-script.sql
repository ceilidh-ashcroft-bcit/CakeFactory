
-------------------------------------------------------------
--- #1- Handle DATABASE
-------------------------------------------------------------
USE master;
GO

PRINT '#1- CREATE database ''CakeFactory''';
IF DB_ID('CakeFactory') IS NOT NULL 
BEGIN
	DROP DATABASE CakeFactory;
	PRINT '  CakeFactory DATABASE already exists and it''s been dropped and create again';
END
GO

CREATE DATABASE CakeFactory;
GO

USE CakeFactory;
GO


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
    isAdmin BIT DEFAULT 0 NOT NULL,
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
	[description] VARCHAR(200),
	isActive BIT DEFAULT 1 NOT NULL,
	imagePath VARCHAR(100),
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



-------------------------------------------------------------
--- #3- Handle INSERT data
-------------------------------------------------------------
PRINT CHAR(10) + '#3- INSERT data';
PRINT CHAR(10) + '#3.1- INSERT data INTO ''User''';
INSERT INTO [User] VALUES ('firstadmin@bcit.ca', 'First System Admin', 1, 'Admin1', '', 1);
INSERT INTO [User] VALUES ('secondadmin@bcit.ca', 'The Second Admin', 1, 'Admin2', '777-123-4567', 1);
INSERT INTO [User] VALUES ('admin@bcit.ca', 'The Main Admin', 1, 'Admin', '123-456-7890', 1);
INSERT INTO [User] VALUES ('client01@email.ca', 'The First Client', 0, 'First', '789-123-4567', 1);
INSERT INTO [User] VALUES ('client02@gmail.ca', 'Second Client', 0, 'Second', '888-999-1234', 1);

PRINT CHAR(10) + '#3.2- INSERT data INTO ''Shape''';
INSERT INTO Shape VALUES ('Rectangle', 0 , '', 1);
INSERT INTO Shape VALUES ('Square', 0.5, 'regular square shape', 1);
INSERT INTO Shape VALUES ('Oval', 0.2, 'it is an oval format', 1);
INSERT INTO Shape VALUES ('Triangle', 0.1 , 'equilateral triangle', 1);
INSERT INTO Shape VALUES ('Diamond', 0.15 , '', 1);

PRINT CHAR(10) + '#3.3- INSERT data INTO ''Size''';
INSERT INTO Size VALUES ('Very Small', '15cm X 15cm' , 1, 8.99);
INSERT INTO Size VALUES ('Small', '20cm X 15cm', 1, 14.99);
INSERT INTO Size VALUES ('Medium', '25cm X 20cm', 1, 22.99);
INSERT INTO Size VALUES ('Large', '30cm X 25cm' , 1, 32.99);
INSERT INTO Size VALUES ('Very Large', '40cm X 30cm', 1, 49.99);

PRINT CHAR(10) + '#3.4- INSERT data INTO ''Filling''';
INSERT INTO Filling VALUES ('Chocolate', 0, 'Delicious Chocolate', 1);
INSERT INTO Filling VALUES ('Vanilla', 0, 'Vanilla Super delicious', 1);
INSERT INTO Filling VALUES ('Strawberry', 0, 'Wonderfull Straberry', 1);
INSERT INTO Filling VALUES ('Aniversary', 0.1, 'Combination of Chocolate, Vanilla and Strawberry', 1);
INSERT INTO Filling VALUES ('Belgium Chocolate', 0, 'Exceptional Belgium Chocolate', 1);
INSERT INTO Filling VALUES ('Special', 0.15, 'Combination of Belgium Chocolate, Vanilla and Strawberry', 1);

PRINT CHAR(10) + '#3.5- INSERT data INTO ''Topping''';
INSERT INTO Topping VALUES ('Chocolate', 0, 'Delicious Chocolate', 1);
INSERT INTO Topping VALUES ('Vanilla', 0, 'Super Vanilla', 1);
INSERT INTO Topping VALUES ('Mix', 0.1, 'Chocolate and Vanilla', 1);
INSERT INTO Topping VALUES ('Multicolor', 0.2, 'Chocolate, Vanilla and Strawberry', 1);
INSERT INTO Topping VALUES ('Caramel', 0, 'Sweet Caramel', 1);

PRINT CHAR(10) + '#3.6- INSERT data INTO ''Cake''';
INSERT INTO Cake VALUES ('VS Special Chocolate', 8.99, 'Very Small Rectangle Chocolate', 1, null, 1, 1, 1);
INSERT INTO Cake VALUES ('S Special Chocolate', 14.99, 'Small Rectangle Chocolate', 1, 'K:\cake-images\cake1.jpg', 1, 2, 1);
INSERT INTO Cake VALUES ('M Special Chocolate', 22.99, 'Medium Rectangle Chocolate', 1, 'K:\cake-images\cake2-chocolate.jpg', 1, 3, 1);
INSERT INTO Cake VALUES ('L Special Chocolate', 32.99, 'Large Rectangle Chocolate', 1, 'K:\cake-images\cake-image3.jpg', 1, 4, 1);
INSERT INTO Cake VALUES ('VL Special Chocolate', 49.99, 'Very Large Rectangle Chocolate', 1, 'K:\cake-images\cake4.jpg', 1, 5, 1);
INSERT INTO Cake VALUES ('VS Great Vanilla', 8.99, 'Very Small Rectangle Vanilla', 1, 'K:\cake-images\cake-vanilla-1.jpg', 1, 1, 1);
INSERT INTO Cake VALUES ('S Great Vanilla', 14.99, 'Small Rectangle Vanilla', 1, 'K:\cake-images\cake-vanilla-22.jpg', 1, 2, 1);
INSERT INTO Cake VALUES ('M Great Vanilla', 22.99, 'Medium Rectangle Vanilla', 1, 'K:\cake-images\cake-vanilla-three.jpg', 1, 3, 1);
INSERT INTO Cake VALUES ('G Great Vanilla', 32.99, 'Large Rectangle Vanilla', 1, 'K:\cake-images\cake-vanilla-4444.jpg', 1, 4, 1);
INSERT INTO Cake VALUES ('VL Great Vanilla', 49.99, 'Very Large Rectangle Vanilla', 1, 'K:\cake-images\cake-vanilla-number5.jpg', 1, 5, 1);

PRINT CHAR(10) + '#3.7- INSERT data INTO ''CakeHasToppings''';
INSERT INTO CakeHasToppings VALUES (1, 1);
INSERT INTO CakeHasToppings VALUES (2, 1);
INSERT INTO CakeHasToppings VALUES (3, 1);
INSERT INTO CakeHasToppings VALUES (4, 1);
INSERT INTO CakeHasToppings VALUES (5, 1);
INSERT INTO CakeHasToppings VALUES (6, 2);
INSERT INTO CakeHasToppings VALUES (7, 2);
INSERT INTO CakeHasToppings VALUES (8, 2);
INSERT INTO CakeHasToppings VALUES (9, 2);
INSERT INTO CakeHasToppings VALUES (10, 2);

PRINT CHAR(10) + '#3.8- INSERT data INTO ''Order''';
INSERT INTO [Order] VALUES (null, null, 0, 8.99, 1, '2022-11-20', 3);
INSERT INTO [Order] VALUES (null, null, 0, 12.34, 1, '2022-11-10', 3);
INSERT INTO [Order] VALUES ('20221201', '20221203', 1, 30.15, 0, null, 4);
INSERT INTO [Order] VALUES ('2022-11-30', '20221201', 1, 50, 0, null, 5);
INSERT INTO [Order] VALUES ('2022-11-28', '2022-11-30', 1, 90, 0, null, 3);
INSERT INTO [Order] VALUES ('2022-11-25', '2022-11-26', 1, 88.12, 0, '2022-11-20', 5);
INSERT INTO [Order] VALUES ('2022-11-23', '2022-11-24', 1, 45.67, 0, '2022-11-20', 4);
INSERT INTO [Order] VALUES ('2022-12-01', '2022-12-02', 1, 49.99, 0, null, 5);
INSERT INTO [Order] VALUES ('2022-12-02', '2022-12-03', 1, 33.99, 0, null, 4);

PRINT CHAR(10) + '#3.9- INSERT data INTO ''OrderHasCakes''';
INSERT INTO OrderHasCakes VALUES (1, 8.99, 1, 1);
INSERT INTO OrderHasCakes VALUES (1, 14.99, 2, 2);
INSERT INTO OrderHasCakes VALUES (1, 8.99, 3, 1);
INSERT INTO OrderHasCakes VALUES (1, 14.99, 3, 2);
INSERT INTO OrderHasCakes VALUES (2, 8.99, 4, 1);
INSERT INTO OrderHasCakes VALUES (2, 14.99, 4, 2);
INSERT INTO OrderHasCakes VALUES (2, 22.99, 5, 3);
INSERT INTO OrderHasCakes VALUES (1, 8.99, 5, 1);
INSERT INTO OrderHasCakes VALUES (2, 14.99, 5, 2);
INSERT INTO OrderHasCakes VALUES (3, 22.99, 6, 3);
INSERT INTO OrderHasCakes VALUES (1, 14.99, 6, 2);
INSERT INTO OrderHasCakes VALUES (2, 22.99, 7, 3);
INSERT INTO OrderHasCakes VALUES (1, 49.99, 8, 5);
INSERT INTO OrderHasCakes VALUES (1, 32.99, 9, 4);



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