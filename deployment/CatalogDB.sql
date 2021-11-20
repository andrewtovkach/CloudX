IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [catalog_brand_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [catalog_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [catalog_type_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Baskets] (
    [Id] int NOT NULL IDENTITY,
    [BuyerId] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Baskets] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CatalogBrands] (
    [Id] int NOT NULL,
    [Brand] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CatalogBrands] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CatalogTypes] (
    [Id] int NOT NULL,
    [Type] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CatalogTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [BuyerId] nvarchar(max) NULL,
    [OrderDate] datetimeoffset NOT NULL,
    [ShipToAddress_Street] nvarchar(180) NULL,
    [ShipToAddress_City] nvarchar(100) NULL,
    [ShipToAddress_State] nvarchar(60) NULL,
    [ShipToAddress_Country] nvarchar(90) NULL,
    [ShipToAddress_ZipCode] nvarchar(18) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BasketItems] (
    [Id] int NOT NULL IDENTITY,
    [UnitPrice] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    [CatalogItemId] int NOT NULL,
    [BasketId] int NOT NULL,
    CONSTRAINT [PK_BasketItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BasketItems_Baskets_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [Baskets] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Catalog] (
    [Id] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [PictureUri] nvarchar(max) NULL,
    [CatalogTypeId] int NOT NULL,
    [CatalogBrandId] int NOT NULL,
    CONSTRAINT [PK_Catalog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Catalog_CatalogBrands_CatalogBrandId] FOREIGN KEY ([CatalogBrandId]) REFERENCES [CatalogBrands] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Catalog_CatalogTypes_CatalogTypeId] FOREIGN KEY ([CatalogTypeId]) REFERENCES [CatalogTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [ItemOrdered_CatalogItemId] int NULL,
    [ItemOrdered_ProductName] nvarchar(50) NULL,
    [ItemOrdered_PictureUri] nvarchar(max) NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [Units] int NOT NULL,
    [OrderId] int NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_BasketItems_BasketId] ON [BasketItems] ([BasketId]);
GO

CREATE INDEX [IX_Catalog_CatalogBrandId] ON [Catalog] ([CatalogBrandId]);
GO

CREATE INDEX [IX_Catalog_CatalogTypeId] ON [Catalog] ([CatalogTypeId]);
GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201202111507_InitialModel', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'BuyerId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Orders] ALTER COLUMN [BuyerId] nvarchar(256) NOT NULL;
ALTER TABLE [Orders] ADD DEFAULT N'' FOR [BuyerId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Baskets]') AND [c].[name] = N'BuyerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Baskets] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Baskets] ALTER COLUMN [BuyerId] nvarchar(256) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211026175614_FixBuyerId', N'6.0.0');
GO

COMMIT;
GO

