/*
Deployment script for AdepFramework
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "AdepFramework"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.ADEPDEV\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.ADEPDEV\MSSQL\DATA\"

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
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [AdepTemplate], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB)
    LOG ON (NAME = [AdepTemplate_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_1.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE Latin1_General_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]
GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


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
PRINT N'Creating [dbo].[ADRoleLookup]...';


GO
CREATE TABLE [dbo].[ADRoleLookup] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [ADGroup]       NVARCHAR (50)    NOT NULL,
    [RoleCode]      UNIQUEIDENTIFIER NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_ADRoleLookup...';


GO
ALTER TABLE [dbo].[ADRoleLookup]
    ADD CONSTRAINT [PK_ADRoleLookup] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Application]...';


GO
CREATE TABLE [dbo].[Application] (
    [Code]                                 UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]                        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationName]                      NVARCHAR (50)    NOT NULL,
    [Location]                             NVARCHAR (300)   NOT NULL,
    [Description]                          NVARCHAR (1000)  NULL,
    [IsActive]                             BIT              NOT NULL,
    [IsSpecificOrganisationAccessRequired] BIT              NOT NULL,
    [RowIdentifier]                        TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Application...';


GO
ALTER TABLE [dbo].[Application]
    ADD CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[ApplicationAttribute]...';


GO
CREATE TABLE [dbo].[ApplicationAttribute] (
    [Code]            UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [AttributeName]   NVARCHAR (50)    NOT NULL,
    [AttributeType]   NVARCHAR (50)    NOT NULL,
    [IsDataSecurity]  BIT              NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [IsRole]          BIT              NOT NULL,
    [RowIdentifier]   TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_ApplicationAttribute...';


GO
ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [PK_ApplicationAttribute] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[ApplicationOrganisationTypeGroup]...';


GO
CREATE TABLE [dbo].[ApplicationOrganisationTypeGroup] (
    [Code]                               UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]                    UNIQUEIDENTIFIER NOT NULL,
    [OrganisationTypeGroupCode]          UNIQUEIDENTIFIER NOT NULL,
    [RootOrganisationForApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [IsActive]                           BIT              NOT NULL,
    [RowIdentifier]                      TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_ApplicationOrganisationTypeGroup...';


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup]
    ADD CONSTRAINT [PK_ApplicationOrganisationTypeGroup] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Audit]...';


GO
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


GO
PRINT N'Creating PK_Audit...';


GO
ALTER TABLE [dbo].[Audit]
    ADD CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Category]...';


GO
CREATE TABLE [dbo].[Category] (
    [CategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (64) NOT NULL
);


GO
PRINT N'Creating PK_Categories...';


GO
ALTER TABLE [dbo].[Category]
    ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[CategoryLog]...';


GO
CREATE TABLE [dbo].[CategoryLog] (
    [CategoryLogID] INT IDENTITY (1, 1) NOT NULL,
    [CategoryID]    INT NOT NULL,
    [LogID]         INT NOT NULL
);


GO
PRINT N'Creating PK_CategoryLog...';


GO
ALTER TABLE [dbo].[CategoryLog]
    ADD CONSTRAINT [PK_CategoryLog] PRIMARY KEY CLUSTERED ([CategoryLogID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Country]...';


GO
CREATE TABLE [dbo].[Country] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Description]   NVARCHAR (60)    NOT NULL,
    [IsFrozenRate]  BIT              NOT NULL,
    [IsEC]          BIT              NOT NULL,
    [IsRA]          BIT              NOT NULL,
    [IsANZAC]       BIT              NOT NULL,
    [Nationality]   NVARCHAR (50)    NULL,
    [CountryCode]   NVARCHAR (4)     NOT NULL,
    [Notes]         NVARCHAR (MAX)   NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Country...';


GO
ALTER TABLE [dbo].[Country]
    ADD CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating CK_Country_Description_Unique...';


GO
ALTER TABLE [dbo].[Country]
    ADD CONSTRAINT [CK_Country_Description_Unique] UNIQUE NONCLUSTERED ([Description] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Grade]...';


GO
CREATE TABLE [dbo].[Grade] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Grade]         NVARCHAR (10)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Grade...';


GO
ALTER TABLE [dbo].[Grade]
    ADD CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating UK_Grade_Grade...';


GO
ALTER TABLE [dbo].[Grade]
    ADD CONSTRAINT [UK_Grade_Grade] UNIQUE NONCLUSTERED ([Grade] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Holiday]...';


GO
CREATE TABLE [dbo].[Holiday] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel] UNIQUEIDENTIFIER NOT NULL,
    [Date]          DATETIME         NOT NULL,
    [IsNational]    BIT              NOT NULL,
    [Description]   NVARCHAR (30)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Holiday...';


GO
ALTER TABLE [dbo].[Holiday]
    ADD CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating CK_Holiday_Date_Unique...';


GO
ALTER TABLE [dbo].[Holiday]
    ADD CONSTRAINT [CK_Holiday_Date_Unique] UNIQUE NONCLUSTERED ([Date] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Log]...';


GO
CREATE TABLE [dbo].[Log] (
    [LogID]            INT             IDENTITY (1, 1) NOT NULL,
    [EventID]          INT             NULL,
    [Priority]         INT             NOT NULL,
    [Severity]         NVARCHAR (32)   NOT NULL,
    [Title]            NVARCHAR (256)  NOT NULL,
    [Timestamp]        DATETIME        NOT NULL,
    [MachineName]      NVARCHAR (32)   NOT NULL,
    [AppDomainName]    NVARCHAR (512)  NOT NULL,
    [ProcessID]        NVARCHAR (256)  NOT NULL,
    [ProcessName]      NVARCHAR (512)  NOT NULL,
    [ThreadName]       NVARCHAR (512)  NULL,
    [Win32ThreadId]    NVARCHAR (128)  NULL,
    [Message]          NVARCHAR (1500) NULL,
    [FormattedMessage] NTEXT           NULL
);


GO
PRINT N'Creating PK_Log...';


GO
ALTER TABLE [dbo].[Log]
    ADD CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Organisation]...';


GO
CREATE TABLE [dbo].[Organisation] (
    [Code]                 UNIQUEIDENTIFIER NOT NULL,
    [ID]                   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (1000)  NOT NULL,
    [OrganisationTypeCode] UNIQUEIDENTIFIER NOT NULL,
    [HEO]                  NVARCHAR (35)    NULL,
    [DateDeleted]          DATETIME         NULL,
    [IsActive]             BIT              NOT NULL,
    [RowIdentifier]        TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Organisation...';


GO
ALTER TABLE [dbo].[Organisation]
    ADD CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[OrganisationHierarchy]...';


GO
CREATE TABLE [dbo].[OrganisationHierarchy] (
    [Code]                      UNIQUEIDENTIFIER NOT NULL,
    [AncestorOrganisationCode]  UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ImmediateParent]           BIT              NOT NULL,
    [HopsBetweenOrgAndAncestor] INT              NULL,
    [IsActive]                  BIT              NOT NULL,
    [RowIdentifier]             TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_OrganisationHierarchy...';


GO
ALTER TABLE [dbo].[OrganisationHierarchy]
    ADD CONSTRAINT [PK_OrganisationHierarchy] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[OrganisationType]...';


GO
CREATE TABLE [dbo].[OrganisationType] (
    [Code]                       UNIQUEIDENTIFIER NOT NULL,
    [Name]                       NVARCHAR (100)   NOT NULL,
    [LevelNumber]                INT              NOT NULL,
    [OrganisationTypeGroupCode]  UNIQUEIDENTIFIER NOT NULL,
    [ParentOrganisationTypeCode] UNIQUEIDENTIFIER NULL,
    [IsActive]                   BIT              NOT NULL,
    [RowIdentifier]              TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_OrganisationType...';


GO
ALTER TABLE [dbo].[OrganisationType]
    ADD CONSTRAINT [PK_OrganisationType] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[OrganisationTypeGroup]...';


GO
CREATE TABLE [dbo].[OrganisationTypeGroup] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (50)    NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_OrganisationTypeGroup...';


GO
ALTER TABLE [dbo].[OrganisationTypeGroup]
    ADD CONSTRAINT [PK_OrganisationTypeGroup] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [Code]            UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]   UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode] UNIQUEIDENTIFIER NOT NULL,
    [RoleName]        NVARCHAR (50)    NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [RowIdentifier]   TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_Role...';


GO
ALTER TABLE [dbo].[Role]
    ADD CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Staff]...';


GO
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


GO
PRINT N'Creating PK_Staff...';


GO
ALTER TABLE [dbo].[Staff]
    ADD CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating UK_Staff_StaffNumber...';


GO
ALTER TABLE [dbo].[Staff]
    ADD CONSTRAINT [UK_Staff_StaffNumber] UNIQUE NONCLUSTERED ([StaffNumber] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[StaffAttributes]...';


GO
CREATE TABLE [dbo].[StaffAttributes] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [SecurityLabel]            UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]                UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]          UNIQUEIDENTIFIER NOT NULL,
    [ApplicationAttributeCode] UNIQUEIDENTIFIER NOT NULL,
    [LookupValue]              NVARCHAR (100)   NOT NULL,
    [IsActive]                 BIT              NOT NULL,
    [RowIdentifier]            TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_StaffAttributes...';


GO
ALTER TABLE [dbo].[StaffAttributes]
    ADD CONSTRAINT [PK_StaffAttributes] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[StaffDetails]...';


GO
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


GO
PRINT N'Creating PK_StaffDetails...';


GO
ALTER TABLE [dbo].[StaffDetails]
    ADD CONSTRAINT [PK_StaffDetails] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[StaffOffices]...';


GO
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


GO
PRINT N'Creating PK_StaffOffices...';


GO
ALTER TABLE [dbo].[StaffOffices]
    ADD CONSTRAINT [PK_StaffOffices] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[StaffOrganisation]...';


GO
CREATE TABLE [dbo].[StaffOrganisation] (
    [Code]             UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]        UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode] UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]  UNIQUEIDENTIFIER NOT NULL,
    [IsDefault]        BIT              NOT NULL,
    [IsCurrent]        BIT              NOT NULL,
    [RowIdentifier]    TIMESTAMP        NOT NULL
);


GO
PRINT N'Creating PK_StaffOrganisation...';


GO
ALTER TABLE [dbo].[StaffOrganisation]
    ADD CONSTRAINT [PK_StaffOrganisation] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating DF_ApplicationAttribute_IsDataSecurityFlag...';


GO
ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [DF_ApplicationAttribute_IsDataSecurityFlag] DEFAULT ((0)) FOR [IsDataSecurity];


GO
PRINT N'Creating DF_ApplicationAttribute_IsRole...';


GO
ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [DF_ApplicationAttribute_IsRole] DEFAULT ((0)) FOR [IsRole];


GO
PRINT N'Creating FK_ADRoleLookup_Organisation_Security...';


GO
ALTER TABLE [dbo].[ADRoleLookup] WITH NOCHECK
    ADD CONSTRAINT [FK_ADRoleLookup_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ADRoleLookup] NOCHECK CONSTRAINT [FK_ADRoleLookup_Organisation_Security];


GO
PRINT N'Creating FK_ADRoleLookup_Role...';


GO
ALTER TABLE [dbo].[ADRoleLookup] WITH NOCHECK
    ADD CONSTRAINT [FK_ADRoleLookup_Role] FOREIGN KEY ([RoleCode]) REFERENCES [dbo].[Role] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ADRoleLookup] NOCHECK CONSTRAINT [FK_ADRoleLookup_Role];


GO
PRINT N'Creating FK_Application_Organisation_Security...';


GO
ALTER TABLE [dbo].[Application] WITH NOCHECK
    ADD CONSTRAINT [FK_Application_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Application] NOCHECK CONSTRAINT [FK_Application_Organisation_Security];


GO
PRINT N'Creating FK_ApplicationAttribute_Application...';


GO
ALTER TABLE [dbo].[ApplicationAttribute] WITH NOCHECK
    ADD CONSTRAINT [FK_ApplicationAttribute_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ApplicationAttribute] NOCHECK CONSTRAINT [FK_ApplicationAttribute_Application];


GO
PRINT N'Creating FK_ApplicationOrganisationTypeGroup_Application...';


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] NOCHECK CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Application];


GO
PRINT N'Creating FK_ApplicationOrganisationTypeGroup_Organisation...';


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Organisation] FOREIGN KEY ([RootOrganisationForApplicationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] NOCHECK CONSTRAINT [FK_ApplicationOrganisationTypeGroup_Organisation];


GO
PRINT N'Creating FK_ApplicationOrganisationTypeGroup_OrganisationTypeGroup...';


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] WITH NOCHECK
    ADD CONSTRAINT [FK_ApplicationOrganisationTypeGroup_OrganisationTypeGroup] FOREIGN KEY ([OrganisationTypeGroupCode]) REFERENCES [dbo].[OrganisationTypeGroup] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ApplicationOrganisationTypeGroup] NOCHECK CONSTRAINT [FK_ApplicationOrganisationTypeGroup_OrganisationTypeGroup];


GO
PRINT N'Creating FK_CategoryLog_Category...';


GO
ALTER TABLE [dbo].[CategoryLog] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoryLog_Category] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Category] ([CategoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_CategoryLog_Log...';


GO
ALTER TABLE [dbo].[CategoryLog] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoryLog_Log] FOREIGN KEY ([LogID]) REFERENCES [dbo].[Log] ([LogID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Country_Organisation_Security...';


GO
ALTER TABLE [dbo].[Country] WITH NOCHECK
    ADD CONSTRAINT [FK_Country_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Grade_Organisation...';


GO
ALTER TABLE [dbo].[Grade] WITH NOCHECK
    ADD CONSTRAINT [FK_Grade_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Grade] NOCHECK CONSTRAINT [FK_Grade_Organisation];


GO
PRINT N'Creating FK_Holiday_Organisation_Security...';


GO
ALTER TABLE [dbo].[Holiday] WITH NOCHECK
    ADD CONSTRAINT [FK_Holiday_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Organisation_OrganisationType...';


GO
ALTER TABLE [dbo].[Organisation] WITH NOCHECK
    ADD CONSTRAINT [FK_Organisation_OrganisationType] FOREIGN KEY ([OrganisationTypeCode]) REFERENCES [dbo].[OrganisationType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Organisation] NOCHECK CONSTRAINT [FK_Organisation_OrganisationType];


GO
PRINT N'Creating FK_OrganisationHierarchy_Organisation...';


GO
ALTER TABLE [dbo].[OrganisationHierarchy] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationHierarchy_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[OrganisationHierarchy] NOCHECK CONSTRAINT [FK_OrganisationHierarchy_Organisation];


GO
PRINT N'Creating FK_OrganisationHierarchyAncestor_Organisation...';


GO
ALTER TABLE [dbo].[OrganisationHierarchy] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationHierarchyAncestor_Organisation] FOREIGN KEY ([AncestorOrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[OrganisationHierarchy] NOCHECK CONSTRAINT [FK_OrganisationHierarchyAncestor_Organisation];


GO
PRINT N'Creating FK_OrganisationType_OrganisationTypeGroup...';


GO
ALTER TABLE [dbo].[OrganisationType] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationType_OrganisationTypeGroup] FOREIGN KEY ([OrganisationTypeGroupCode]) REFERENCES [dbo].[OrganisationTypeGroup] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[OrganisationType] NOCHECK CONSTRAINT [FK_OrganisationType_OrganisationTypeGroup];


GO
PRINT N'Creating FK_OrganisationType_ParentOrganisationType...';


GO
ALTER TABLE [dbo].[OrganisationType] WITH NOCHECK
    ADD CONSTRAINT [FK_OrganisationType_ParentOrganisationType] FOREIGN KEY ([ParentOrganisationTypeCode]) REFERENCES [dbo].[OrganisationType] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[OrganisationType] NOCHECK CONSTRAINT [FK_OrganisationType_ParentOrganisationType];


GO
PRINT N'Creating FK_Role_Application...';


GO
ALTER TABLE [dbo].[Role] WITH NOCHECK
    ADD CONSTRAINT [FK_Role_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_Application];


GO
PRINT N'Creating FK_Role_Organisation...';


GO
ALTER TABLE [dbo].[Role] WITH NOCHECK
    ADD CONSTRAINT [FK_Role_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_Organisation];


GO
PRINT N'Creating FK_Role_Organisation_Security...';


GO
ALTER TABLE [dbo].[Role] WITH NOCHECK
    ADD CONSTRAINT [FK_Role_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_Organisation_Security];


GO
PRINT N'Creating FK_Staff_Grade...';


GO
ALTER TABLE [dbo].[Staff] WITH NOCHECK
    ADD CONSTRAINT [FK_Staff_Grade] FOREIGN KEY ([GradeCode]) REFERENCES [dbo].[Grade] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Staff] NOCHECK CONSTRAINT [FK_Staff_Grade];


GO
PRINT N'Creating FK_Staff_Organisation...';


GO
ALTER TABLE [dbo].[Staff] WITH NOCHECK
    ADD CONSTRAINT [FK_Staff_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Staff] NOCHECK CONSTRAINT [FK_Staff_Organisation];


GO
PRINT N'Creating FK_StaffAttributes_Application...';


GO
ALTER TABLE [dbo].[StaffAttributes] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffAttributes_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_Application];


GO
PRINT N'Creating FK_StaffAttributes_ApplicationAttribute...';


GO
ALTER TABLE [dbo].[StaffAttributes] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffAttributes_ApplicationAttribute] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_ApplicationAttribute];


GO
PRINT N'Creating FK_StaffAttributes_Organisation_Security...';


GO
ALTER TABLE [dbo].[StaffAttributes] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffAttributes_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_Organisation_Security];


GO
PRINT N'Creating FK_StaffAttributes_Staff...';


GO
ALTER TABLE [dbo].[StaffAttributes] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffAttributes_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_Staff];


GO
PRINT N'Creating FK_StaffDetails_Staff...';


GO
ALTER TABLE [dbo].[StaffDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffDetails_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_StaffOrganisation_Application...';


GO
ALTER TABLE [dbo].[StaffOrganisation] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffOrganisation_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffOrganisation] NOCHECK CONSTRAINT [FK_StaffOrganisation_Application];


GO
PRINT N'Creating FK_StaffOrganisation_Organisation...';


GO
ALTER TABLE [dbo].[StaffOrganisation] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffOrganisation_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffOrganisation] NOCHECK CONSTRAINT [FK_StaffOrganisation_Organisation];


GO
PRINT N'Creating FK_StaffOrganisation_Staff...';


GO
ALTER TABLE [dbo].[StaffOrganisation] WITH NOCHECK
    ADD CONSTRAINT [FK_StaffOrganisation_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffOrganisation] NOCHECK CONSTRAINT [FK_StaffOrganisation_Staff];


GO
PRINT N'Creating [dbo].[ClearLogs]...';


GO

CREATE PROCEDURE [dbo].[ClearLogs]
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CategoryLog
	DELETE FROM [Log]
    DELETE FROM Category
END
GO
PRINT N'Creating [dbo].[InsertCategoryLog]...';


GO

CREATE PROCEDURE [dbo].[InsertCategoryLog]
	@CategoryID INT,
	@LogID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CatLogID INT
	SELECT @CatLogID FROM CategoryLog WHERE CategoryID=@CategoryID and LogID = @LogID
	IF @CatLogID IS NULL
	BEGIN
		INSERT INTO CategoryLog (CategoryID, LogID) VALUES(@CategoryID, @LogID)
		RETURN @@IDENTITY
	END
	ELSE RETURN @CatLogID
END
GO
PRINT N'Creating [dbo].[WriteLog]...';


GO





/****** Object:  Stored Procedure dbo.WriteLog    Script Date: 10/1/2004 3:16:36 PM ******/

