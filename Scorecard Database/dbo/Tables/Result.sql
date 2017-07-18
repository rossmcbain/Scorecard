CREATE TABLE [dbo].[Result]
(
	[ResultID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AgentID] INT NOT NULL, 
    [ScorerID] INT NOT NULL, 
    [ScorecardID] INT NOT NULL, 
    [DateScored] DATETIME NOT NULL, 
    [CallReference] INT NOT NULL, 
    [Score] INT NOT NULL, 
    [Comment] VARCHAR(5000) NULL, 
    CONSTRAINT [FK_Result_Scorecard] FOREIGN KEY ([ScorecardID]) REFERENCES [Scorecard]([ScorecardID]), 
    CONSTRAINT [FK_Result_User_AgentID] FOREIGN KEY ([AgentID]) REFERENCES [User]([UserID]), 
    CONSTRAINT [FK_Result_User_ScorerID] FOREIGN KEY ([ScorerID]) REFERENCES [User]([UserID])
)
