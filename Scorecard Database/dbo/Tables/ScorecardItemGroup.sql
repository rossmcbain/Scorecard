CREATE TABLE [dbo].[ScorecardItemGroup]
(
	[ScorecardItemGroupID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ScorecardID] INT NOT NULL,
    [GroupName] VARCHAR(100) NOT NULL, 
    [Description] VARCHAR(5000) NULL, 
    [PassScore] INT NULL, 
    CONSTRAINT [FK_ScorecardItemGroup_Scorecard] FOREIGN KEY ([ScorecardID]) REFERENCES [Scorecard]([ScorecardID])
)
