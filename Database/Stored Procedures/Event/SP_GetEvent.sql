CREATE PROCEDURE [dbo].[SP_GetEvent]
	@Id INT
AS
BEGIN
	SELECT Id,[Name], [Type], Organizer, [Date], [Location], Tickets, Price, [Description]
	FROM [Event]
	WHERE Id = @Id;
END