CREATE PROCEDURE [dbo].[WriteLog]
(
	@EventID int, 
	@Priority int, 
	@Severity nvarchar(32), 
	@Title nvarchar(256), 
	@Timestamp datetime,
	@MachineName nvarchar(32), 
	@AppDomainName nvarchar(512),
	@ProcessID nvarchar(256),
	@ProcessName nvarchar(512),
	@ThreadName nvarchar(512),
	@Win32ThreadId nvarchar(128),
	@Message nvarchar(1500),
	@FormattedMessage ntext,
	@LogId int OUTPUT
)
AS 

	INSERT INTO [Log] (
		EventID,
		Priority,
		Severity,
		Title,
		[Timestamp],
		MachineName,
		AppDomainName,
		ProcessID,
		ProcessName,
		ThreadName,
		Win32ThreadId,
		Message,
		FormattedMessage
	)
	VALUES (
		@EventID, 
		@Priority, 
		@Severity, 
		@Title, 
		@Timestamp,
		@MachineName, 
		@AppDomainName,
		@ProcessID,
		@ProcessName,
		@ThreadName,
		@Win32ThreadId,
		@Message,
		@FormattedMessage)

	SET @LogID = @@IDENTITY
	RETURN @LogID
GO
PRINT N'Creating [dbo].[AddCategory]...';


