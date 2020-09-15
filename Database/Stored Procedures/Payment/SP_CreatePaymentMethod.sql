CREATE PROCEDURE [dbo].[SP_CreatePaymentMethod]
	@UserId INT,
	@CardHolder NVARCHAR(75),
	@CardNumber NVARCHAR(19),
	@ExpirationDate DATE,
	@CVVnumber INT
AS
BEGIN
	INSERT INTO PaymentMethod (UserId, CardHolder, CardNumber, ExpirationDate, CVVnumber) 
	OUTPUT inserted.Id
	VALUES (@UserId, @CardHolder, @CardNumber, @ExpirationDate, @CVVnumber);
END