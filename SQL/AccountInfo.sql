IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('AccountInfo'))
BEGIN
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON

CREATE TABLE [dbo].[AccountInfo](
	[UserNo] [varchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[UserPriority] tinyint DEFAULT(0),
	[Remark] [varchar](30) NULL,
	[CreateDate] [datetime] DEFAULT GETDATE(),
	[ModifyDate] [datetime] NULL
PRIMARY KEY CLUSTERED 
(
	[UserNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END