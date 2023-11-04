DROP TABLE [IBGE]

CREATE TABLE [IBGE]
(    
    [Id] CHAR(7) NOT NULL,    
    [State] CHAR(2) NULL,    
	[City] NVARCHAR(80) NULL,
    CONSTRAINT [PK_IBGE] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_IBGE_Id] ON [IBGE] ([Id]);
GO

CREATE INDEX [IX_IBGE_City] ON [IBGE] ([City]);
GO

CREATE INDEX [IX_IBGE_State] ON [IBGE] ([State]);
GO

GO
CREATE TABLE [dbo].[User](
    [UserId] [uniqueidentifier] NOT NULL,
    [Username] [nvarchar](255) NOT NULL,
    [Password] [nvarchar](255) NOT NULL,
    [Email] [nvarchar](255) NOT NULL,
    [Role] [nvarchar](255) NOT NULL,
    [CreatedAt] [datetime] NOT NULL,
    [LastUpdated] [datetime] NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UQ_User_Username] UNIQUE NONCLUSTERED ([Username] ASC),
    CONSTRAINT [UQ_User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
)
GO

GO
CREATE NONCLUSTERED INDEX IDX_User_Role ON [dbo].[User] (Role);
GO