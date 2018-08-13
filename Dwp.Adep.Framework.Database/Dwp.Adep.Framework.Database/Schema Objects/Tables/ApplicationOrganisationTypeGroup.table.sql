CREATE TABLE [dbo].[ApplicationOrganisationTypeGroup] (
    [Code]                               UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]                    UNIQUEIDENTIFIER NOT NULL,
    [OrganisationTypeGroupCode]          UNIQUEIDENTIFIER NOT NULL,
    [RootOrganisationForApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [IsActive]                           BIT              NOT NULL,
    [RowIdentifier]                      TIMESTAMP        NOT NULL
);

