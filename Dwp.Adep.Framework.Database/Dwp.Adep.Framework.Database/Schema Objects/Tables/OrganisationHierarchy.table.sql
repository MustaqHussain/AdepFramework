CREATE TABLE [dbo].[OrganisationHierarchy] (
    [Code]                      UNIQUEIDENTIFIER NOT NULL,
    [AncestorOrganisationCode]  UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ImmediateParent]           BIT              NOT NULL,
    [HopsBetweenOrgAndAncestor] INT              NULL,
    [IsActive]                  BIT              NOT NULL,
    [RowIdentifier]             TIMESTAMP        NOT NULL
);

