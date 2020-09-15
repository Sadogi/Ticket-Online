CREATE PROCEDURE [dbo].[SP_GetAllUserComment]
--	@UserId INT
--AS
--BEGIN
--	SELECT Id, UserId, EventId, [Date], Content
--	FROM Comment
--	WHERE UserId = @UserId;
--END

	@UserId INT
AS
BEGIN
	SELECT c.Id, c.UserId, c.EventId, c.[Date], c.Content, u.ScreenName AS [User], e.[Name] AS [Event]
	FROM [User] u 
	RIGHT JOIN Comment c ON u.Id = c.UserId
	LEFT JOIN [Event] e ON c.EventId = e.Id
	WHERE UserId = @UserId;
END