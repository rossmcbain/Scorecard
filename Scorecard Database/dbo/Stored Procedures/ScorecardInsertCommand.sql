CREATE PROCEDURE [dbo].ScorecardInsertCommand
(
	@ScorecardName varchar(100),
	@ScorecardDescription varchar(5000),
	@PassMark int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [dbo].[Scorecard] ([ScorecardName], [ScorecardDescription], [PassMark]) VALUES (@ScorecardName, @ScorecardDescription, @PassMark);
	
SELECT ScorecardID, ScorecardName, ScorecardDescription, PassMark FROM Scorecard WHERE (ScorecardID = SCOPE_IDENTITY())