CREATE TABLE [dbo].[UserLevel]
(
	[UserLevelID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Description] VARCHAR(100) NOT NULL, 
    [PagePermissions] VARCHAR(MAX) NULL
)
