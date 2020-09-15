CREATE PROCEDURE [dbo].[SP_UpdatePasswd]
	@Id INT,
	@Passwd NVARCHAR(20),
	@NewPasswd NVARCHAR(20)
AS
BEGIN
	UPDATE [User]
	SET Passwd = HASHBYTES('SHA2_512', [dbo].[GetPreSalt]() + @NewPasswd + [dbo].[GetPostSalt]())
	WHERE Id = @Id AND Passwd = HASHBYTES('SHA2_512', [dbo].[GetPreSalt]() + @Passwd + [dbo].[GetPostSalt]());
END
