CREATE TABLE [dbo].[Event]
(
	Id INT NOT NULL IDENTITY,
	[Name] NVARCHAR(75) NOT NULL,
	[Type] NVARCHAR(75) NOT NULL,
	Organizer NVARCHAR(75) NOT NULL,	
	[Date] DATE,
	[Location] NVARCHAR(75),
	Tickets INT,
	Price FLOAT,
	[Description] NVARCHAR(384),
	CONSTRAINT [PK_Event] PRIMARY KEY ([Id])
)
