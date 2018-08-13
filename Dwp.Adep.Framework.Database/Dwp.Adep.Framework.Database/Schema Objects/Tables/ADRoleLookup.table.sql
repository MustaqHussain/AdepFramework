CREATE TABLE [dbo].[ADRoleLookup] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [ADGroup]       NVARCHAR (50)    NOT NULL,
    [RoleCode]      UNIQUEIDENTIFIER NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

