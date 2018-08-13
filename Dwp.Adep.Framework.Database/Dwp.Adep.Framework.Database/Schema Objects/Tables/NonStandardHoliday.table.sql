CREATE TABLE [dbo].[NonStandardHoliday] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Date]          DATETIME         NOT NULL,
    [Country]       NVARCHAR (100)   NULL,
    [Description]   NVARCHAR (30)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

