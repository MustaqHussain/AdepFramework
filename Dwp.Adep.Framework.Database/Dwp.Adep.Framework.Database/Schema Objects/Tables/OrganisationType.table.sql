CREATE TABLE [dbo].[OrganisationType] (
    [Code]                       UNIQUEIDENTIFIER NOT NULL,
    [Name]                       NVARCHAR (100)   NOT NULL,
    [LevelNumber]                INT              NOT NULL,
    [OrganisationTypeGroupCode]  UNIQUEIDENTIFIER NOT NULL,
    [ParentOrganisationTypeCode] UNIQUEIDENTIFIER NULL,
    [IsActive]                   BIT              NOT NULL,
    [RowIdentifier]              TIMESTAMP        NOT NULL
);

