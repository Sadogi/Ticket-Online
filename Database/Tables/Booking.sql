CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL IDENTITY,
	UserId INT NOT NULL,
	EventId INT NOT NULL,
	PurchaseDate DATE NOT NULL,
	TicketsPurchased INT NOT NULL,
	Amount FLOAT NOT NULL,
	CONSTRAINT [PK_Booking] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_UserBooking] FOREIGN KEY (UserId) REFERENCES [User]([Id]),
	CONSTRAINT [FK_EventBooking] FOREIGN KEY (EventId) REFERENCES [Event]([Id])
)
