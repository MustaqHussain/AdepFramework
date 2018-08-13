CREATE TABLE [dbo].[StaffOrganisation] (
    [Code]             UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]        UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode] UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]  UNIQUEIDENTIFIER NOT NULL,
    [IsDefault]        BIT              NOT NULL,
    [IsCurrent]        BIT              NOT NULL,
    [RowIdentifier]    TIMESTAMP        NOT NULL
);

