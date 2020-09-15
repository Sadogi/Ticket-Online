CREATE PROCEDURE [dbo].[SP_UpdateComment]
	@Id INT,
	@Date DATE,
	@Content NVARCHAR(MAX)
AS
BEGIN
	UPDATE Comment 
	SET [Date] = @Date, Content = @Content 
	WHERE Id = @Id;
END