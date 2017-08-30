CREATE PROCEDURE [dbo].ResultItemSelectCommand
@ResultID int = null
AS
	SET NOCOUNT ON;
SELECT ResultItemID, ResultID, QuestionID, Answer, Score, Comment, ResultGroupID FROM dbo.ResultItem
WHERE (@ResultID is null or ResultID = @ResultID)