CREATE PROCEDURE [dbo].[SP_GetReply]
--	@Id INT
--AS
--BEGIN
--	SELECT Id, UserId, CommentId, [Date], Content
--	FROM Reply
--	WHERE Id = @Id;
--END

	@Id INT
AS
BEGIN
SELECT r.Id, r.UserId, r.CommentId, r.[Date], r.Content, u.ScreenName AS [User], c.Content AS Comment
FROM [User] u RIGHT JOIN Reply r ON u.Id = r.UserId
LEFT JOIN Comment c ON r.CommentId = c.Id
WHERE r.Id = @Id;
END