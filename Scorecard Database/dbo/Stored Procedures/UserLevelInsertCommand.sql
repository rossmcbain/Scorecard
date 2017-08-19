CREATE PROCEDURE [dbo].UserLevelInsertCommand
(
	@Description varchar(100),
	@PagePermissions varchar(MAX)
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[UserLevel] ([Description], [PagePermissions]) VALUES (@Description, @PagePermissions);
	
SELECT UserLevelID, Description, PagePermissions FROM UserLevel WHERE (UserLevelID = SCOPE_IDENTITY())

RETURN SCOPE_IDENTITY()