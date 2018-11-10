CREATE TABLE [dbo].[Journal] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Teacher] NVARCHAR (30) NOT NULL,
    [Subject] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
