CREATE PROCEDURE [dbo].ScorecardSelectCommand
@ScorecardID int = null
AS
	SET NOCOUNT ON;
SELECT ScorecardID, ScorecardName, ScorecardDescription, PassMark FROM dbo.Scorecard
where (@ScorecardID is null or ScorecardID = @ScorecardID)
