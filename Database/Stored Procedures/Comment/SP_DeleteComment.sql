﻿CREATE PROCEDURE [dbo].[SP_DeleteComment]
	@Id INT
AS
BEGIN
	DELETE FROM Comment 
	WHERE Id = @Id
END