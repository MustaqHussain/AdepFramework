CREATE TABLE [dbo].[UploadQueue] (
    [Code]             UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]  UNIQUEIDENTIFIER NULL,
    [TempTableName]    NVARCHAR (100)   NOT NULL,
    [StoredProcedure]  NVARCHAR (500)   NOT NULL,
    [ConnectionString] NVARCHAR (300)   NOT NULL,
    [UploadFileName]   NVARCHAR (500)   NOT NULL,
    [Status]           CHAR (1)         NOT NULL,
    [IsActive]         BIT              NOT NULL,
    [RowIdentifier]    TIMESTAMP        NOT NULL
);

