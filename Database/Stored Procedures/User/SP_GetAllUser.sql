CREATE PROCEDURE [dbo].[SP_GetAllUser]
AS
BEGIN
	SELECT Id, LastName, FirstName, ScreenName, Email, [Address], IsActive, IsAdmin
	FROM [User]
END