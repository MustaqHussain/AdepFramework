﻿/****************************************************************************************************************
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
IF '$(IncludeStaticDataRelease1)' = 'true'
BEGIN
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

END
DROP TABLE #temptable

PRINT 'Finished updating static data table'

GO