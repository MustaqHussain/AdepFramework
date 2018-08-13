
CREATE PROCEDURE [dbo].[SetContextInfo]
	@contextInfo VARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @infoAsBinary BINARY(128)
	
	-- Convert text to binary
	SET @infoAsBinary = CAST(@contextInfo AS BINARY(128))
	
	-- Save context info
	SET context_info @infoAsBinary
END
