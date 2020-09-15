CREATE TABLE [dbo].[User]
(
	Id INT NOT NULL IDENTITY,
	LastName NVARCHAR(75),
	FirstName NVARCHAR(75),
	ScreenName NVARCHAR(20) NOT NULL,
	Email NVARCHAR(384) NOT NULL,
	Passwd VARBINARY(64) NOT NULL,
	[Address] NVARCHAR(100),
	IsActive BIT NOT NULL 
		CONSTRAINT DF_User_IsActive DEFAULT (1),
	IsAdmin BIT NOT NULL 
		CONSTRAINT DF_User_IsAdmin DEFAULT (0),
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
	CONSTRAINT [UC_User_Email] Unique (Email),
	CONSTRAINT [UC_User_ScreenName] Unique (ScreenName)
)

GO

CREATE TRIGGER [dbo].[TR_InstedOfDeleteUser]
    ON [dbo].[User]
    INSTEAD OF DELETE
    AS
    BEGIN
        SET NoCount ON
		UPDATE [User] SET IsActive = 0 WHERE Id in (SELECT Id FROM deleted);
    END
