CREATE TABLE [dbo].[Scorecard]
(
	[ScorecardID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ScorecardName] VARCHAR(100) NOT NULL, 
    [ScorecardDescription] VARCHAR(5000) NULL, 
    [PassMark] INT NULL
)
