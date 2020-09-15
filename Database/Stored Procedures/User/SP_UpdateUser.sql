CREATE PROCEDURE [dbo].[SP_UpdateUser]
	@Id INT,
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@ScreenName NVARCHAR (20),
	@Email NVARCHAR(384),
	@Address NVARCHAR(100),
	@IsActive BIT,
	@IsAdmin BIT 
AS
BEGIN
	UPDATE [User]
	SET LastName = @LastName, FirstName = @FirstName, ScreenName = @ScreenName, Email = @Email, [Address] = @Address, 
		IsActive = @IsActive, IsAdmin = @IsAdmin
	WHERE Id = @Id;
END