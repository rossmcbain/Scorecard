CREATE PROCEDURE [dbo].ScorecardItemSelectCommand
AS
	SET NOCOUNT ON;
SELECT ScorecardItemID, ScorecardID, Question, QuestionType, PossibleAnswers, ScoreModifier, AutoFail, ScorecardItemGroupID FROM dbo.ScorecardItem