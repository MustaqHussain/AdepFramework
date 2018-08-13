CREATE TABLE [dbo].[Application] (
    [Code]                                 UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]                        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationName]                      NVARCHAR (50)    NOT NULL,
    [Location]                             NVARCHAR (300)   NOT NULL,
    [Description]                          NVARCHAR (1000)  NULL,
    [IsActive]                             BIT              NOT NULL,
    [IsSpecificOrganisationAccessRequired] BIT              NOT NULL,
    [RowIdentifier]                        TIMESTAMP        NOT NULL
);

