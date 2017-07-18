CREATE PROCEDURE [dbo].ScorecardItemGroupInsertCommand
(
	@ScorecardID int,
	@GroupName varchar(100),
	@Description varchar(5000),
	@PassScore int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[ScorecardItemGroup] ([ScorecardID], [GroupName], [Description], [PassScore]) VALUES (@ScorecardID, @GroupName, @Description, @PassScore);
	
SELECT ScorecardItemGroupID, ScorecardID, GroupName, Description, PassScore FROM ScorecardItemGroup WHERE (ScorecardItemGroupID = SCOPE_IDENTITY())