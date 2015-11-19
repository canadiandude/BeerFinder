-- Table Types
CREATE TABLE [dbo].[Types]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY, 
    [NomType] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(250) NULL,
)
-- Table Bieres
CREATE TABLE [dbo].[Bieres]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY, 
    [NomBiere] NVARCHAR(50) NULL, 
    [IdType] BIGINT NULL, 
    [Brasserie] NVARCHAR(50) NULL, 
    [VolumeAlcool] INT NULL, 
    [Etiquette] NVARCHAR(50) NULL, 
	CONSTRAINT fk_Types FOREIGN KEY (IdType)
	REFERENCES Types(Id)
)
-- Table Bars
CREATE TABLE [dbo].[Bars]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY, 
    [NomBar] NVARCHAR(50) NULL, 
    [Logo] NVARCHAR(50) NULL, 
    [Adresse] NVARCHAR(100) NULL, 
)
-- Table Selections
CREATE TABLE [dbo].[Selections]
(
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY, 
    [IdBar] BIGINT NULL, 
    [IdBiere] BIGINT NULL, 
    [Prix] MONEY NULL, 
)