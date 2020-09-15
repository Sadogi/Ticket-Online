CREATE TABLE [dbo].[PaymentMethod]
(
	[Id] INT NOT NULL IDENTITY,
	UserId INT NOT NULL,
	CardHolder NVARCHAR(75) NOT NULL,
	CardNumber NVARCHAR(19) NOT NULL,
	ExpirationDate DATE NOT NULL,
	CVVnumber INT NOT NULL
		CONSTRAINT CHK_PaymentMethod_CVVnumber CHECK (CVVnumber BETWEEN 0 AND 999),
	CONSTRAINT [PK_PaymentMethod] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PaymentMethod_User] FOREIGN KEY (UserId) REFERENCES [User]([Id])
)
