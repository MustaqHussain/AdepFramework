/*
Deployment script for AdepFramework
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO

:setvar DatabaseName "AdepFramework"


GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script. The database for which this script was built, AdepFramework, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]
GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[ApplicationAttributeExtension]...';


GO
CREATE TABLE [dbo].[ApplicationAttributeExtension] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [IsStaffAdmin]             BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_ApplicationAttributeExtension...';


GO
ALTER TABLE [dbo].[ApplicationAttributeExtension]
    ADD CONSTRAINT [PK_ApplicationAttributeExtension] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[OrganisationAttribute]...';


GO
CREATE TABLE [dbo].[OrganisationAttribute] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode]         UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [LookupValue]              NVARCHAR (100)   NOT NULL,
    [IsActive]                 BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_OrganisationAttribute...';


GO
ALTER TABLE [dbo].[OrganisationAttribute]
    ADD CONSTRAINT [PK_OrganisationAttribute] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[UploadErrorLog]...';


GO
CREATE TABLE [dbo].[UploadErrorLog] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [UploadCode]    UNIQUEIDENTIFIER NOT NULL,
    [ErrorMessage]  NVARCHAR (500)   NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_UploadErrorLog...';


GO
ALTER TABLE [dbo].[UploadErrorLog]
    ADD CONSTRAINT [PK_UploadErrorLog] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[UploadQueue]...';


GO
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


GO
PRINT N'Creating PK_UploadQueue...';


GO
ALTER TABLE [dbo].[UploadQueue]
    ADD CONSTRAINT [PK_UploadQueue] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO

/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[NonStandardHoliday]...';


GO
CREATE TABLE [dbo].[NonStandardHoliday] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Date]          DATETIME         NOT NULL,
    [Country]       NVARCHAR (100)   NULL,
    [Description]   NVARCHAR (30)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_NonStandardHoliday...';


GO
ALTER TABLE [dbo].[NonStandardHoliday]
    ADD CONSTRAINT [PK_NonStandardHoliday] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating FK_ApplicationAttributeExtension_ApplicationAttribute1...';


GO
ALTER TABLE [dbo].[ApplicationAttributeExtension] WITH NOCHECK
    ADD CONSTRAINT [FK_ApplicationAttributeExtension_ApplicationAttribute1] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_OrganisationAttribute_Application...';


GO
ALTER TABLE [dbo].[OrganisationAttribute] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationAttribute_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_OrganisationAttribute_ApplicationAttribute...';


GO
ALTER TABLE [dbo].[OrganisationAttribute] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationAttribute_ApplicationAttribute] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_OrganisationAttribute_Organisation...';


GO
ALTER TABLE [dbo].[OrganisationAttribute] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationAttribute_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_UploadErrorLog_UploadQueue...';


GO
ALTER TABLE [dbo].[UploadErrorLog] WITH NOCHECK
    ADD CONSTRAINT [FK_UploadErrorLog_UploadQueue] FOREIGN KEY ([UploadCode]) REFERENCES [dbo].[UploadQueue] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[UploadErrorLog] NOCHECK CONSTRAINT [FK_UploadErrorLog_UploadQueue];


GO
PRINT N'Creating FK_UploadQueue_Application...';


GO
ALTER TABLE [dbo].[UploadQueue] WITH NOCHECK
    ADD CONSTRAINT [FK_UploadQueue_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[UploadQueue] NOCHECK CONSTRAINT [FK_UploadQueue_Application];


GO


/****************************************************************************************************************
** Created by: sp_CreateDataFromTablesToInsertCommands                                                         **
*****************************************************************************************************************
** WARNING - IF A TABLE CONTAINS A DATATYPE OF TEXT OR NTEXT ONLY 8K OR 4K CHARS FROM THIS FIELD WILL BE       **
**           RETURNED RESPECTIVELY.                                                                            **
**                                                                                                             **
** BINARY, VARBINARY AND IMAGES ARE CURRENTLY NOT HANDLED AT ALL                                               **
** DATE EXECUTED: Jan 17 2013  5:24PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for ApplicationAttribute
****************************************/
PRINT 'Inserts for ApplicationAttribute'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table ApplicationAttribute'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM ApplicationAttribute WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('0E213A18-13E5-4920-9DCB-F35797F588F4', 'BC0D238F-18BE-427D-B53C-B68F8B27DCF7', 'FW-STAFF-ADMIN', 'Bool', 0, 1 ,1, NULL)
GO
-- 3: Insert any new items into the table from the table variable

