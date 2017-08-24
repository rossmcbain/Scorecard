CREATE PROCEDURE [dbo].ScorecardItemGroupSelectCommand
@ScorecardID int = null
AS
	SET NOCOUNT ON;
SELECT ScorecardItemGroupID, ScorecardID, GroupName, Description, PassScore FROM dbo.ScorecardItemGroup
where (@ScorecardID is null or ScorecardID = @ScorecardID)