CREATE PROCEDURE [dbo].ResultGroupInsertCommand
(
	@ResultID int,
	@ScorecardItemGroupID int,
	@Comment varchar(5000),
	@Score int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[ResultGroup] ([ResultID], [ScorecardItemGroupID], [Comment], [Score]) VALUES (@ResultID, @ScorecardItemGroupID, @Comment, @Score);
	
SELECT ResultGroupID, ResultID, ScorecardItemGroupID, Comment, Score FROM ResultGroup WHERE (ResultGroupID = SCOPE_IDENTITY())

RETURN SCOPE_IDENTITY()