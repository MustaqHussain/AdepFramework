CREATE TABLE [dbo].[Grade] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Grade]         NVARCHAR (10)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

