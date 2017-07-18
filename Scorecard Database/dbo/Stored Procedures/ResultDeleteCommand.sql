CREATE PROCEDURE [dbo].ResultDeleteCommand
(
	@Original_ResultID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[Result] WHERE (([ResultID] = @Original_ResultID))