CREATE TABLE [dbo].[StaffAttributes] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]            UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]                UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [LookupValue]              NVARCHAR (100)   NOT NULL,
    [IsActive]                 BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);

