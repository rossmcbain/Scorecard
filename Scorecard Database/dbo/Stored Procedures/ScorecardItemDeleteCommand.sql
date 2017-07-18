CREATE PROCEDURE [dbo].ScorecardItemDeleteCommand
(
	@Original_ScorecardItemID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[ScorecardItem] WHERE (([ScorecardItemID] = @Original_ScorecardItemID))