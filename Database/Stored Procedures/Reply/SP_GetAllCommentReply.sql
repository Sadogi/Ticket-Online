CREATE PROCEDURE [dbo].[SP_GetAllCommentReply]
--	@CommentId INT
--AS
--BEGIN
--	SELECT Id, UserId, CommentId, [Date], Content
--	FROM Reply
--	WHERE CommentId = @CommentId
--END

	@CommentId INT
AS
BEGIN
SELECT r.Id, r.UserId, r.CommentId, r.[Date], r.Content, u.ScreenName AS [User], c.Content AS Comment
FROM [User] u RIGHT JOIN Reply r ON u.Id = r.UserId
LEFT JOIN Comment c ON r.CommentId = c.Id
WHERE r.CommentId = @CommentId;
END