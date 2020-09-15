CREATE PROCEDURE [dbo].[SP_UpdateReply]
	@Id INT,
	@Date DATE,
	@Content NVARCHAR
AS
BEGIN
	UPDATE Reply 
	SET [Date] = @Date, Content = @Content
	WHERE Id = @Id;
END