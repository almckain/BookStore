1. GetAllBooks: Returns all the books from the BookTitles table along with data for the authors name.
CREATE PROCEDURE GetAllBooks
AS
BEGIN
SELECT
bt.BookID,
bt.Title,
a.Author AS AuthorName,
bt.Price,
bt.CoverImagePath
FROM
  BookTitles bt
JOIN
  Authors a on bt.AuthorID = a.AuthorID
ORDER BY
bt.Title;
END
GO
	

2. GetTopSellingBooks: Returns the four best selling books based on their total sales.
CREATE PROCEDURE GetTopSellingBooks
AS
BEGIN
  SELECT TOP 4
      bt.BookID,
      bt.Title,
      bt.Price,
      a.Author,
      bt.CoverImagePath,
      SUM(ol.Quantity) AS TotalSales
  FROM
      BookTitles bt
  JOIN
      OrderLines ol ON bt.BookID = ol.BookID
  JOIN
      Authors a ON bt.AuthorID = a.AuthorID
  GROUP BY
      bt.BookID, bt.Title, bt.Price, a.Author, bt.CoverImagePath
  ORDER BY
      TotalSales DESC;
END
	

3. GetAllGenres: Returns all genres from the Genre table.
CREATE PROCEDURE GetAllGenres
AS
BEGIN
SELECT
  GenreID,
  Name AS GenreName
FROM
  Genre
END
	

4. GetBooksByGenreID: Using the parameter GENREID, we return all the books that store that genre ID in its BookTitles GenreID table column.
CREATE PROCEDURE GetBooksByGenreID
  @GENREID INT
AS
BEGIN
SELECT
      bt.BookID,
      bt.Title,
      a.Author AS AuthorName,
      bt.Price,
      bt.CoverImagePath
  FROM
      BookTitles bt
  JOIN Authors a ON bt.AuthorID = a.AuthorID
  INNER JOIN Genre G ON G.GenreID = bt.GenreID
  WHERE
      bt.GenreID = @GENREID
  ORDER BY
      bt.Title;
END;
	

5. GetBookDetails: Using a BookID parameter we return all necessary information about a specific book for the customer.
CREATE PROCEDURE GetBookDetails
  @BookID INT
AS
BEGIN
SELECT
  B.BookID,
  B.Title,
  A.Author,
  B.ISBN,
  B.Price,
  B.CoverImagePath,
  B.GenreID,
  B.Publisher_ID AS PublisherID,
  BI.Available
FROM BookTitles B
INNER JOIN BookInventory BI ON BI.BookID = B.BookID
INNER JOIN Authors A ON A.AuthorId = B.AuthorID
WHERE B.BookID = @BookID
END
	

6. GetBooksByPublisherID: Using the PUBLISHERID parameter, we return all the books that are associated with that publisher id from the BookTitles Publisher_ID column.
CREATE PROCEDURE GetBooksByPublisherID
  @PUBLISHERID INT
AS
BEGIN
SELECT
      bt.BookID,
      bt.Title,
      a.Author AS AuthorName,
      bt.Price,
      bt.CoverImagePath
  FROM
      BookTitles bt
  JOIN Authors a ON bt.AuthorID = a.AuthorID
  INNER JOIN Publishers P ON P.Publisher_ID = bt.Publisher_ID
  WHERE
      bt.Publisher_ID = @PUBLISHERID
  ORDER BY
      bt.Title;
END;
	

7. GetLowStockBooks: Returns any books that have an available stock number that is equal to or less than two.
CREATE PROCEDURE GetLowStockBooks
AS
BEGIN
SELECT
  BT.BookID,
  BT.Title,
  P.Name,
  BI.Available,
  BI.OnPurchaseOrder,
  Bi.OnOrder,
  P.Publisher_ID
FROM BookTitles BT
INNER JOIN Publishers P ON P.Publisher_ID = BT.Publisher_ID
INNER JOIN BookInventory BI ON BI.BookID = BT.BookID
WHERE BI.Available<=2
GROUP BY BT.BookID, BT.Title, P.Name, BI.Available, BI.OnPurchaseOrder, BI.OnOrder, P.Publisher_ID
END
	







