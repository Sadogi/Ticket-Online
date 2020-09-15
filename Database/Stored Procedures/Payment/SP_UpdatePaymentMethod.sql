CREATE PROCEDURE [dbo].[SP_UpdatePaymentMethod]
	@Id INT,
	@CardHolder NVARCHAR(75),
	@CardNumber NVARCHAR(19),
	@ExpirationDate DATE,
	@CVVnumber INT
AS
BEGIN
	UPDATE PaymentMethod
	SET CardHolder = @CardHolder, CardNumber = @CardNumber, ExpirationDate = @ExpirationDate, CVVnumber = @CVVnumber
	WHERE Id = @Id;
END