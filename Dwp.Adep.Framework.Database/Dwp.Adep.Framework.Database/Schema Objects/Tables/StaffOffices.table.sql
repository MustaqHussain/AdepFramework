CREATE TABLE [dbo].[StaffOffices] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (70)    NOT NULL,
    [Address1]      NVARCHAR (35)    NOT NULL,
    [Address2]      NVARCHAR (35)    NOT NULL,
    [Address3]      NVARCHAR (35)    NULL,
    [Address4]      NVARCHAR (35)    NULL,
    [Address5]      NVARCHAR (35)    NULL,
    [Postcode]      NVARCHAR (10)    NULL,
    [Telephone]     NVARCHAR (20)    NULL,
    [OpeningTimes]  NVARCHAR (MAX)   NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

