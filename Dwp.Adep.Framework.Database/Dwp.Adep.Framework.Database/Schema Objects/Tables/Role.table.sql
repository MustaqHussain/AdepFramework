CREATE TABLE [dbo].[Role] (
    [Code]            UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]   UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [RoleName]        NVARCHAR (50)    NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [RowIdentifier]   TIMESTAMP        NOT NULL
);

