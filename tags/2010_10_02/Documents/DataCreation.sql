USE [RIS]
GO
/****** Object:  Table [dbo].[tUserRoles]    Script Date: 09/08/2007 23:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tUsers]    Script Date: 09/08/2007 23:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [varchar](10) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [char](1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy (datetime, not null)] [datetime] NOT NULL,
	[LastUpdateDate] [int] NOT NULL,
 CONSTRAINT [PK_tUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tPatients]    Script Date: 09/08/2007 23:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tPatients](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[ExternalPatientId] [varchar](64) NOT NULL,
	[Name] [varchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [char](1) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tPatients] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tScreens]    Script Date: 09/08/2007 23:15:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tScreens](
	[ScreenId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Path] [varchar](200) NOT NULL,
	[IsActive] [char](1) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tScreens] PRIMARY KEY CLUSTERED 
(
	[ScreenId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tFindings]    Script Date: 09/08/2007 23:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tFindings](
	[FindingId] [int] IDENTITY(1,1) NOT NULL,
	[StudyId] [int] NOT NULL,
	[AudioPath] [varchar](100) NULL,
	[Text] [varchar](1024) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tFindings] PRIMARY KEY CLUSTERED 
(
	[FindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tRelatedUsers]    Script Date: 09/08/2007 23:15:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRelatedUsers](
	[RelationId] [int] IDENTITY(1,1) NOT NULL,
	[SupervisorUserId] [int] NOT NULL,
	[SubordinateUserId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tRelatedUsers] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tStudies]    Script Date: 09/08/2007 23:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tStudies](
	[StudyId] [int] IDENTITY(1,1) NOT NULL,
	[StudyInstance] [varchar](64) NOT NULL,
	[StudyDate] [datetime] NULL,
	[Description] [varchar](64) NULL,
	[ReferringPhysician] [varchar](64) NULL,
	[PatientId] [int] NULL,
	[PatientWeight] [float] NULL,
	[StudyModal] [varchar](64) NULL,
	[StationName] [varchar](16) NULL,
	[Instituition] [varchar](64) NULL,
	[StudyStatusId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tStudies] PRIMARY KEY CLUSTERED 
(
	[StudyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tTypes]    Script Date: 09/08/2007 23:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tTypes](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[Group] [varchar](30) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tTypes] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tSeries]    Script Date: 09/08/2007 23:16:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tSeries](
	[SeriesId] [int] IDENTITY(1,1) NOT NULL,
	[StudyId] [int] NOT NULL,
	[SeriesInstance] [varchar](64) NOT NULL,
	[SeriesNumber] [varchar](12) NULL,
	[SeriesDate] [datetime] NULL,
	[Description] [varchar](64) NULL,
	[ModalityId] [int] NULL,
	[PatientPosition] [varchar](16) NULL,
	[Contrast] [varchar](64) NULL,
	[BodyPartExamined] [varchar](64) NULL,
	[ProtocolName] [varchar](64) NULL,
	[FrameOfReference] [varchar](64) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tImages]    Script Date: 09/08/2007 23:14:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tImages](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[SeriesId] [int] NOT NULL,
	[ImageInstance] [varchar](64) NOT NULL,
	[ImageClassUI] [varchar](64) NULL,
	[ImageNumber] [int] NULL,
	[ImageDate] [datetime] NULL,
	[EchoNumber] [int] NULL,
	[NumberOfFrames] [int] NULL,
	[AcquiredDate] [datetime] NULL,
	[SliceLocation] [varchar](16) NULL,
	[NumberOfSamples] [int] NULL,
	[PhotoMetric] [varchar](64) NULL,
	[Rows] [int] NULL,
	[Columns] [int] NULL,
	[BitsStored] [int] NULL,
	[Path] [varchar](256) NULL,
	[DeviceName] [varchar](32) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tImages] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tGroups]    Script Date: 09/08/2007 23:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tGroups](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tUserGroups]    Script Date: 09/08/2007 23:17:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUserGroups](
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tUserGroups] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tGroupRelations]    Script Date: 09/08/2007 23:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tGroupRelations](
	[FirstGroupId] [int] NOT NULL,
	[SecondGroupId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tGroupRelations] PRIMARY KEY CLUSTERED 
(
	[FirstGroupId] ASC,
	[SecondGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tModalities]    Script Date: 09/08/2007 23:14:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tModalities](
	[ModalityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](16) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tModalityTypes] PRIMARY KEY CLUSTERED 
(
	[ModalityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tModalityDetails]    Script Date: 09/08/2007 23:14:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tModalityDetails](
	[ModalityDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ModalityId] [int] NOT NULL,
	[Manufacturer] [varchar](64) NULL,
	[ModelName] [varchar](64) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
	[LastUpdatedby] [int] NOT NULL,
 CONSTRAINT [PK_tModalities] PRIMARY KEY CLUSTERED 
(
	[ModalityDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tModalityCategories]    Script Date: 09/08/2007 23:14:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tModalityCategories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ModalityId] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdateBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tModalityCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tProcedures]    Script Date: 09/08/2007 23:15:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tProcedures](
	[ProcedureId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[CPTCode] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tProcedures] PRIMARY KEY CLUSTERED 
(
	[ProcedureId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tRoles]    Script Date: 09/08/2007 23:15:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdatedBy] [int] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
