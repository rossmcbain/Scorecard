CREATE TABLE [dbo].[ScorecardItem]
(
	[ScorecardItemID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ScorecardID] INT NOT NULL,
    [Question] VARCHAR(500) NOT NULL, 
    [QuestionType] VARCHAR(20) NOT NULL, 
    [PossibleAnswers] VARCHAR(1000) NULL, 
    [ScoreModifier] INT NOT NULL, 
    [AutoFail] BIT NOT NULL, 
    [ScorecardItemGroupID] INT NOT NULL, 
    CONSTRAINT [FK_ScorecardItem_Scorecard] FOREIGN KEY ([ScorecardID]) REFERENCES [Scorecard]([ScorecardID]), 
    CONSTRAINT [FK_ScorecardItem_ScorecardItemGroup] FOREIGN KEY ([ScorecardItemGroupID]) REFERENCES [ScorecardItemGroup]([ScorecardItemGroupID])
)
