use QuickBite;
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        -- Define your table columns here
        Id INT PRIMARY KEY
        , [Name] VARCHAR(100) NOT NULL
        , CreatedAt Datetime NOT NULL
        , UpdatedAt Datetime NULL        
    )

	INSERT INTO Categories Values (1, 'Lanches', GetDate(), NULL), (2, 'Bebidas',GetDate(), NULL), (3,'Acompanhamentos',GetDate(), NULL), (4, 'Sobremesas',GetDate(), NULL), (5,'Combos',GetDate(), NULL)
END


