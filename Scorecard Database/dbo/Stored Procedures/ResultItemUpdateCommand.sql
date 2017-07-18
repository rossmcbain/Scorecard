CREATE PROCEDURE [dbo].ResultItemUpdateCommand
(
	@ResultID int,
	@QuestionID int,
	@Answer varchar(100),
	@Score int,
	@Comment varchar(5000),
	@ResultGroupID int,
	@Original_ResultItemID int,
	@ResultItemID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[ResultItem] SET [ResultID] = @ResultID, [QuestionID] = @QuestionID, [Answer] = @Answer, [Score] = @Score, [Comment] = @Comment, [ResultGroupID] = @ResultGroupID WHERE (([ResultItemID] = @Original_ResultItemID));
	
SELECT ResultItemID, ResultID, QuestionID, Answer, Score, Comment, ResultGroupID FROM ResultItem WHERE (ResultItemID = @ResultItemID)