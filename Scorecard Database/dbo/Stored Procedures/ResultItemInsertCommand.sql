CREATE PROCEDURE [dbo].ResultItemInsertCommand
(
	@ResultID int,
	@QuestionID int,
	@Answer varchar(100),
	@Score int,
	@Comment varchar(5000),
	@ResultGroupID int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[ResultItem] ([ResultID], [QuestionID], [Answer], [Score], [Comment], [ResultGroupID]) VALUES (@ResultID, @QuestionID, @Answer, @Score, @Comment, @ResultGroupID);
	
SELECT ResultItemID, ResultID, QuestionID, Answer, Score, Comment, ResultGroupID FROM ResultItem WHERE (ResultItemID = SCOPE_IDENTITY())