CREATE PROCEDURE [dbo].ResultItemSelectCommand
AS
	SET NOCOUNT ON;
SELECT ResultItemID, ResultID, QuestionID, Answer, Score, Comment, ResultGroupID FROM dbo.ResultItem