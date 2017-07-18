CREATE PROCEDURE [dbo].ResultInsertCommand
(
	@AgentID int,
	@ScorerID int,
	@ScorecardID int,
	@DateScored datetime,
	@CallReference int,
	@Score int,
	@Comment varchar(5000)
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[Result] ([AgentID], [ScorerID], [ScorecardID], [DateScored], [CallReference], [Score], [Comment]) VALUES (@AgentID, @ScorerID, @ScorecardID, @DateScored, @CallReference, @Score, @Comment);
	
SELECT ResultID, AgentID, ScorerID, ScorecardID, DateScored, CallReference, Score, Comment FROM Result WHERE (ResultID = SCOPE_IDENTITY())