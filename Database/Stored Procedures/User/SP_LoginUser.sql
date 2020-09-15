CREATE PROCEDURE [dbo].[SP_LoginUser]
	@Email nvarchar(384),
	@Passwd nvarchar(20)
AS
Begin
	Select Id, ScreenName, Email 
	From [User]
	Where Email = @Email And Passwd = HASHBYTES('SHA2_512', [dbo].[GetPreSalt]() + @Passwd + [dbo].[GetPostSalt]())
End