8. GetRecentPurchaseOrders: Returns the ten most recent stock refills for the admin to manage and track what books were recently restocked.
CREATE PROCEDURE GetRecentPurchaseOrders
AS
BEGIN
SELECT TOP 10
  b.Title AS Booktitle,
  p.Name AS PublisherName,
  pol.Quanity,
  po.OrderDate
FROM PurchaseOrderLines pol
JOIN PurchaseOrders po ON pol.PurchaseOrderID = po.PurchaseOrderID
JOIN BookTitles b ON pol.BookID = b.BookID
JOIN Publishers p ON b.Publisher_ID = p.Publisher_ID
ORDER BY po.OrderDate DESC
END
	

9. GetTopCustomers: Using TotalMoneySpent we return the top ten customers who have spent the most money on our book store.
CREATE PROCEDURE GetTopCustomers
AS
BEGIN
SELECT TOP 10
      C.Name,
      C.CustomerID,
      C.Email,
      COUNT(O.OrderID) AS OrderCount,
      ROUND(SUM(OL.Quantity * bt.Price), 2) AS TotalMoneySpent
  FROM
      Customer C
  INNER JOIN
      Orders O ON O.CustomerID = C.CustomerID
  INNER JOIN OrderLines OL ON OL.OrderID = O.OrderID
  INNER JOIN BookTitles BT ON BT.BookID = OL.BookID
  GROUP BY C.Name, c.CustomerID, C.Email, O.OrderID
  ORDER BY
      TotalMoneySpent DESC;
END;
	

10. GetMonthlyProfit: Returns the individual months profits made in the past six months of operation.
CREATE PROCEDURE GetMonthlyProfit
AS
BEGIN
  SELECT
      YEAR(O.OrderDate) AS Year,
      MONTH(O.OrderDate) AS Month,
      SUM(OL.Quantity * B.Price) AS MonthlyProfit
  FROM Orders O
  INNER JOIN OrderLines OL ON O.OrderID = OL.OrderID
  INNER JOIN BookTitles B ON OL.BookID = B.BookID
  WHERE O.OrderDate >= DATEADD(MONTH, -6, GETDATE())
  GROUP BY YEAR(O.OrderDate), MONTH(O.OrderDate)
  ORDER BY YEAR(O.OrderDate), MONTH(O.OrderDate);
END;
	

11. ReturnCustOrders: Returns a certain customers purchases from the website using the CustomerID parameter.
CREATE PROCEDURE ReturnCustOrders
@CustomerID INT
AS
BEGIN
  SELECT
      O.OrderID,
      SUM (OL.Quantity * BT.Price) AS OrderTotal,
      O.OrderDate
  FROM Orders O
  INNER JOIN OrderLines OL ON OL.OrderID = O.OrderID
  INNER JOIN BookTitles BT ON BT.BookID = OL.BookID
  WHERE O.CustomerID = @CustomerID
  GROUP BY O.OrderID, O.OrderDate
END
	

12. GetCustomerByEmail: Used for logging in we check to see if the Email parameter is found in the Customer table.
CREATE PROCEDURE GetCustomerByEmail
  @Email NVARCHAR(255)
AS
BEGIN
  SELECT
      c.CustomerID,
      c.Name,
      c.Email
  FROM
      Customer c
  WHERE Email = @Email;
END;
	

13. CreateNewAccount: Using the parameters Name and Email we create a new customer account and store it in the Customer table.
CREATE PROCEDURE CreateNewAccount
  @Name NVARCHAR(100),
  @Email NVARCHAR(100)
AS
BEGIN
INSERT INTO Customer (Name, Email)
VALUES (@Name, @Email);
END
	

14. GetGenreByID: Gets a single Genre based on the GenreID parameter.
CREATE PROCEDURE GetGenreByID
  @GenreID INT
AS
BEGIN
  SELECT
      g.GenreID,
      g.Name
  FROM Genre g
  WHERE GenreID = @GenreID;
END;
	

15. GetBiggestOrderId: Finds and returns the highest order ID.
CREATE PROCEDURE GetBiggestOrderId
AS
BEGIN
SELECT MAX(OrderID) AS max_orderid FROM Orders;
END
	

