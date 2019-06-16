CREATE TABLE [dbo].[Results] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (255)  NOT NULL,
    [Coeff] DECIMAL (18, 5) NOT NULL,
    [Concl] NVARCHAR (255)  NOT NULL,
    [Time]  DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

