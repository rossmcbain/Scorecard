CREATE TABLE [dbo].[User]
(
	[UserID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] VARCHAR(100) NOT NULL, 
    [FirstName] VARCHAR(100) NOT NULL, 
    [Surname] VARCHAR(100) NOT NULL, 
    [EmailAddress] VARCHAR(500) NOT NULL, 
    [UserLevelID] INT NOT NULL, 
    CONSTRAINT [FK_User_UserLevel] FOREIGN KEY ([UserLevelID]) REFERENCES [UserLevel]([UserLevelID])
)
