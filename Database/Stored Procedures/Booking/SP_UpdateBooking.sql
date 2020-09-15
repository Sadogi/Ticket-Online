CREATE PROCEDURE [dbo].[SP_UpdateBooking]
	@Id INT,
	@PurchaseDate DATE, 
	@TicketsPurchased INT, 
	@Amount FLOAT
AS
BEGIN
	UPDATE Booking 
	SET PurchaseDate = @PurchaseDate, TicketsPurchased = @TicketsPurchased, Amount = @Amount
	WHERE Id = @Id;
END