INSERT INTO ApplicationAttribute 
SELECT 
Code,
ApplicationCode,
AttributeName,
AttributeType,
IsDataSecurity,
IsActive,
IsRole,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM ApplicationAttribute)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.ApplicationCode = tmp.ApplicationCode,
 LiveTable.AttributeName = tmp.AttributeName,
 LiveTable.AttributeType = tmp.AttributeType,
 LiveTable.IsDataSecurity = tmp.IsDataSecurity,
 LiveTable.IsActive = tmp.IsActive,
 LiveTable.IsRole = tmp.IsRole

FROM ApplicationAttribute LiveTable 
	INNER JOIN 
		#tempTable tmp 
			ON LiveTable.Code = tmp.Code

DROP TABLE #temptable

PRINT 'Finished updating static data table'

GO

/****************************************************************************************************************
** Created by: sp_CreateDataFromTablesToInsertCommands                                                         **
*****************************************************************************************************************
** WARNING - IF A TABLE CONTAINS A DATATYPE OF TEXT OR NTEXT ONLY 8K OR 4K CHARS FROM THIS FIELD WILL BE       **
**           RETURNED RESPECTIVELY.                                                                            **
**                                                                                                             **
** BINARY, VARBINARY AND IMAGES ARE CURRENTLY NOT HANDLED AT ALL                                               **
** DATE EXECUTED: Jan 17 2013  5:40PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for StaffAttributes
****************************************/
PRINT 'Inserts for StaffAttributes'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table StaffAttributes'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM StaffAttributes WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

INSERT INTO #tempTable ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('997DD322-7DF6-4BE1-821E-3D4E5FA10C41', '39879701-FB41-4331-A069-A57D93F171BE', 'EAEE7A91-754B-4499-958D-1306989F248A', 'BC0D238F-18BE-427D-B53C-B68F8B27DCF7', '0E213A18-13E5-4920-9DCB-F35797F588F4', 'Yes', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable

INSERT INTO StaffAttributes 
SELECT 
Code,
SecurityLabel,
StaffCode,
ApplicationCode,
ApplicationAttributeCode,
LookupValue,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM StaffAttributes)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.StaffCode = tmp.StaffCode,
 LiveTable.ApplicationCode = tmp.ApplicationCode,
 LiveTable.ApplicationAttributeCode = tmp.ApplicationAttributeCode,
 LiveTable.LookupValue = tmp.LookupValue,
 LiveTable.IsActive = tmp.IsActive

FROM StaffAttributes LiveTable 
	INNER JOIN 
		#tempTable tmp 
			ON LiveTable.Code = tmp.Code

DROP TABLE #temptable

PRINT 'Finished updating static data table'

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[ApplicationAttributeExtension] WITH CHECK CHECK CONSTRAINT [FK_ApplicationAttributeExtension_ApplicationAttribute1];

ALTER TABLE [dbo].[OrganisationAttribute] WITH CHECK CHECK CONSTRAINT [FK_OrganisationAttribute_Application];

ALTER TABLE [dbo].[OrganisationAttribute] WITH CHECK CHECK CONSTRAINT [FK_OrganisationAttribute_ApplicationAttribute];

ALTER TABLE [dbo].[OrganisationAttribute] WITH CHECK CHECK CONSTRAINT [FK_OrganisationAttribute_Organisation];


GO
