CREATE PROCEDURE [dbo].ScorecardDeleteCommand
(
	@Original_ScorecardID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[Scorecard] WHERE (([ScorecardID] = @Original_ScorecardID))