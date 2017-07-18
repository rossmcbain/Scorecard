CREATE PROCEDURE [dbo].UserUpdateCommand
(
	@Username varchar(100),
	@FirstName varchar(100),
	@Surname varchar(100),
	@EmailAddress varchar(500),
	@UserLevelID int,
	@Original_UserID int,
	@UserID int
)
AS
	SET NOCOUNT OFF;
UPDATE [dbo].[User] SET [Username] = @Username, [FirstName] = @FirstName, [Surname] = @Surname, [EmailAddress] = @EmailAddress, [UserLevelID] = @UserLevelID WHERE (([UserID] = @Original_UserID));
	
SELECT UserID, Username, FirstName, Surname, EmailAddress, UserLevelID FROM [User] WHERE (UserID = @UserID)