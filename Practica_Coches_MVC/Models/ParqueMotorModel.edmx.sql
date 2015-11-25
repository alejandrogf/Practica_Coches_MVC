
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/25/2015 12:14:31
-- Generated from EDMX file: C:\Users\Alumno\documents\visual studio 2015\Projects\Practica_Coches_MVC\Practica_Coches_MVC\Models\ParqueMotorModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ParqueMotor];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Vehiculo_Tipo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehiculo] DROP CONSTRAINT [FK_Vehiculo_Tipo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tipo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipo];
GO
IF OBJECT_ID(N'[dbo].[Vehiculo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehiculo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tipo'
CREATE TABLE [dbo].[Tipo] (
    [idTipo] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Descripcion] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Vehiculo'
CREATE TABLE [dbo].[Vehiculo] (
    [idVechiculo] int IDENTITY(1,1) NOT NULL,
    [Matricula] nvarchar(7)  NOT NULL,
    [Marca] nvarchar(50)  NOT NULL,
    [Modelo] nvarchar(50)  NOT NULL,
    [Coste] decimal(18,2)  NOT NULL,
    [Tipo] int  NOT NULL
);
GO

-- Creating table 'UsuarioSet'
CREATE TABLE [dbo].[UsuarioSet] (
    [idUsuario] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Apellidos] nvarchar(100)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [idRol] int  NOT NULL
);
GO

-- Creating table 'RolSet'
CREATE TABLE [dbo].[RolSet] (
    [idRol] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idTipo] in table 'Tipo'
ALTER TABLE [dbo].[Tipo]
ADD CONSTRAINT [PK_Tipo]
    PRIMARY KEY CLUSTERED ([idTipo] ASC);
GO

-- Creating primary key on [idVechiculo] in table 'Vehiculo'
ALTER TABLE [dbo].[Vehiculo]
ADD CONSTRAINT [PK_Vehiculo]
    PRIMARY KEY CLUSTERED ([idVechiculo] ASC);
GO

-- Creating primary key on [idUsuario] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [PK_UsuarioSet]
    PRIMARY KEY CLUSTERED ([idUsuario] ASC);
GO

-- Creating primary key on [idRol] in table 'RolSet'
ALTER TABLE [dbo].[RolSet]
ADD CONSTRAINT [PK_RolSet]
    PRIMARY KEY CLUSTERED ([idRol] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Tipo] in table 'Vehiculo'
ALTER TABLE [dbo].[Vehiculo]
ADD CONSTRAINT [FK_Vehiculo_Tipo]
    FOREIGN KEY ([Tipo])
    REFERENCES [dbo].[Tipo]
        ([idTipo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehiculo_Tipo'
CREATE INDEX [IX_FK_Vehiculo_Tipo]
ON [dbo].[Vehiculo]
    ([Tipo]);
GO

-- Creating foreign key on [idRol] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [FK_UsuarioRol]
    FOREIGN KEY ([idRol])
    REFERENCES [dbo].[RolSet]
        ([idRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioRol'
CREATE INDEX [IX_FK_UsuarioRol]
ON [dbo].[UsuarioSet]
    ([idRol]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------