CREATE PROCEDURE [dbo].[SP_GetAllUserReply]
--	@UserId INT
--AS
--BEGIN
--	SELECT Id, UserId, CommentId, [Date], Content
--	FROM Reply
--	WHERE UserId = @UserId
--END

	@UserId INT
AS
BEGIN
SELECT r.Id, r.UserId, r.CommentId, r.[Date], r.Content, u.ScreenName AS [User], c.Content AS Comment
FROM [User] u RIGHT JOIN Reply r ON u.Id = r.UserId
LEFT JOIN Comment c ON r.CommentId = c.Id
WHERE r.UserId = @UserId;
END