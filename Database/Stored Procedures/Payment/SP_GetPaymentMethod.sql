CREATE PROCEDURE [dbo].[SP_GetPaymentMethod]
--	@Id INT
--AS
--BEGIN
--	SELECT Id, UserId, CardHolder, CardNumber, ExpirationDate, CVVnumber
--	FROM PaymentMethod
--	WHERE Id = @Id;
--END

	@Id INT
AS
BEGIN
	SELECT pm.Id, pm.UserId, pm.CardHolder, pm.CardNumber, pm.ExpirationDate, pm.CVVnumber, u.ScreenName AS [User], u.Email
	FROM PaymentMethod pm
	LEFT JOIN [User] u ON pm.UserId = u.Id
	WHERE pm.Id = @Id;
END