CREATE PROCEDURE [dbo].[SP_GetAllEventComment]
--	@EventId INT
--AS
--BEGIN
--	SELECT Id, UserId, EventId, [Date], Content
--	FROM Comment
--	WHERE EventId = @EventId;
--END

	@EventId INT
AS
BEGIN
	SELECT c.Id, c.UserId, c.EventId, c.[Date], c.Content, u.ScreenName AS [User], e.[Name] AS [Event]
	FROM [User] u 
	RIGHT JOIN Comment c ON u.Id = c.UserId
	LEFT JOIN [Event] e ON c.EventId = e.Id
	WHERE EventId = @EventId;
END