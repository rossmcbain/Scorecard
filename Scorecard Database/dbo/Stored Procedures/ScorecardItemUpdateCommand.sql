CREATE PROCEDURE [dbo].ScorecardItemUpdateCommand
(
	@ScorecardID int,
	@Question varchar(500),
	@QuestionType varchar(20),
	@PossibleAnswers varchar(1000),
	@ScoreModifier int,
	@AutoFail bit,
	@ScorecardItemGroupID int,
	@Original_ScorecardItemID int,
	@ScorecardItemID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[ScorecardItem] SET [ScorecardID] = @ScorecardID, [Question] = @Question, [QuestionType] = @QuestionType, [PossibleAnswers] = @PossibleAnswers, [ScoreModifier] = @ScoreModifier, [AutoFail] = @AutoFail, [ScorecardItemGroupID] = @ScorecardItemGroupID WHERE (([ScorecardItemID] = @Original_ScorecardItemID));
	
SELECT ScorecardItemID, ScorecardID, Question, QuestionType, PossibleAnswers, ScoreModifier, AutoFail, ScorecardItemGroupID FROM ScorecardItem WHERE (ScorecardItemID = @ScorecardItemID)