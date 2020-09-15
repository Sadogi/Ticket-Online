CREATE PROCEDURE [dbo].[SP_CreateEvent]
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
	INSERT INTO [Event]([Name], [Type], Organizer, [Date], [Location], Tickets, Price, [Description])
	OUTPUT inserted.Id 
	VALUES(@Name, @Type, @Organizer, @Date, @Location, @Tickets, @Price, @Description); 
END