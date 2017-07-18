CREATE PROCEDURE [dbo].UserLevelUpdateCommand
(
	@Description varchar(100),
	@PagePermissions varchar(MAX),
	@Original_UserLevelID int,
	@UserLevelID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[UserLevel] SET [Description] = @Description, [PagePermissions] = @PagePermissions WHERE (([UserLevelID] = @Original_UserLevelID));
	
SELECT UserLevelID, Description, PagePermissions FROM UserLevel WHERE (UserLevelID = @UserLevelID)