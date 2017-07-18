CREATE PROCEDURE [dbo].ResultGroupSelectCommand
AS
	SET NOCOUNT ON;
SELECT ResultGroupID, ResultID, ScorecardItemGroupID, Comment, Score FROM dbo.ResultGroup