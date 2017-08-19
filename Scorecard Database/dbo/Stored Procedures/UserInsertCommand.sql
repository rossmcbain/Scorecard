CREATE PROCEDURE [dbo].UserInsertCommand
(
	@Username varchar(100),
	@FirstName varchar(100),
	@Surname varchar(100),
	@EmailAddress varchar(500),
	@UserLevelID int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[User] ([Username], [FirstName], [Surname], [EmailAddress], [UserLevelID]) VALUES (@Username, @FirstName, @Surname, @EmailAddress, @UserLevelID);
	
SELECT UserID, Username, FirstName, Surname, EmailAddress, UserLevelID FROM [User] WHERE (UserID = SCOPE_IDENTITY())

RETURN SCOPE_IDENTITY()