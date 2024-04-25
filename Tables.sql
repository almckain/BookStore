1. Customer: Stores customer information. Email is unique and CustomerID is the primary key. 
CREATE TABLE Customer (
   CustomerID INT PRIMARY KEY,
   Name VARCHAR(255),
   Email VARCHAR(255) UNIQUE
);
	

2. Publisher: Stores information regarding publishers. The primary key is Publisher_ID, and has a unique key on Name.
CREATE TABLE Publishers (
   Publisher_ID INT PRIMARY KEY,
   Name VARCHAR(255) UNIQUE,
   PhoneNumber VARCHAR(20),
   StreetAddress VARCHAR(255)
);
	

3. Orders: Stores information for a single customer order. Primary key is OrderID, uses CustomerID to associate an order with a customer, and stores the date of the order. CustomerID has a foreign key with the Customer table CustomerID.
CREATE TABLE Orders (
   OrderID INT PRIMARY KEY,
   CustomerID INT,
   OrderDate DATE,
   FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
	

4. OrderLines: Stores book information regarding each order. OrderId may be a repeated value but different book IDs will be associated with the order. OrderLineID is the primary key, a foreign key on OrderID and Orders table OrderID, and a foreign key on BookID on BookTitles BookID.
CREATE TABLE OrderLines (
   OrderLineID INT PRIMARY KEY,
   OrderID INT,
   BookID INT,
   Quantity INT,
   FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
   FOREIGN KEY (BookID) REFERENCES BookTitle(BookID)
);
	

5. PurchaseOrders: Stores order stocks from publishers regarding the restock of certain book titles. The primary key is PurchaseOrderID, and a foreign key exists on PublisherId and Publishers table Publisher_ID.
CREATE TABLE PurchaseOrders (
   PurchaseOrderID INT PRIMARY KEY,
   OrderDate DATE,
   PublisherID INT,
   FOREIGN KEY (PublisherID) REFERENCES Publishers(Publisher_ID)
);
	

6. PurchaseOrderLines: Stores order information on a Publisher refill orders. Stores which books are associated with which order id. Primary key is PurchaseOrderLineID, and a foreign key exists on BookID and BookTitle tables BookID.
CREATE TABLE PurchaseOrderLines (
   PurchaseOrderLineID INT PRIMARY KEY,
   PurchaseOrderID INT,
   BookID INT,
   Quantity INT,
   FOREIGN KEY (PurchaseOrderID) REFERENCES PurchaseOrders(PurchaseOrderID),
   FOREIGN KEY (BookID) REFERENCES BookTitle(BookID)
);
	

7. Genre: Stores information regarding the book Genres. Primary key is GenreID and the Name is a unique key.
CREATE TABLE Genre (
   GenreID INT PRIMARY KEY,
   Name VARCHAR(255) UNIQUE
);
	

8. Author: Stores information regarding the book Authors. AuthorID is the primary key and Name is a unique key.
CREATE TABLE Author (
   AuthorID INT PRIMARY KEY,
   Name VARCHAR(255) UNIQUE
);
	

9. BookTitle: Stores all the information for a book. Some information is stored directly in the table and some used foreign keys and pulls information from other tables. The primary key is BookID, ISBN is unique, CoverImagePath is nullable, a foreign key exists on Publishe_ID with the Publishers table Publisher_ID, a foreign key exists on AuthorID on the Author tables AuthorID, and a foreign key exits on Genre with the Genre tables GenreID.
CREATE TABLE BookTitle (
   BookID INT PRIMARY KEY,
   Publisher_ID INT,
   Title VARCHAR(255),
   ISBN VARCHAR(17) UNIQUE,
   AuthorID INT,
   Genre INT,
   Price DECIMAL(10, 2),
   CoverImagePath VARCHAR(255) NULL,
   FOREIGN KEY (Publisher_ID) REFERENCES Publishers(Publisher_ID),
   FOREIGN KEY (AuthorID) REFERENCES Author(AuthorID),
   FOREIGN KEY (Genre) REFERENCES Genre(GenreID)
);
	

10. BookInventory: Keeps track of book inventory. There exists a foreign key BookID on the BookTitles table BookID.
CREATE TABLE BookInventory (
   BookID INT,
   OnOrder INT,
   OnPurchaseOrder INT,
   Available INT,
   FOREIGN KEY (BookID) REFERENCES BookTitle(BookID)
);