GO



CREATE PROCEDURE [dbo].[AddCategory]
	-- Add the parameters for the function here
	@CategoryName nvarchar(64),
	@LogID int
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @CatID INT
	SELECT @CatID = CategoryID FROM Category WHERE CategoryName = @CategoryName
	IF @CatID IS NULL
	BEGIN
		INSERT INTO Category (CategoryName) VALUES(@CategoryName)
		SELECT @CatID = @@IDENTITY
	END

	EXEC InsertCategoryLog @CatID, @LogID 

	RETURN @CatID
END
GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/****************************************************************************************************************
** Created by: sp_CreateDataFromTablesToInsertCommands                                                         **
*****************************************************************************************************************
** WARNING - IF A TABLE CONTAINS A DATATYPE OF TEXT OR NTEXT ONLY 8K OR 4K CHARS FROM THIS FIELD WILL BE       **
**           RETURNED RESPECTIVELY.                                                                            **
**                                                                                                             **
** BINARY, VARBINARY AND IMAGES ARE CURRENTLY NOT HANDLED AT ALL                                               **
** DATE EXECUTED: Jan 17 2013  5:32PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for OrganisationTypeGroup
****************************************/
PRINT 'Inserts for OrganisationTypeGroup'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table OrganisationTypeGroup'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM OrganisationTypeGroup WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.
INSERT INTO #tempTable ([Code], [Name], [IsActive], [RowIdentifier]) VALUES ('B1080D88-36EE-4EE7-B2FB-7D96851C7F9D', 'BCAS', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO OrganisationTypeGroup 
SELECT 
Code,
Name,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM OrganisationTypeGroup)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.Name = tmp.Name,
 LiveTable.IsActive = tmp.IsActive

