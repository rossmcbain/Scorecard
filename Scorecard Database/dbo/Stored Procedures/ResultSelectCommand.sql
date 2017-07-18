CREATE PROCEDURE [dbo].ResultSelectCommand
AS
	SET NOCOUNT ON;
SELECT ResultID, AgentID, ScorerID, ScorecardID, DateScored, CallReference, Score, Comment FROM dbo.Result