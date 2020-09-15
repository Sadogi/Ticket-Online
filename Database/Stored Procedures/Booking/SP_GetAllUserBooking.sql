CREATE PROCEDURE [dbo].[SP_GetAllUserBooking]
--	@UserId INT
--AS
--BEGIN
--	SELECT Id, UserId, EventId, PurchaseDate, TicketsPurchased, Amount
--	FROM Booking
--	WHERE UserId = @UserId;
--END

	@UserId INT
AS
BEGIN
	SELECT b.Id, b.UserId, b.EventId, b.PurchaseDate, b.TicketsPurchased, b.Amount, u.ScreenName AS [User], u.Email, e.[Name] AS [Event]
	FROM [User] u 
	RIGHT JOIN Booking b ON u.Id = b.UserId
	LEFT JOIN [Event] e ON b.EventId = e.Id
	WHERE b.UserId = @UserId;
END