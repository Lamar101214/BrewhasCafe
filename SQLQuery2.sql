-- 1. Create the Users Table
-- This matches AdminAddUser.cs which expects an 'id' column to identify records for updates/deletes.

CREATE TABLE users
(
    id INT PRIMARY KEY IDENTITY (1,1),
    username VARCHAR(MAX) NULL,
    password VARCHAR(MAX) NULL,
    profile_image VARCHAR(MAX) NULL,
    role VARCHAR(MAX) NULL,
    status VARCHAR(MAX) NULL,
    date_reg DATE NULL
);

-- 2. Create the Products Table
-- NOTE: If you want to manually enter Product IDs in your WinForms app, 
-- we remove IDENTITY(1,1) so your code doesn't crash on INSERT.
CREATE TABLE products
(	
    prod_id INT PRIMARY KEY, -- Manual ID entry from adminAddProducts_id.Text
    prod_name VARCHAR(MAX) NULL,	
    prod_type VARCHAR(MAX) NULL,
    prod_stock INT NULL,
    prod_price FLOAT NULL,
    prod_status VARCHAR(MAX) NULL,
    prod_image VARCHAR(MAX) NULL,
    date_insert DATE NULL,
    date_update DATE NULL,
    date_delete DATE NULL
);

-- 3. Initial Admin Data
-- Useful for first-time login testing
INSERT INTO users (username, password, profile_image, role, status, date_reg) 
VALUES ('admin', 'admin123', '', 'Admin', 'Active', GETDATE());

-- 4. Essential Selection Queries for your DataGridViews
-- Use this for AdminAddUser displayAddUserData()
SELECT * FROM users;

-- Use this for AdminAddProducts displayData() to hide "deleted" items
SELECT * FROM products WHERE date_delete IS NULL;

CREATE TABLE orders (
    id INT PRIMARY KEY IDENTITY (1,1),
    customer_id INT,
    prod_id VARCHAR(MAX),
    prod_name VARCHAR(MAX),
    prod_type VARCHAR(MAX),
    qty INT,
    prod_price FLOAT,
    order_date DATE
);

CREATE TABLE customers (
    id INT PRIMARY KEY IDENTITY (1,1),
    customer_id INT,
    total_price FLOAT,
    amount FLOAT,
    change FLOAT,
    date DATE
);