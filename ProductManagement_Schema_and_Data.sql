
-- Create the database
CREATE DATABASE ProductManagement;
GO

USE ProductManagement;
GO

-- Create the ProductTypes table
CREATE TABLE ProductTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    EntryDate DATETIME NOT NULL
);
GO

-- Create the Products table
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    EntryDate DATETIME NOT NULL
);
GO

-- Create the Product_ProductType table (Many-to-Many relationship)
CREATE TABLE Product_ProductType (
    ProductId INT,
    ProductTypeId INT,
    PRIMARY KEY (ProductId, ProductTypeId),
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductTypeId) REFERENCES ProductTypes(Id) ON DELETE CASCADE
);
GO

-- Insert sample data into ProductTypes table
INSERT INTO ProductTypes (Name, EntryDate) VALUES
('Electronics', GETDATE()),
('Clothing', GETDATE()),
('Groceries', GETDATE()),
('Furniture', GETDATE()),
('Books', GETDATE());
GO

-- Insert sample data into Products table
INSERT INTO Products (Name, Price, EntryDate) VALUES
('Laptop', 1000.00, '2023-06-01'),
('T-Shirt', 20.00, '2023-06-02'),
('Apple', 2.00, '2023-06-03'),
('Sofa', 300.00, '2023-06-04'),
('Novel', 15.00, '2023-06-05');
GO

-- Insert data into Product_ProductType table
INSERT INTO Product_ProductType (ProductId, ProductTypeId) VALUES
(1, 1), -- Laptop belongs to Electronics
(2, 2), -- T-Shirt belongs to Clothing
(3, 3), -- Apple belongs to Groceries
(4, 4), -- Sofa belongs to Furniture
(5, 5); -- Novel belongs to Books
GO
