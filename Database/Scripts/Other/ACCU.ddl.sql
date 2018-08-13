-- =============================================
-- Script Template
-- =============================================



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Checker]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Checker](
	[sStaffNumber] [nvarchar](8) NULL,
	[sLastName] [nvarchar](35) NULL,
	[sFirstName] [nvarchar](35) NULL,
	[bOPIT] [bit] NOT NULL,
	[bCST] [bit] NOT NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckSubType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CheckSubType](
	[lTableID] [int] NOT NULL,
	[lCheckTypeID] [int] NULL,
	[sDesc] [nvarchar](35) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CheckType](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](50) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Command]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Command](
	[lID] [int] NULL,
	[sCommand] [nvarchar](35) NULL,
	[sHEO] [nvarchar](35) NULL,
	[dteDeleted] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](60) NULL,
	[bFrozenRate] [bit] NOT NULL,
	[bEC] [bit] NOT NULL,
	[bRA] [bit] NOT NULL,
	[bANZAC] [bit] NOT NULL,
	[sNationality] [nvarchar](50) NULL,
	[sCode] [nvarchar](4) NULL,
	[memNotes] [nvarchar](max) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorChoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorChoice](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](80) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorSubChoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorSubChoice](
	[lTableID] [int] NOT NULL,
	[lErrorChoiceID] [int] NULL,
	[sDesc] [nvarchar](130) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ErrorType](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](35) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grade]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Grade](
	[lID] [int] NULL,
	[sGrade] [nvarchar](10) NULL,
	[dteDeleted] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastUpdate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LastUpdate](
	[lID] [int] NOT NULL,
	[dteLastUpdate] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Location](
	[lID] [int] NULL,
	[sLocation] [nvarchar](12) NULL,
	[dteDeleted] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReasonForDelay]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ReasonForDelay](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](40) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ReportType](
	[lTableID] [int] NOT NULL,
	[sDescription] [nvarchar](50) NULL,
	[bOPIT_Report] [bit] NOT NULL,
	[bOpsReport] [bit] NOT NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScheduleNumber]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ScheduleNumber](
	[lTableID] [int] NOT NULL,
	[sNumber] [nvarchar](14) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Server](
	[lTableID] [int] NOT NULL,
	[sNumber] [nvarchar](7) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Team]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Team](
	[lID] [int] NULL,
	[sTeam] [nvarchar](35) NULL,
	[dteDeleted] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Test](
	[lID] [float] NULL,
	[sTeam] [nvarchar](50) NULL,
	[dteDeleted] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Validator]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Validator](
	[sStaffNumber] [nvarchar](8) NULL,
	[sFirstName] [nvarchar](35) NULL,
	[sLastName] [nvarchar](35) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VAT2000Number]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VAT2000Number](
	[lTableID] [int] NOT NULL,
	[lNumber] [int] NULL,
	[sDesc] [nvarchar](100) NULL,
	[iTargetDays] [smallint] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Holiday]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Holiday](
	[lTableID] [int] NOT NULL,
	[dteDate] [datetime] NULL,
	[sngDays] [real] NULL,
	[bNational] [bit] NOT NULL,
	[sDescription] [nvarchar](30) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Check1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Check1](
	[lTableID] [int] NOT NULL,
	[lSEF_ID] [int] NULL,
	[dteRaised] [datetime] NULL,
	[lCheckerTableID] [int] NULL,
	[lCustomerTableID] [int] NULL,
	[lLiaisonCountryTableID] [int] NULL,
	[lReasonForDelayTableID] [int] NULL,
	[lReferredTableID] [int] NULL,
	[lVat2000NumberTableID] [int] NULL,
	[lServerTableID] [int] NULL,
	[bPOD1274] [bit] NOT NULL,
	[dteAccountDeletion] [datetime] NULL,
	[dteBF] [datetime] NULL,
	[dteCheckGenerated] [datetime] NULL,
	[dteCheckCompleted] [datetime] NULL,
	[lDaysToClear] [int] NULL,
	[dteCheckCompletedEntered] [datetime] NULL,
	[dteEO_Reminder] [datetime] NULL,
	[dteEvidenceRequested] [datetime] NULL,
	[dteHEO_Reminder] [datetime] NULL,
	[dteSentToSAT] [datetime] NULL,
	[dteReceivedBySAT] [datetime] NULL,
	[dteSentToIST] [datetime] NULL,
	[dteReceivedByIST] [datetime] NULL,
	[dteReceivedInOPS] [datetime] NULL,
	[dteReferred] [datetime] NULL,
	[dteRequestReceivedByOps] [datetime] NULL,
	[dteReturnedBySAT] [datetime] NULL,
	[dteValidation] [datetime] NULL,
	[memNotes] [nvarchar](max) NULL,
	[memBF_Notes] [nvarchar](max) NULL,
	[sUserStaffNumber] [nvarchar](8) NULL,
	[sUserSurname] [nvarchar](35) NULL,
	[sUserForename] [nvarchar](35) NULL,
	[sUserGrade] [nvarchar](3) NULL,
	[sTeam1] [nvarchar](50) NULL,
	[sCommand] [nvarchar](50) NULL,
	[sLocation] [nvarchar](7) NULL,
	[dteGeneratedMonth] [datetime] NULL,
	[dteClearedMonth] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[lTableID] [int] NOT NULL,
	[dteRaised] [datetime] NULL,
	[lTitleTableID] [int] NULL,
	[sSurname] [nvarchar](35) NULL,
	[sForenames] [nvarchar](70) NULL,
	[sRequestedSurname] [nvarchar](35) NULL,
	[sMaidenName] [nvarchar](35) NULL,
	[dteDOB] [datetime] NULL,
	[sNI_Number] [nvarchar](8) NULL,
	[sStagger] [nvarchar](1) NULL,
	[sAddress1] [nvarchar](35) NULL,
	[sAddress2] [nvarchar](35) NULL,
	[sAddress3] [nvarchar](35) NULL,
	[sAddress4] [nvarchar](35) NULL,
	[sAddress5] [nvarchar](35) NULL,
	[sPostCode] [nvarchar](10) NULL,
	[lCountryOfResidenceTableID] [int] NULL,
	[sTelephone] [nvarchar](20) NULL,
	[dteLastSaved] [datetime] NULL,
	[memNotes] [nvarchar](max) NULL,
	[dteWeed] [datetime] NULL,
	[lLinkedCustomerTableID] [int] NULL,
	[lUserTableID] [int] NULL,
	[bDelete] [bit] NOT NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Commands]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Commands](
	[sCommand] [nvarchar](50) NULL,
	[sAmendedCommand] [nvarchar](50) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Referred]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Referred](
	[lTableID] [int] NOT NULL,
	[sDescription] [nvarchar](40) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Title]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Title](
	[lTableID] [int] NOT NULL,
	[sDesc] [nvarchar](9) NOT NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Check]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Check](
	[lTableID] [int] NOT NULL,
	[dteRaisedDate] [datetime] NULL,
	[bOPIT_Check] [bit] NOT NULL,
	[bCST_Check] [bit] NOT NULL,
	[bNewClaim] [bit] NOT NULL,
	[bChangeOfCircs] [bit] NOT NULL,
	[sCheckerStaffNumber] [nvarchar](8) NULL,
	[lBenefitID] [int] NULL,
	[dteGeneratedDate] [datetime] NULL,
	[lCheckTypeID] [int] NULL,
	[lCheckSubTypeID] [int] NULL,
	[bPrePaymentCheck] [bit] NOT NULL,
	[bPostPaymentCheck] [bit] NOT NULL,
	[sNI_Number] [nvarchar](9) NULL,
	[sCustomerFirstName] [nvarchar](50) NULL,
	[sCustomerLastName] [nvarchar](70) NULL,
	[lCountryID] [int] NULL,
	[bClaim] [bit] NOT NULL,
	[bMaintenance] [bit] NOT NULL,
	[dteStartDateOfClaim] [datetime] NULL,
	[dteEndDateOfClaim] [datetime] NULL,
	[dteClearanceTargetDate] [datetime] NULL,
	[lDaysToClear] [int] NULL,
	[lVAT2000_NumberID] [int] NULL,
	[dteVAT2000_Date] [datetime] NULL,
	[dteVAT2000_TargetDate] [datetime] NULL,
	[lSchedule] [int] NULL,
	[lIOP_OnSchedule] [int] NULL,
	[lServerID] [int] NULL,
	[dtePapersRequestedForChecking] [datetime] NULL,
	[dteRequestReceivedByOps] [datetime] NULL,
	[lReasonForDelayID] [int] NULL,
	[dtePapersSentForChecking] [datetime] NULL,
	[dtePapersReceivedForChecking] [datetime] NULL,
	[sStaffNumber] [nvarchar](8) NULL,
	[lTeamID] [int] NULL,
	[lCommandID] [int] NULL,
	[lLocationID] [int] NULL,
	[dteBF_Date] [datetime] NULL,
	[dteCheckCompletedDate] [datetime] NULL,
	[sValidatorStaffNumber] [nvarchar](8) NULL,
	[dteValidatedOn] [datetime] NULL,
	[dtePapersReturnedToSection] [datetime] NULL,
	[dtePapersReceivedInOps] [datetime] NULL,
	[memValidationNotes] [nvarchar](max) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Error]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Error](
	[lTableID] [int] NOT NULL,
	[lCheckID] [int] NULL,
	[dteRaisedDate] [datetime] NULL,
	[dteDateOccurred] [datetime] NULL,
	[dteDateFound] [datetime] NULL,
	[lDaysToDetect] [int] NULL,
	[lErrorTypeID] [int] NULL,
	[bOverpayment] [bit] NOT NULL,
	[bUnderpayment] [bit] NOT NULL,
	[lErrorChoiceID] [int] NULL,
	[lErrorSubChoiceID] [int] NULL,
	[curWeeklyAmount] [money] NULL,
	[curTotalOverpayment] [money] NULL,
	[sStaffNumber] [nvarchar](8) NULL,
	[lTeamID] [int] NULL,
	[lCommandID] [int] NULL,
	[lLocationID] [int] NULL,
	[dteDateReturnedForCorrection] [datetime] NULL,
	[dteDateCorrectiveAction] [datetime] NULL,
	[bEO_AgreeNo] [bit] NOT NULL,
	[bEO_AgreeYes] [bit] NOT NULL,
	[memCorrectiveActionNotes] [nvarchar](max) NULL,
	[memNotes] [nvarchar](max) NULL,
	[memOPIT_Notes] [nvarchar](max) NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportStaff]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImportStaff](
	[sStaffNumber] [nvarchar](8) NULL,
	[sLastName] [nvarchar](35) NULL,
	[sFirstName] [nvarchar](35) NULL,
	[sNetworkLastName] [nvarchar](35) NULL,
	[sNetworkFirstName] [nvarchar](35) NULL,
	[lGradeID] [int] NULL,
	[lTeamID] [int] NULL,
	[lCommandID] [int] NULL,
	[lLocationID] [int] NULL,
	[bNonIPC] [bit] NOT NULL,
	[dteLeftIPC] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Staff]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Staff](
	[sStaffNumber] [nvarchar](8) NULL,
	[sLastName] [nvarchar](35) NULL,
	[sFirstName] [nvarchar](35) NULL,
	[sNetworkLastName] [nvarchar](35) NULL,
	[sNetworkFirstName] [nvarchar](35) NULL,
	[lGradeID] [int] NULL,
	[lTeamID] [int] NULL,
	[lCommandID] [int] NULL,
	[lLocationID] [int] NULL,
	[bUser] [bit] NOT NULL,
	[bReport] [bit] NOT NULL,
	[bValidator] [bit] NOT NULL,
	[bAdmin] [bit] NOT NULL,
	[bSupport] [bit] NOT NULL,
	[bNonIPC] [bit] NOT NULL,
	[dteLeftIPC] [datetime] NULL
) ON [Data1]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Benefit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Benefit](
	[lTableID] [int] NOT NULL,
	[sBenefit] [nvarchar](50) NULL
) ON [Data1]
END
