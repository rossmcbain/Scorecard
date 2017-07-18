CREATE PROCEDURE [dbo].ResultItemDeleteCommand
(
	@Original_ResultItemID int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [dbo].[ResultItem] WHERE (([ResultItemID] = @Original_ResultItemID))