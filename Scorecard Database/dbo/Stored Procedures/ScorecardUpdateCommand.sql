CREATE PROCEDURE [dbo].ScorecardUpdateCommand
(
	@ScorecardName varchar(100),
	@ScorecardDescription varchar(5000),
	@PassMark int,
	@Original_ScorecardID int,
	@ScorecardID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[Scorecard] SET [ScorecardName] = @ScorecardName, [ScorecardDescription] = @ScorecardDescription, [PassMark] = @PassMark WHERE (([ScorecardID] = @Original_ScorecardID));
	
SELECT ScorecardID, ScorecardName, ScorecardDescription, PassMark FROM Scorecard WHERE (ScorecardID = @ScorecardID)