CREATE PROCEDURE [dbo].ResultGroupUpdateCommand
(
	@ResultID int,
	@ScorecardItemGroupID int,
	@Comment varchar(5000),
	@Score int,
	@Original_ResultGroupID int,
	@ResultGroupID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[ResultGroup] SET [ResultID] = @ResultID, [ScorecardItemGroupID] = @ScorecardItemGroupID, [Comment] = @Comment, [Score] = @Score WHERE (([ResultGroupID] = @Original_ResultGroupID));
	
SELECT ResultGroupID, ResultID, ScorecardItemGroupID, Comment, Score FROM ResultGroup WHERE (ResultGroupID = @ResultGroupID)