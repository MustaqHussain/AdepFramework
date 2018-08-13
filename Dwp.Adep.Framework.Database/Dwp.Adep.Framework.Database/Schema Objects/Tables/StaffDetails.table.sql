CREATE TABLE [dbo].[StaffDetails] (
    [Code]            UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]       UNIQUEIDENTIFIER NOT NULL,
    [StaffOfficeCode] UNIQUEIDENTIFIER NULL,
    [Section]         NVARCHAR (35)    NULL,
    [Room]            NVARCHAR (35)    NULL,
    [DirectDialNo]    NVARCHAR (20)    NULL,
    [Extension]       NVARCHAR (35)    NULL,
    [Email]           NVARCHAR (100)   NULL,
    [RowIdentifier]   TIMESTAMP        NOT NULL
);

