CREATE PROCEDURE [dbo].[SP_DeletePaymentMethod]
	@Id INT
AS
BEGIN
	DELETE FROM PaymentMethod 
	WHERE Id = @Id;
END