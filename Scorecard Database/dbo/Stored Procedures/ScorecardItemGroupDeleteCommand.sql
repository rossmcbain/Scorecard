CREATE PROCEDURE [dbo].ScorecardItemGroupDeleteCommand
(
	@Original_ScorecardItemGroupID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[ScorecardItemGroup] WHERE (([ScorecardItemGroupID] = @Original_ScorecardItemGroupID))