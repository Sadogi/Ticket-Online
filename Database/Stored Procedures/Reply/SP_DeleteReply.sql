﻿CREATE PROCEDURE [dbo].[SP_DeleteReply]
	@Id INT
AS
BEGIN
	DELETE FROM Reply
	WHERE Id = @Id;
END