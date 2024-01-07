use QuickBite;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN 
 Create TABLE ProductTypes {
    Id INT PRIMARY KEY IDENTITY,
        [Name] VARCHAR(50) NOT NULL,
    }

    INSERT INTO ProductTypes ([Name]) VALUES ('Ingredient')
 }

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
        Removable BIT NOT NULL,
        CategoryID INT FOREIGN KEY REFERENCES Categories(Id),
        ProductTypeID INT FOREIGN KEY REFERENCES ProductTypes(Id),
        CreatedAt Datetime NOT NULL,
        UpdatedAt Datetime NULL
    )
END


Select * from Products