FROM OrganisationTypeGroup LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:31PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for OrganisationType
****************************************/
PRINT 'Inserts for OrganisationType'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table OrganisationType'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM OrganisationType WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.
INSERT INTO #tempTable ([Code], [Name], [LevelNumber], [OrganisationTypeGroupCode], [ParentOrganisationTypeCode], [IsActive], [RowIdentifier]) VALUES ('F64F3CF9-C946-4182-B642-9392F805D466', 'BCASRoot', 1, 'B1080D88-36EE-4EE7-B2FB-7D96851C7F9D', NULL, 1, NULL)
INSERT INTO #tempTable ([Code], [Name], [LevelNumber], [OrganisationTypeGroupCode], [ParentOrganisationTypeCode], [IsActive], [RowIdentifier]) VALUES ('4A772D26-8DFC-4605-B9A5-FB3A7ADC19F3', 'BCASTeam', 2, 'B1080D88-36EE-4EE7-B2FB-7D96851C7F9D', 'F64F3CF9-C946-4182-B642-9392F805D466', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO OrganisationType 
SELECT 
Code,
Name,
LevelNumber,
OrganisationTypeGroupCode,
ParentOrganisationTypeCode,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM OrganisationType)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.Name = tmp.Name,
 LiveTable.LevelNumber = tmp.LevelNumber,
 LiveTable.OrganisationTypeGroupCode = tmp.OrganisationTypeGroupCode,
 LiveTable.ParentOrganisationTypeCode = tmp.ParentOrganisationTypeCode,
 LiveTable.IsActive = tmp.IsActive

