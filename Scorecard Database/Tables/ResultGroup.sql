CREATE TABLE [dbo].[ResultGroup]
(
	[ResultGroupID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ResultID] INT NOT NULL,
	[ScorecardItemGroupID] INT NOT NULL,
    [Comment] VARCHAR(5000) NULL, 
    [Score] INT NOT NULL, 
    CONSTRAINT [FK_ResultGroup_ScorecardItemGroup] FOREIGN KEY ([ScorecardItemGroupID]) REFERENCES [ScorecardItemGroup]([ScorecardItemGroupID]), 
    CONSTRAINT [FK_ResultGroup_ResultID] FOREIGN KEY ([ResultID]) REFERENCES [Result]([ResultID])

)
