
CREATE TABLE [dbo].[ElmahSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[LengthToKeepResults] [int] NOT NULL
 CONSTRAINT [PK_ElmahSettings] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
CONSTRAINT [FK_ElmahSettings_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id),
) ON [PRIMARY]

GO