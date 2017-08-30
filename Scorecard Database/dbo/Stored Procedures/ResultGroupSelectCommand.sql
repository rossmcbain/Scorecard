CREATE PROCEDURE [dbo].ResultGroupSelectCommand
@ResultID int = null
AS
	SET NOCOUNT ON;
SELECT ResultGroupID, ResultID, ScorecardItemGroupID, Comment, Score FROM dbo.ResultGroup
WHERE (@ResultID is null or ResultID = @ResultID)