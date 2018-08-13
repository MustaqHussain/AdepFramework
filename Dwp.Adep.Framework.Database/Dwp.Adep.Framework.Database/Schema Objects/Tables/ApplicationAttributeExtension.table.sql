CREATE TABLE [dbo].[ApplicationAttributeExtension] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [IsStaffAdmin]             BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);