FROM OrganisationType LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:27PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for Organisation
****************************************/
PRINT 'Inserts for Organisation'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table Organisation'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM Organisation WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

SET IDENTITY_INSERT #tempTable ON

INSERT INTO #tempTable ([Code], [ID], [Name], [OrganisationTypeCode], [HEO], [DateDeleted], [IsActive], [RowIdentifier]) VALUES ('FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', 448, 'BCAS', 'F64F3CF9-C946-4182-B642-9392F805D466', NULL, NULL, 1, NULL)

SET IDENTITY_INSERT #tempTable OFF

GO

SET IDENTITY_INSERT Organisation ON

-- 3: Insert any new items into the table from the table variable
INSERT INTO Organisation ([Code], [ID], [Name], [OrganisationTypeCode], [HEO], [DateDeleted], [IsActive], [RowIdentifier])
SELECT 
Code,
ID,
Name,
OrganisationTypeCode,
HEO,
DateDeleted,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM Organisation)
		
SET IDENTITY_INSERT Organisation OFF
GO
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
** DATE EXECUTED: Jan 17 2013  5:19PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for Application
****************************************/
PRINT 'Inserts for Application'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table Application'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM Application WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

INSERT INTO #tempTable ([Code], [SecurityLabel], [ApplicationName], [Location], [Description], [IsActive], [IsSpecificOrganisationAccessRequired], [RowIdentifier]) VALUES ('BC0D238F-18BE-427D-B53C-B68F8B27DCF7', '39879701-FB41-4331-A069-A57D93F171BE', 'Framework', '', '', 1, 0, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [ApplicationName], [Location], [Description], [IsActive], [IsSpecificOrganisationAccessRequired], [RowIdentifier]) VALUES ('07653ABF-406F-43BC-B41A-C2FFA727138C', '00000000-0000-0000-0000-000000000000', 'BCAS', 'http://localhost/benefitcapweb', 'Benefit Cap Application System (BCAS)', 1, 0, NULL)

GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO Application 
SELECT 
Code,
SecurityLabel,
ApplicationName,
Location,
Description,
IsActive,
IsSpecificOrganisationAccessRequired,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM Application)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.ApplicationName = tmp.ApplicationName,
 LiveTable.Location = tmp.Location,
 LiveTable.Description = tmp.Description,
 LiveTable.IsActive = tmp.IsActive,
 LiveTable.IsSpecificOrganisationAccessRequired = tmp.IsSpecificOrganisationAccessRequired

FROM Application LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:34PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for Role
****************************************/
PRINT 'Inserts for Role'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table Role'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM Role WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.
INSERT INTO #tempTable ([Code], [SecurityLabel], [ApplicationCode], [RoleName], [IsActive], [RowIdentifier]) VALUES ('BDE7B654-AA29-4B7B-8D98-068730042F12', '84B108A0-0938-497A-B48E-B353EAD1E0FF', 'FA7B00D7-3804-4A4D-978C-78C114B0D986', 'FW-APPLICATION', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [ApplicationCode], [RoleName], [IsActive], [RowIdentifier]) VALUES ('3EBF8611-C637-4094-896A-2FEE3F3E293D', '84B108A0-0938-497A-B48E-B353EAD1E0FF', 'FA7B00D7-3804-4A4D-978C-78C114B0D986', 'FW-ADMIN', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [ApplicationCode], [RoleName], [IsActive], [RowIdentifier]) VALUES ('4CA7C6FD-2AE7-4D07-9556-C190C409F5EA', '00000000-0000-0000-0000-000000000000', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-APPLICATION', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO Role 
SELECT 
Code,
SecurityLabel,
ApplicationCode,
RoleName,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM Role)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.ApplicationCode = tmp.ApplicationCode,
 LiveTable.RoleName = tmp.RoleName,
 LiveTable.IsActive = tmp.IsActive

FROM Role LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:23PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for ADRoleLookup
****************************************/
PRINT 'Inserts for ADRoleLookup'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table ADRoleLookup'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM ADRoleLookup WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.
INSERT INTO #tempTable ([Code], [SecurityLabel], [ADGroup], [RoleCode], [IsActive], [RowIdentifier]) VALUES ('4D8F2562-EAC2-4BC5-B640-113D9596DB58', '84B108A0-0938-497A-B48E-B353EAD1E0FF', 'DWP-FW-ADMIN', '3EBF8611-C637-4094-896A-2FEE3F3E293D', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [ADGroup], [RoleCode], [IsActive], [RowIdentifier]) VALUES ('CA773960-5225-433E-9D93-4CFDAB61D7B0', '84B108A0-0938-497A-B48E-B353EAD1E0FF', 'DWP-FW-APPLICATION', 'BDE7B654-AA29-4B7B-8D98-068730042F12', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [ADGroup], [RoleCode], [IsActive], [RowIdentifier]) VALUES ('3C968E61-6C77-496E-92EE-B2980CC64A66', '00000000-0000-0000-0000-000000000000', 'DWP-BCAS-APPLICATION', '4CA7C6FD-2AE7-4D07-9556-C190C409F5EA', 1, NULL)

GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO ADRoleLookup 
SELECT 
Code,
SecurityLabel,
ADGroup,
RoleCode,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM ADRoleLookup)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.ADGroup = tmp.ADGroup,
 LiveTable.RoleCode = tmp.RoleCode,
 LiveTable.IsActive = tmp.IsActive

