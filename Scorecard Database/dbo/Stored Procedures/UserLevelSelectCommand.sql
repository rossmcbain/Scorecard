CREATE PROCEDURE [dbo].UserLevelSelectCommand
AS
	SET NOCOUNT ON;
SELECT UserLevelID, Description, PagePermissions FROM dbo.UserLevel

