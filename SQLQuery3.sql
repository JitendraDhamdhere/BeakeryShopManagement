CREATE TABLE BillInfoTable (
    ID INT PRIMARY KEY,
    BillId VARCHAR(10),
    ProductName VARCHAR(100),
    Price DECIMAL(10, 2),
    Quantity INT,
    SubTotal DECIMAL(10, 2)
);
