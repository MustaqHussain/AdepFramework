CREATE TABLE [dbo].[ApplicationAttribute] (
    [Code]            UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [AttributeName]   NVARCHAR (50)    NOT NULL,
    [AttributeType]   NVARCHAR (50)    NOT NULL,
    [IsDataSecurity]  BIT              NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [IsRole]          BIT              NOT NULL,
    [RowIdentifier]   TIMESTAMP        NOT NULL
);

