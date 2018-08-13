CREATE TABLE [dbo].[Organisation] (
    [Code]                 UNIQUEIDENTIFIER NOT NULL,
    [ID]                   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (1000)  NOT NULL,
    [OrganisationTypeCode] UNIQUEIDENTIFIER NOT NULL,
    [HEO]                  NVARCHAR (35)    NULL,
    [DateDeleted]          DATETIME         NULL,
    [IsActive]             BIT              NOT NULL,
    [RowIdentifier]        TIMESTAMP        NOT NULL
);

