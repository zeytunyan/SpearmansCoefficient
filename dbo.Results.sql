<<<<<<< HEAD
﻿CREATE TABLE [dbo].[Results] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (255)  NOT NULL,
    [Coeff] DECIMAL (18, 5) NOT NULL,
    [Concl] NVARCHAR (255)  NOT NULL,
    [Time]  DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

=======
﻿CREATE TABLE [dbo].[Results] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (255)  NOT NULL,
    [Coeff] DECIMAL (18, 5) NOT NULL,
    [Concl] NVARCHAR (255)  NOT NULL,
    [Time]  DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

>>>>>>> 2ff17273403c36b340e770197aef05c8910e6989
