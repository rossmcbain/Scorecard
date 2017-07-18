CREATE PROCEDURE [dbo].ScorecardItemGroupSelectCommand
AS
	SET NOCOUNT ON;
SELECT ScorecardItemGroupID, ScorecardID, GroupName, Description, PassScore FROM dbo.ScorecardItemGroup