FROM ADRoleLookup LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:42PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for Grade
****************************************/
PRINT 'Inserts for Grade'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table Grade'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM Grade WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('712175B3-4BA8-4506-BB5E-128D054E8457', '39879701-FB41-4331-A069-A57D93F171BE', 'AA FTA', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('187A4EA2-5864-4F84-8776-17D00E198DB3', '39879701-FB41-4331-A069-A57D93F171BE', 'EO', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('03A59689-E75F-42E2-9F91-24FEF1FBAC4B', '84B108A0-0938-497A-B48E-B353EAD1E0FF', 'HEO', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('7A2F2834-3D44-4F60-8E91-2A13D88C40C5', '39879701-FB41-4331-A069-A57D93F171BE', 'SEO', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('0077D893-5A7D-4B72-A0BB-4A78FEC972BA', '39879701-FB41-4331-A069-A57D93F171BE', 'Grade 7', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('9021DCC7-073F-4DF4-A305-87883EB2F7F3', '00000000-0000-0000-0000-000000000000', 'AA', 0, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('0333B484-6266-4580-851E-953C72B172F3', '39879701-FB41-4331-A069-A57D93F171BE', 'AA CAS', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('E7AECAFC-7319-4EF1-90F8-C42A47568C53', '39879701-FB41-4331-A069-A57D93F171BE', 'AO', 1, NULL)
INSERT INTO #tempTable ([Code], [SecurityLabel], [Grade], [IsActive], [RowIdentifier]) VALUES ('4249199E-FA91-40F8-A58C-EDBCE8F7197F', '39879701-FB41-4331-A069-A57D93F171BE', 'Grade 6', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO Grade 
SELECT 
Code,
SecurityLabel,
Grade,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM Grade)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.Grade = tmp.Grade,
 LiveTable.IsActive = tmp.IsActive

FROM Grade LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:41PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for Staff
****************************************/
PRINT 'Inserts for Staff'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table Staff'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM Staff WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

INSERT INTO #tempTable ([Code], [SecurityLabel], [StaffNumber], [LastName], [FirstName], [GradeCode], [IsActive], [RowIdentifier]) VALUES ('EAEE7A91-754B-4499-958D-1306989F248A', '00000000-0000-0000-0000-000000000000', 'fwkadmin', 'Adep Framework', 'Admin', '03A59689-E75F-42E2-9F91-24FEF1FBAC4B', 1, NULL)

GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO Staff 
SELECT 
Code,
SecurityLabel,
StaffNumber,
LastName,
FirstName,
GradeCode,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM Staff)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.SecurityLabel = tmp.SecurityLabel,
 LiveTable.StaffNumber = tmp.StaffNumber,
 LiveTable.LastName = tmp.LastName,
 LiveTable.FirstName = tmp.FirstName,
 LiveTable.GradeCode = tmp.GradeCode,
 LiveTable.IsActive = tmp.IsActive

FROM Staff LiveTable 
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

INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('BC17F8CD-F976-466A-9763-122CBC4E3F45', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-MI-USER', 'Bool', 0, 1, 1, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('24F1A84E-FE90-46CD-B8BE-1B7AD891C510', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-ACCURACYCHECKER', 'Bool', 0, 1, 1, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('1082B21F-FAB8-4CBD-A880-28E57A4C3574', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-AGENT', 'Bool', 0, 1, 1, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('0EE1B718-F64A-41CE-91D9-29383C794776', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-AccessLevel', 'int', 0, 1, 0, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('990AA41F-0718-4B40-B350-C7F4F09F5CA1', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-TEAMLEADER', 'Bool', 0, 1, 1, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('019B28FE-D49D-4696-B51E-E597FBF0F96E', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-ADMIN', 'Bool', 0, 1, 1, NULL)
INSERT INTO #tempTable ([Code], [ApplicationCode], [AttributeName], [AttributeType], [IsDataSecurity], [IsActive], [IsRole], [RowIdentifier]) VALUES ('A1D2CE88-2670-4635-A857-4D25860B29A8', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'BCAS-MANAGEMENTCHECKADMIN', 'Bool', 0, 1, 1, NULL)

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

INSERT INTO #tempTable ([Code], [SecurityLabel], [StaffCode], [ApplicationCode], [ApplicationAttributeCode], [LookupValue], [IsActive], [RowIdentifier]) VALUES ('81C4528E-9707-40A3-995E-84C071F797F5', '00000000-0000-0000-0000-000000000000', 'EAEE7A91-754B-4499-958D-1306989F248A', '07653ABF-406F-43BC-B41A-C2FFA727138C', '019B28FE-D49D-4696-B51E-E597FBF0F96E', 'Yes', 1, NULL)
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
/****************************************************************************************************************
** Created by: sp_CreateDataFromTablesToInsertCommands                                                         **
*****************************************************************************************************************
** WARNING - IF A TABLE CONTAINS A DATATYPE OF TEXT OR NTEXT ONLY 8K OR 4K CHARS FROM THIS FIELD WILL BE       **
**           RETURNED RESPECTIVELY.                                                                            **
**                                                                                                             **
** BINARY, VARBINARY AND IMAGES ARE CURRENTLY NOT HANDLED AT ALL                                               **
** DATE EXECUTED: Jan 17 2013  5:57PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for StaffOrganisation
****************************************/
PRINT 'Inserts for StaffOrganisation'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table StaffOrganisation'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM StaffOrganisation WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO StaffOrganisation 
SELECT 
Code,
StaffCode,
OrganisationCode,
ApplicationCode,
IsDefault,
IsCurrent,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM StaffOrganisation)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.StaffCode = tmp.StaffCode,
 LiveTable.OrganisationCode = tmp.OrganisationCode,
 LiveTable.ApplicationCode = tmp.ApplicationCode,
 LiveTable.IsDefault = tmp.IsDefault,
 LiveTable.IsCurrent = tmp.IsCurrent

FROM StaffOrganisation LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:25PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for ApplicationOrganisationTypeGroup
****************************************/
PRINT 'Inserts for ApplicationOrganisationTypeGroup'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table ApplicationOrganisationTypeGroup'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM ApplicationOrganisationTypeGroup WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.`
INSERT INTO #tempTable ([Code], [ApplicationCode], [OrganisationTypeGroupCode], [RootOrganisationForApplicationCode], [IsActive], [RowIdentifier]) VALUES ('F5B8F528-541D-40D7-85FF-40D7D8634600', '07653ABF-406F-43BC-B41A-C2FFA727138C', 'B1080D88-36EE-4EE7-B2FB-7D96851C7F9D', 'FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', 1, NULL)
GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO ApplicationOrganisationTypeGroup 
SELECT 
Code,
ApplicationCode,
OrganisationTypeGroupCode,
RootOrganisationForApplicationCode,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM ApplicationOrganisationTypeGroup)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.ApplicationCode = tmp.ApplicationCode,
 LiveTable.OrganisationTypeGroupCode = tmp.OrganisationTypeGroupCode,
 LiveTable.RootOrganisationForApplicationCode = tmp.RootOrganisationForApplicationCode,
 LiveTable.IsActive = tmp.IsActive

