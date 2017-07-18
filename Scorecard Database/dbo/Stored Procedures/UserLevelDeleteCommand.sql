CREATE PROCEDURE [dbo].UserLevelDeleteCommand
(
	@Original_UserLevelID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[UserLevel] WHERE (([UserLevelID] = @Original_UserLevelID))