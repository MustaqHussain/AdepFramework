CREATE TABLE [dbo].[UploadErrorLog] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [UploadCode]    UNIQUEIDENTIFIER NOT NULL,
    [ErrorMessage]  NVARCHAR (500)   NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