FROM ApplicationOrganisationTypeGroup LiveTable 
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
** DATE EXECUTED: Jan 17 2013  5:29PM                                                           **
*****************************************************************************************************************/
/****************************************
** Inserts for OrganisationHierarchy
****************************************/
PRINT 'Inserts for OrganisationHierarchy'
--insertstatement
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
PRINT 'Updating static data table OrganisationHierarchy'

-- Set to your regions date format to ensure dates are updated correctly

SET DATEFORMAT dmy

-- 1: Define table variable
SELECT * INTO #tempTable FROM OrganisationHierarchy WHERE 1 = 0


-- 2: Populate the table variable with data
-- This is where you manage your data in source control. You
-- can add and modify entries, but because of potential foreign
-- key contraint violations this script will not delete any
-- removed entries. If you remove an entry then it will no longer
-- be added to new databases based on your schema, but the entry
-- will not be deleted from databases in which the value already exists.

INSERT INTO #tempTable ([Code], [AncestorOrganisationCode], [OrganisationCode], [ImmediateParent], [HopsBetweenOrgAndAncestor], [IsActive], [RowIdentifier]) VALUES ('80C23B05-02D0-4EA2-B863-80371D8C0F01', 'FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', 'FB7BE9EC-D95B-4D29-9D47-FFEDEC64687B', 0, 0, 1, NULL)

GO
-- 3: Insert any new items into the table from the table variable
INSERT INTO OrganisationHierarchy 
SELECT 
Code,
AncestorOrganisationCode,
OrganisationCode,
ImmediateParent,
HopsBetweenOrgAndAncestor,
IsActive,
NULL
FROM #tempTable WHERE [Code] NOT IN (SELECT [Code] FROM OrganisationHierarchy)

UPDATE LiveTable SET
 LiveTable.Code = tmp.Code,
 LiveTable.AncestorOrganisationCode = tmp.AncestorOrganisationCode,
 LiveTable.OrganisationCode = tmp.OrganisationCode,
 LiveTable.ImmediateParent = tmp.ImmediateParent,
 LiveTable.HopsBetweenOrgAndAncestor = tmp.HopsBetweenOrgAndAncestor,
 LiveTable.IsActive = tmp.IsActive

FROM OrganisationHierarchy LiveTable 
	INNER JOIN 
		#tempTable tmp 
			ON LiveTable.Code = tmp.Code

DROP TABLE #temptable

PRINT 'Finished updating static data table'

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[CategoryLog] WITH CHECK CHECK CONSTRAINT [FK_CategoryLog_Category];

ALTER TABLE [dbo].[CategoryLog] WITH CHECK CHECK CONSTRAINT [FK_CategoryLog_Log];

ALTER TABLE [dbo].[Country] WITH CHECK CHECK CONSTRAINT [FK_Country_Organisation_Security];

ALTER TABLE [dbo].[Holiday] WITH CHECK CHECK CONSTRAINT [FK_Holiday_Organisation_Security];

ALTER TABLE [dbo].[StaffDetails] WITH CHECK CHECK CONSTRAINT [FK_StaffDetails_Staff];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
