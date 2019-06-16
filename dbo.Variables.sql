CREATE TABLE [dbo].[Variables] (
    [Id]  INT IDENTITY (1, 1) NOT NULL,
    [RID] INT NOT NULL,
    [X]   INT NOT NULL,
    [Y]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([RID]) REFERENCES [dbo].[Results] ([Id])
);

