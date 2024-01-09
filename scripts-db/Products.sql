use QuickBite;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN 
 Create TABLE ProductTypes (
    Id INT PRIMARY KEY IDENTITY,
        [Name] VARCHAR(50) NOT NULL,
     )

    INSERT INTO ProductTypes ([Name]) VALUES ('Ingredient')
    INSERT INTO ProductTypes ([Name]) VALUES ('UniqueProduct')
END


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        -- Define your table columns here
        Id INT PRIMARY KEY IDENTITY,
        [Name] VARCHAR(50),
        [Description] VARCHAR(150),
        PathImage VARCHAR(100),
        Sellable BIT NOT NULL,        
        CategoryId INT NULL FOREIGN KEY REFERENCES Categories(Id),
        ProductTypeId INT FOREIGN KEY REFERENCES ProductTypes(Id),
        CreatedAt Datetime NOT NULL,
        UpdatedAt Datetime NULL
    )
END

Insert into Products ([Name]
, [Description]
, PathImage
, Sellable
, CategoryID
, ProductTypeID
, CreatedAt) 
values ('Pão Francês'
, 'Pão francês tradicional'
, 'c:\images\pao-frances.jpg'
, 0
, null
, 1
, GETDATE())

Insert into Products ([Name]
, [Description]
, PathImage
, Sellable
, CategoryID
, ProductTypeID
, CreatedAt) 
values ('Sal - 10 mg'
, 'Sal comum'
, 'c:\images\sal.jpg'
, 0
, null
, 1
, GETDATE())