CREATE TABLE [dbo].[Audit] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [TypeOfObject]  VARCHAR (150)    NULL,
    [AuditAction]   VARCHAR (50)     NULL,
    [ObjectCode]    VARCHAR (50)     NULL,
    [DateUpdated]   DATETIME         NULL,
    [ChangedBy]     VARCHAR (100)    NULL,
    [AuditText]     VARCHAR (MAX)    NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

