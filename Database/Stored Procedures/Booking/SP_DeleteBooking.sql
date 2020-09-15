CREATE PROCEDURE [dbo].[SP_DeleteBooking]
	@Id INT
AS
BEGIN
	DELETE FROM Booking 
	WHERE Id = @Id; 
END