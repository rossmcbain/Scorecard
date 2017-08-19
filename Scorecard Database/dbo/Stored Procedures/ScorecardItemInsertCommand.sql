CREATE PROCEDURE [dbo].ScorecardItemInsertCommand
(
	@ScorecardID int,
	@Question varchar(500),
	@QuestionType varchar(20),
	@PossibleAnswers varchar(1000),
	@ScoreModifier int,
	@AutoFail bit,
	@ScorecardItemGroupID int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[ScorecardItem] ([ScorecardID], [Question], [QuestionType], [PossibleAnswers], [ScoreModifier], [AutoFail], [ScorecardItemGroupID]) VALUES (@ScorecardID, @Question, @QuestionType, @PossibleAnswers, @ScoreModifier, @AutoFail, @ScorecardItemGroupID);
	
SELECT ScorecardItemID, ScorecardID, Question, QuestionType, PossibleAnswers, ScoreModifier, AutoFail, ScorecardItemGroupID FROM ScorecardItem WHERE (ScorecardItemID = SCOPE_IDENTITY())

RETURN SCOPE_IDENTITY()