16. GetBiggestOrderLineId: Finds and returns the highest OrderLine ID.
CREATE PROCEDURE GetBiggestOrderLineId
AS
BEGIN
SELECT MAX(OrderLineID) AS max_orderlineid FROM OrderLines;
END
	

17. PlaceOrder: Using the returned order ID and the Customer Id parameter we place a new order into the Orders table.
CREATE PROCEDURE PlaceOrder
  @OrderId INT,
  @CustomerId INT
AS
BEGIN
INSERT INTO Orders (OrderID, CustomerID, OrderDate)
VALUES (@OrderId, @CustomerId, GETDATE())
END
	



18. CreateOrderLines: Using the OrderLineId, OrderId, Quantity, and BookId, parameters we generate new lines for each book purchased in an order and store it in the OrderLines table.
CREATE PROCEDURE CreateOrderLines
  @OrderLineId INT,
  @OrderId INT,
  @Quantity INT,
  @BookId INT
AS
BEGIN
INSERT INTO OrderLines (OrderLineID, OrderID, Quantity, BookID)
VALUES (@OrderLineId, @OrderId, @Quantity, @BookId)
END
	

19. DecrementBookInventory: Using the BookId and Quantity parameters we decrement the Available column in the BookInventory table to reflect a book is not longer available. 
CREATE PROCEDURE DecrementBookIventory
  @BookId INT,
  @Quantity INT
AS
BEGIN
UPDATE BookInventory
SET Available = Available -@Quantity
WHERE BookID = @BookId
END
	

20. GetBiggestPurchaseOrderID: Finds and returns the highest PurchaseOrder ID.
CREATE PROCEDURE GetBiggestPurchaseOrderID
AS
BEGIN
  SELECT MAX(PurchaseOrderID) AS max_orderid FROM PurchaseOrders;
END
	

21. GetBiggestPurchaseOrderLineID: Finds and returns the highest PurchaseOrderLine ID.
CREATE PROCEDURE GetBiggestPurchaseOrderLineID
AS
BEGIN
  SELECT MAX(PurchaseOrderLineId) AS max_orderlineid FROM PurchaseOrderLines;
END
	

22. PlaceOrderPublishers: Using the PurchaseOrderID and PublisherID we can place a new refill stock order and associate it with a publisher. The order is then stored in the PurchaseOrders table.
CREATE PROCEDURE PlaceOrderPublishers
  @PurchaseOrderID INT,
  @PublisherID INT
AS
BEGIN
  INSERT INTO PurchaseOrders(PurchaseOrderID, OrderDate, PublisherID)
  VALUES (@PurchaseOrderID, GETDATE(), @PublisherID)
END
	

23. CreateProcedureOrderLines: Creates a orderline for each book restocked in the PurchaseOrder and stores it in the PurchaseOrderLines table.
CREATE PROCEDURE CreateProcedureOrderLines
  @PurchaseOrderLineID INT,
  @PurchaseOrderID INT,
  @BookID INT,
  @Quantity INT
AS
BEGIN
INSERT INTO PurchaseOrderLines(PurchaseOrderLineID, PurchaseOrderID, BookID, Quanity)
VALUES(@PurchaseOrderLineID, @PurchaseOrderID, @BookID, @Quantity)
END
	



24. IncrementBookInventory: Increments a books available stock column in the BookInventory table using the BookID parameter and Quantity parameter.
CREATE PROCEDURE IncrementBookInventory
@BookID INT,
@Quantity INT
AS
BEGIN
UPDATE BookInventory
SET Available = Available +@Quantity
WHERE BookID = @BookID
END
	

25. GetPublisherByID: Returns a single publisher based off the PublisherID parameter.
CREATE PROCEDURE GetPublisherByID
  @PublisherID INT
AS
BEGIN
  SELECT
      p.Publisher_ID AS PublisherID,
      p.Name,
      p.PhoneNumber,
      p.StreetAddress
  FROM Publishers p
  WHERE Publisher_ID = @PublisherID;
END;
	

26. GetAllPublishers: Returns all the publishers found in the Publishers table.
CREATE PROCEDURE GetAllPublishers
AS
BEGIN
SELECT
p.Publisher_ID,
p.Name
FROM Publishers p
END