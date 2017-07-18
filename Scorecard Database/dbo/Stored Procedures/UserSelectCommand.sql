CREATE PROCEDURE [dbo].UserSelectCommand
AS
	SET NOCOUNT ON;
SELECT UserID, Username, FirstName, Surname, EmailAddress, UserLevelID FROM dbo.[User]