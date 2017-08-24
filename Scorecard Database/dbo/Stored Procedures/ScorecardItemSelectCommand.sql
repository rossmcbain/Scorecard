CREATE PROCEDURE [dbo].ScorecardItemSelectCommand
@ScorecardID int = null,
@ScorecardItemGroupID int = null
AS
	SET NOCOUNT ON;
SELECT ScorecardItemID, ScorecardID, Question, QuestionType, PossibleAnswers, ScoreModifier, AutoFail, ScorecardItemGroupID FROM dbo.ScorecardItem
where (@ScorecardID is null or ScorecardID = @ScorecardID)
and
(@ScorecardItemGroupID is null or ScorecardItemGroupID = @ScorecardItemGroupID)