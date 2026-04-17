CREATE TABLE users
(

id INT PRIMARY KEY IDENTITY (1,1),
username VARCHAR(MAX) NULL,
password VARCHAR(MAX) NULL,
profile_image VARCHAR(MAX) NULL,
role VARCHAR(MAX) NULL,
status VARCHAR(MAX) NULL,
date_reg DATE NULL,

);

GO

INSERT INTO users (username, password, profile_image, role, status, date_reg) VALUES ('admin', 'admin123','', 'Admin', 'Active', '2026-04-03')

GO

SELECT * FROM users

CREATE TABLE products
(	
prod_id INT PRIMARY KEY IDENTITY (1,1),
prod_name VARCHAR(MAX) NULL,	
prod_type VARCHAR(MAX) NULL,
prod_stock INT NULL,
prod_price FLOAT NULL,
prod_status VARCHAR(MAX) NULL,
prod_image VARCHAR(MAX) NULL,
date_insert DATE NULL,
date_update DATE NULL,
date_delete DATE NULL,

)

SELECT * FROM products 
SELECT * FROM products WHERE date_delete IS NULL 
DELETE FROM products WHERE prod_id = 2





