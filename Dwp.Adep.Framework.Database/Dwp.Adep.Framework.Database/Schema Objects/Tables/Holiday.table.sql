CREATE TABLE [dbo].[Holiday] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Date]          DATETIME         NOT NULL,
    [IsNational]    BIT              NOT NULL,
    [Description]   NVARCHAR (30)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

