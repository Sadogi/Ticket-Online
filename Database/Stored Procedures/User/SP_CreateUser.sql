CREATE PROCEDURE [dbo].[SP_CreateUser]
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@ScreenName NVARCHAR(20), 
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20),
	@Address NVARCHAR(100)
	
AS
BEGIN
	Insert into [User] (LastName, FirstName, ScreenName, Email, Passwd, [Address])
	values (@LastName, @FirstName, @ScreenName, @Email, HASHBYTES('SHA2_512',[dbo].[GetPreSalt]() + @Passwd + [dbo].[GetPostSalt]()), @Address);
END