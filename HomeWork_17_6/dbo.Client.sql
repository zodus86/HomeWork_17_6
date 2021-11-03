CREATE TABLE [dbo].[Clients]
(
	[Id] INT  NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [LastName] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
	[MiddleName] VARCHAR(50) NOT NULL, 
	[TelephonNumber] DECIMAL NOT NULL, 
	[Email] VARCHAR(50) NOT NULL
)
