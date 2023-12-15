-- 001_InitialCreate.sql
CREATE TABLE IF NOT EXISTS Shops
(
    Code INTEGER PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Products
(
    Id       INTEGER PRIMARY KEY,
    Name     TEXT,
    ShopCode INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    Price    DECIMAL NOT NULL,
    FOREIGN KEY (ShopCode) REFERENCES Shops (Code)
);