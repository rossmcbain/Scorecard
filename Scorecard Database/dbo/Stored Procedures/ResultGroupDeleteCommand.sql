CREATE PROCEDURE [dbo].ResultGroupDeleteCommand
(
	@Original_ResultGroupID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[ResultGroup] WHERE (([ResultGroupID] = @Original_ResultGroupID))