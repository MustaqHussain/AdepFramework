﻿/****************************************************************************************************************
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
IF '$(IncludeStaticDataRelease1)' = 'true'
BEGIN

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

END
DROP TABLE #temptable

PRINT 'Finished updating static data table'

GO