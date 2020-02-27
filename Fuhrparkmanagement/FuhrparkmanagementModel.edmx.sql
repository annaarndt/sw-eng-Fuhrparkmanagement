
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/18/2020 10:03:59
-- Generated from EDMX file: C:\Users\I2CM Developer\Desktop\V10_2 Fuhrparkmanagement\Fuhrparkmanagement\FuhrparkmanagementModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FuhrparkmanagementDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_IstVomTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fahrzeug] DROP CONSTRAINT [FK_IstVomTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_HatHauptnutzer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mitarbeiter] DROP CONSTRAINT [FK_HatHauptnutzer];
GO
IF OBJECT_ID(N'[dbo].[FK_LegtBuchungAn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buchung] DROP CONSTRAINT [FK_LegtBuchungAn];
GO
IF OBJECT_ID(N'[dbo].[FK_BuchungMitFahrzeug]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buchung] DROP CONSTRAINT [FK_BuchungMitFahrzeug];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Fahrzeugtyp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fahrzeugtyp];
GO
IF OBJECT_ID(N'[dbo].[Fahrzeug]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fahrzeug];
GO
IF OBJECT_ID(N'[dbo].[Mitarbeiter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mitarbeiter];
GO
IF OBJECT_ID(N'[dbo].[Buchung]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buchung];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Fahrzeugtyp'
CREATE TABLE [dbo].[Fahrzeugtyp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Modell] nvarchar(max)  NOT NULL,
    [Leistung] int  NOT NULL,
    [Sitzplatzanzahl] int  NOT NULL
);
GO

-- Creating table 'Fahrzeug'
CREATE TABLE [dbo].[Fahrzeug] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Kennzeichen] nvarchar(max)  NOT NULL,
    [Farbe] nvarchar(max)  NOT NULL,
    [Kilometerstand] int  NOT NULL,
    [FahrzeugtypId] int  NOT NULL
);
GO

-- Creating table 'Mitarbeiter'
CREATE TABLE [dbo].[Mitarbeiter] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Personalnummer] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Führerschein] bit  NOT NULL,
    [Führerscheinnummer] nvarchar(max)  NULL,
    [IstAdmin] bit  NOT NULL,
    [Hauptnutzerfahrzeug_Id] int  NULL
);
GO

-- Creating table 'Buchung'
CREATE TABLE [dbo].[Buchung] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Zweck] nvarchar(max)  NOT NULL,
    [Start] datetime  NOT NULL,
    [Ende] datetime  NOT NULL,
    [Kilometerplan] int  NOT NULL,
    [MitarbeiterId] int  NOT NULL,
    [FahrzeugId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Fahrzeugtyp'
ALTER TABLE [dbo].[Fahrzeugtyp]
ADD CONSTRAINT [PK_Fahrzeugtyp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Fahrzeug'
ALTER TABLE [dbo].[Fahrzeug]
ADD CONSTRAINT [PK_Fahrzeug]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mitarbeiter'
ALTER TABLE [dbo].[Mitarbeiter]
ADD CONSTRAINT [PK_Mitarbeiter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Buchung'
ALTER TABLE [dbo].[Buchung]
ADD CONSTRAINT [PK_Buchung]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FahrzeugtypId] in table 'Fahrzeug'
ALTER TABLE [dbo].[Fahrzeug]
ADD CONSTRAINT [FK_IstVomTyp]
    FOREIGN KEY ([FahrzeugtypId])
    REFERENCES [dbo].[Fahrzeugtyp]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IstVomTyp'
CREATE INDEX [IX_FK_IstVomTyp]
ON [dbo].[Fahrzeug]
    ([FahrzeugtypId]);
GO

-- Creating foreign key on [Hauptnutzerfahrzeug_Id] in table 'Mitarbeiter'
ALTER TABLE [dbo].[Mitarbeiter]
ADD CONSTRAINT [FK_HatHauptnutzer]
    FOREIGN KEY ([Hauptnutzerfahrzeug_Id])
    REFERENCES [dbo].[Fahrzeug]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HatHauptnutzer'
CREATE INDEX [IX_FK_HatHauptnutzer]
ON [dbo].[Mitarbeiter]
    ([Hauptnutzerfahrzeug_Id]);
GO

-- Creating foreign key on [MitarbeiterId] in table 'Buchung'
ALTER TABLE [dbo].[Buchung]
ADD CONSTRAINT [FK_LegtBuchungAn]
    FOREIGN KEY ([MitarbeiterId])
    REFERENCES [dbo].[Mitarbeiter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LegtBuchungAn'
CREATE INDEX [IX_FK_LegtBuchungAn]
ON [dbo].[Buchung]
    ([MitarbeiterId]);
GO

-- Creating foreign key on [FahrzeugId] in table 'Buchung'
ALTER TABLE [dbo].[Buchung]
ADD CONSTRAINT [FK_BuchungMitFahrzeug]
    FOREIGN KEY ([FahrzeugId])
    REFERENCES [dbo].[Fahrzeug]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BuchungMitFahrzeug'
CREATE INDEX [IX_FK_BuchungMitFahrzeug]
ON [dbo].[Buchung]
    ([FahrzeugId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------