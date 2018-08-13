CREATE TABLE [dbo].[Country] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Description]   NVARCHAR (60)    NOT NULL,
    [IsFrozenRate]  BIT              NOT NULL,
    [IsEC]          BIT              NOT NULL,
    [IsRA]          BIT              NOT NULL,
    [IsANZAC]       BIT              NOT NULL,
    [Nationality]   NVARCHAR (50)    NULL,
    [CountryCode]   NVARCHAR (4)     NOT NULL,
    [Notes]         NVARCHAR (MAX)   NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

