CREATE PROCEDURE [dbo].ResultUpdateCommand
(
	@AgentID int,
	@ScorerID int,
	@ScorecardID int,
	@DateScored datetime,
	@CallReference int,
	@Score int,
	@Comment varchar(5000),
	@Original_ResultID int,
	@ResultID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[Result] SET [AgentID] = @AgentID, [ScorerID] = @ScorerID, [ScorecardID] = @ScorecardID, [DateScored] = @DateScored, [CallReference] = @CallReference, [Score] = @Score, [Comment] = @Comment WHERE (([ResultID] = @Original_ResultID));
	
SELECT ResultID, AgentID, ScorerID, ScorecardID, DateScored, CallReference, Score, Comment FROM Result WHERE (ResultID = @ResultID)