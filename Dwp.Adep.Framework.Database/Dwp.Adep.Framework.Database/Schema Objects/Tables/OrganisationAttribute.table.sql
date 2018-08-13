CREATE TABLE [dbo].[OrganisationAttribute] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode]         UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [LookupValue]              NVARCHAR (100)   NOT NULL,
    [IsActive]                 BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);

