CREATE TABLE [dbo].[Staff] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [StaffNumber]   NVARCHAR (8)     NOT NULL,
    [LastName]      NVARCHAR (35)    NOT NULL,
    [FirstName]     NVARCHAR (35)    NOT NULL,
    [GradeCode]     UNIQUEIDENTIFIER NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

