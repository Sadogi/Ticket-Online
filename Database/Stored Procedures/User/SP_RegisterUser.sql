CREATE PROCEDURE [dbo].[SP_RegisterUser]
	@ScreenName NVARCHAR(20),
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	Insert into [User] (ScreenName, Email, Passwd)
	OUTPUT inserted.Id
	values (@ScreenName, @Email, HASHBYTES('SHA2_512',[dbo].[GetPreSalt]() + @Passwd + [dbo].[GetPostSalt]()));
END
