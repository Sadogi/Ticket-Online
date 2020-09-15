CREATE PROCEDURE [dbo].[SP_UpdateEvent]
	@Id INT,
	@Name NVARCHAR(75),
	@Type NVARCHAR(75),
	@Organizer NVARCHAR(75),	
	@Date DATE,
	@Location NVARCHAR(75),
	@Tickets INT,
	@Price FLOAT,
	@Description NVARCHAR(384)
AS
BEGIN
	UPDATE [Event] 
	SET [Name] = @Name, [Type] = @Type, Organizer = @Organizer, [Date] = @Date, [Location] = @Location, 
		Tickets = @Tickets, Price = @Price, [Description] = @Description
	WHERE Id = @Id;
END
