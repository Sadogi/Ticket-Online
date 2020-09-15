CREATE PROCEDURE [dbo].[SP_CreateComment]
	@UserId INT,
	@EventId INT,
	@Date DATE,
	@Content NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO Comment (UserId, EventId, [Date], Content) 
	OUTPUT inserted.Id 
	VALUES (@UserId, @EventId, @Date, @Content);
END