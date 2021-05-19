IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('UserInfo'))
BEGIN
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON

CREATE TABLE [dbo].[UserInfo](
	[UserNo] [varchar](30) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserClass] [nvarchar](50) NULL,
	[Tel] [varchar](20) NULL,
	[CreateDate] [datetime] DEFAULT GETDATE(),
	[ModifyDate] [datetime] NULL
PRIMARY KEY CLUSTERED 
(
	[UserNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END