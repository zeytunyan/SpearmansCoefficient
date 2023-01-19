<<<<<<< HEAD
﻿CREATE TABLE [dbo].[Variables] (
    [Id]  INT IDENTITY (1, 1) NOT NULL,
    [RID] INT NOT NULL,
    [X]   INT NOT NULL,
    [Y]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([RID]) REFERENCES [dbo].[Results] ([Id])
);

=======
﻿CREATE TABLE [dbo].[Variables] (
    [Id]  INT IDENTITY (1, 1) NOT NULL,
    [RID] INT NOT NULL,
    [X]   INT NOT NULL,
    [Y]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([RID]) REFERENCES [dbo].[Results] ([Id])
);

>>>>>>> 2ff17273403c36b340e770197aef05c8910e6989
