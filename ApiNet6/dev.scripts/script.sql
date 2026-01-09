-- ============================================
-- Script: Crear base de datos Rockit
-- Fecha: 2026-01-08
-- ============================================

-- Crear la base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'rockit_db')
BEGIN
    CREATE DATABASE rockit_db;
END
GO

-- Usar la base de datos
USE rockit_db;
GO

-- ============================================
-- 1. Tabla: payment_type (catálogo de tipos de pago)
-- ============================================
CREATE TABLE payment_type (
    id INT PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
);

-- Insertar tipos de pago predefinidos
INSERT INTO payment_type (id, name) VALUES (1, 'Cash');
INSERT INTO payment_type (id, name) VALUES (2, 'Debit Card');
INSERT INTO payment_type (id, name) VALUES (3, 'Credit Card');

-- ============================================
-- 2. Tabla: products (catálogo de productos)
-- ============================================
CREATE TABLE products (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    price DECIMAL(18,2) NOT NULL
);

-- Productos iniciales (opcional)
INSERT INTO products (name, price) VALUES ('Coca Cola', 1200.50);
INSERT INTO products (name, price) VALUES ('Pan', 800.00);
INSERT INTO products (name, price) VALUES ('Leche', 950.00);

-- ============================================
-- 3. Tabla: movement (tickets/movimientos)
-- ============================================
CREATE TABLE movement (
    id INT PRIMARY KEY IDENTITY(1,1),
    ticket_number NVARCHAR(50) NOT NULL UNIQUE,
    date DATE NOT NULL,
    time TIME NOT NULL,
    cuit BIGINT NOT NULL,
    total DECIMAL(18,2) NOT NULL
);

-- ============================================
-- 4. Tabla: movement_items (items del movimiento)
-- ============================================
CREATE TABLE movement_items (
    id INT PRIMARY KEY IDENTITY(1,1),
    movement_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(18,2) NOT NULL, -- Precio al momento de la venta
    CONSTRAINT FK_movement_items_movement FOREIGN KEY (movement_id) REFERENCES movement(id) ON DELETE CASCADE,
    CONSTRAINT FK_movement_items_products FOREIGN KEY (product_id) REFERENCES products(id)
);

-- ============================================
-- 5. Tabla: payment_method (métodos de pago por movimiento)
-- ============================================
CREATE TABLE payment_method (
    id INT PRIMARY KEY IDENTITY(1,1),
    movement_id INT NOT NULL,
    payment_type_id INT NOT NULL,
    amount DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_payment_method_movement FOREIGN KEY (movement_id) REFERENCES movement(id) ON DELETE CASCADE,
    CONSTRAINT FK_payment_method_type FOREIGN KEY (payment_type_id) REFERENCES payment_type(id)
);

-- ============================================
-- Índices para mejorar performance (opcional)
-- ============================================
CREATE INDEX IX_movement_ticket_number ON movement(ticket_number);
CREATE INDEX IX_movement_items_movement_id ON movement_items(movement_id);
CREATE INDEX IX_payment_method_movement_id ON payment_method(movement_id);

GO

-- ============================================
-- Verificar que todo se creó correctamente
-- ============================================
SELECT 'Tablas creadas:' AS Status;
SELECT name FROM sys.tables ORDER BY name;
```