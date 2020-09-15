CREATE PROCEDURE [dbo].[SP_GetAllPaymentMethod]
--	@UserId INT
--AS
--BEGIN
--	SELECT Id, UserId, CardHolder, CardNumber, ExpirationDate, CVVnumber
--	FROM PaymentMethod
--	WHERE UserId = @UserId;
--END

	@UserId INT
AS
BEGIN
	SELECT pm.Id, pm.UserId, pm.CardHolder, pm.CardNumber, pm.ExpirationDate, pm.CVVnumber, u.ScreenName AS [User], u.Email
	FROM PaymentMethod pm
	LEFT JOIN [User] u ON pm.UserId = u.Id
	WHERE pm.UserId = @UserId;
END