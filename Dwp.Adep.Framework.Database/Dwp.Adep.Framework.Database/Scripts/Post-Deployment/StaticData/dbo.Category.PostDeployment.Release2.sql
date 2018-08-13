-- =============================================
-- Static Data Script for Category Table
-- Date		10/05/2013
-- Author	G Knibbs
-- =============================================

PRINT 'Inserts for Category'

DECLARE @CategoryName AS NVARCHAR(64)
SET @CategoryName = 'General'

IF NOT EXISTS (SELECT CategoryID FROM Category WHERE CategoryName = @CategoryName)
BEGIN
	INSERT INTO [Category]
			   ([CategoryName])
		 VALUES
			   (@CategoryName)
END

PRINT 'Finished updating static data table'

GO
