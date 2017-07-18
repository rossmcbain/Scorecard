CREATE PROCEDURE [dbo].ScorecardSelectCommand
AS
	SET NOCOUNT ON;
SELECT ScorecardID, ScorecardName, ScorecardDescription, PassMark FROM dbo.Scorecard