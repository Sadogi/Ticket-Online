CREATE PROCEDURE [dbo].[SP_GetUser]
	@Id INT
AS
BEGIN
	SELECT Id, LastName, FirstName, ScreenName, Email, [Address], IsActive, IsAdmin
	FROM [User]
	WHERE Id = @Id;
END