CREATE PROCEDURE [dbo].ResultSelectCommand
@ScorecardID int = null
AS
	SET NOCOUNT ON;
SELECT ResultID, AgentID, ScorerID, ScorecardID, DateScored, CallReference, Score, Comment FROM dbo.Result
WHERE (@ScorecardID is null or ScorecardID = @ScorecardID)