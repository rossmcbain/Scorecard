CREATE PROCEDURE [dbo].UserDeleteCommand
(
	@Original_UserID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[User] WHERE (([UserID] = @Original_UserID))