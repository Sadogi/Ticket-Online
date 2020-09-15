CREATE PROCEDURE [dbo].[SP_CreateBooking]
	@UserId INT, 
	@EventId INT, 
	@PurchaseDate DATE, 
	@TicketsPurchased INT, 
	@Amount FLOAT
AS
BEGIN
	INSERT INTO Booking (UserId, EventId, PurchaseDate, TicketsPurchased, Amount) 
	OUTPUT inserted.Id 
	VALUES (@UserId, @EventId, @PurchaseDate, @TicketsPurchased, @Amount);
END
