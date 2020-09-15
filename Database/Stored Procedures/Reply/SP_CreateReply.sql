CREATE PROCEDURE [dbo].[SP_CreateReply]
	@UserId INT,
	@CommentId INT,
	@Date DATE,
	@Content NVARCHAR
AS
BEGIN
	INSERT INTO Reply (UserId, CommentId, Date, Content) 
	OUTPUT inserted.Id 
	VALUES (@UserId, @CommentId, @Date, @Content);
END