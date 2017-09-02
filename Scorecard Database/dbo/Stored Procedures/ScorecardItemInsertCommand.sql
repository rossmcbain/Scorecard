CREATE PROCEDURE [dbo].ScorecardItemInsertCommand
(
	@ScorecardID int,
	@Question varchar(500),
	@QuestionType varchar(20),
	@PossibleAnswers varchar(1000),
	@ScoreModifier int,
	@AutoFail bit,
	@ScorecardItemGroupID int,
	@Answer varchar(500)
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[ScorecardItem] ([ScorecardID], [Question], [QuestionType], [PossibleAnswers], [ScoreModifier], [AutoFail], [ScorecardItemGroupID], [Answer]) VALUES (@ScorecardID, @Question, @QuestionType, @PossibleAnswers, @ScoreModifier, @AutoFail, @ScorecardItemGroupID, @Answer);
	
SELECT ScorecardItemID, ScorecardID, Question, QuestionType, PossibleAnswers, ScoreModifier, AutoFail, ScorecardItemGroupID, Answer FROM ScorecardItem WHERE (ScorecardItemID = SCOPE_IDENTITY())

RETURN SCOPE_IDENTITY()