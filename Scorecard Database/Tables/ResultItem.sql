CREATE TABLE [dbo].[ResultItem]
(
	[ResultItemID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ResultID] INT NOT NULL,
    [QuestionID] INT NOT NULL, 
    [Answer] VARCHAR(100) NOT NULL, 
    [Score] INT NOT NULL, 
    [Comment] VARCHAR(5000) NULL, 
    [ResultGroupID] INT NOT NULL, 
    CONSTRAINT [FK_ResultItem_Result] FOREIGN KEY ([ResultID]) REFERENCES [Result]([ResultID]), 
    CONSTRAINT [FK_ResultItem_ResultGroup] FOREIGN KEY ([ResultGroupID]) REFERENCES [ResultGroup]([ResultGroupID])
)
