CREATE PROCEDURE [dbo].ScorecardItemGroupUpdateCommand
(
	@ScorecardID int,
	@GroupName varchar(100),
	@Description varchar(5000),
	@PassScore int,
	@Original_ScorecardItemGroupID int,
	@ScorecardItemGroupID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[ScorecardItemGroup] SET [ScorecardID] = @ScorecardID, [GroupName] = @GroupName, [Description] = @Description, [PassScore] = @PassScore WHERE (([ScorecardItemGroupID] = @Original_ScorecardItemGroupID));
	
SELECT ScorecardItemGroupID, ScorecardID, GroupName, Description, PassScore FROM ScorecardItemGroup WHERE (ScorecardItemGroupID = @ScorecardItemGroupID)