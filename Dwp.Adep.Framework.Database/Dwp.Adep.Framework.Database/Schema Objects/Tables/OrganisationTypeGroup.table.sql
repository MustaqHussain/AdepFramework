CREATE TABLE [dbo].[OrganisationTypeGroup] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (50)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

