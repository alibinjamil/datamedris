--
-- Script To Update dbo.tFindings Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tFindings Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tFindings Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tFindings Table'
END
GO


--
-- Script To Update dbo.tGroupRelations Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tGroupRelations Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tGroupRelations Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tGroupRelations Table'
END
GO


--
-- Script To Update dbo.tImages Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tImages Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tImages Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tImages Table'
END
GO


--
-- Script To Update dbo.tLog Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tLog Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tLog Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tLog Table'
END
GO


--
-- Script To Update dbo.tModalityDetails Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tModalityDetails Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tModalityDetails Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tModalityDetails Table'
END
GO


--
-- Script To Update dbo.tProcedures Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tProcedures Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tProcedures Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tProcedures Table'
END
GO


--
-- Script To Update dbo.tSeries Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tSeries Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[tSeries] ADD CONSTRAINT [PK_tSeries] PRIMARY KEY CLUSTERED ([SeriesId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tSeries Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tSeries Table'
END
GO


--
-- Script To Update dbo.tStations Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStations Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStations Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStations Table'
END
GO


--
-- Script To Update dbo.tStudies Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStudies Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStudies Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStudies Table'
END
GO


--
-- Script To Update dbo.tStudyGroups Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStudyGroups Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStudyGroups Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStudyGroups Table'
END
GO


--
-- Script To Update dbo.tUserGroups Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tUserGroups Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tUserGroups Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tUserGroups Table'
END
GO


--
-- Script To Update dbo.tUserRoles Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tUserRoles Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tUserRoles Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tUserRoles Table'
END
GO


--
-- Script To Update dbo.tUsers Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tUsers Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[tUsers]
      ALTER COLUMN [IsActive] [bit] NOT NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tUsers Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tUsers Table'
END
GO

--
-- Script To Create dbo.GetActiveUsersByGroupAndRole Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GetActiveUsersByGroupAndRole Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[GetActiveUsersByGroupAndRole]
(
	@GroupId int,
	@RoleId int
)
AS
	SET NOCOUNT ON;
SELECT        tUsers.*
FROM            tUsers
INNER JOIN tUserGroups ON tUsers.UserId = tUserGroups.UserId
INNER JOIN tUserRoles ON tUsers.UserId = tUserRoles.UserId
WHERE tUserGroups.GroupId = @GroupId
AND tUserRoles.RoleId = @RoleId
AND tUsers.IsActive = 1;')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GetActiveUsersByGroupAndRole Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GetActiveUsersByGroupAndRole Procedure'
END
GO

--
-- Script To Create dbo.GetActiveUsersByRole Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GetActiveUsersByRole Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[GetActiveUsersByRole]
(
	@RoleId int
)
AS
	SET NOCOUNT ON;
SELECT        tUsers.*
FROM            tUsers
INNER JOIN tUserRoles ON tUsers.UserId = tUserRoles.UserId
WHERE tUserRoles.RoleId = @RoleId
AND tUsers.IsActive = 1;')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GetActiveUsersByRole Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GetActiveUsersByRole Procedure'
END
GO

--
-- Script To Create dbo.GetProcedure Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GetProcedure Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.GetProcedure
(
	@ProcedureId int
)
AS
	SET NOCOUNT ON;
SELECT        tProcedures.ProcedureId, tProcedures.Name AS ProcedureName, tProcedures.CPTCode, tProcedures.CreatedBy, tProcedures.CreationDate, 
                         tProcedures.LastUpdatedBy, tProcedures.LastUpdateDate, tModalities.ModalityId, tModalities.Name AS ModalityName
FROM            tProcedures INNER JOIN
                         tModalities ON tProcedures.ModalityId = tModalities.ModalityId
WHERE tProcedures.ProcedureId = @ProcedureId')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GetProcedure Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GetProcedure Procedure'
END
GO

--
-- Script To Create dbo.GetProceduresByModality Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GetProceduresByModality Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[GetProceduresByModality]
(
	@ModalityId int
)
AS
	SET NOCOUNT ON;
SELECT        tProcedures.ProcedureId, tProcedures.Name AS ProcedureName, tProcedures.CPTCode, tProcedures.CreatedBy, tProcedures.CreationDate, 
                         tProcedures.LastUpdatedBy, tProcedures.LastUpdateDate, tModalities.ModalityId, tModalities.Name AS ModalityName
FROM            tProcedures INNER JOIN
                         tModalities ON tProcedures.ModalityId = tModalities.ModalityId
WHERE tModalities.ModalityId = @ModalityId;')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GetProceduresByModality Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GetProceduresByModality Procedure'
END
GO

--
-- Script To Create dbo.GroupsDeleteCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GroupsDeleteCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.GroupsDeleteCommand
(
	@Original_GroupId int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [tGroups] WHERE (([GroupId] = @Original_GroupId))')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GroupsDeleteCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GroupsDeleteCommand Procedure'
END
GO

--
-- Script To Create dbo.GroupsInsertCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GroupsInsertCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.GroupsInsertCommand
(
	@Name varchar(50),
	@IsDefault char(1),
	@Description varchar(100),
	@CreatedBy int,
	@CreationDate datetime,
	@LastUpdatedBy int,
	@LastUpdateDate datetime
)
AS
	SET NOCOUNT OFF;
INSERT INTO [tGroups] ([Name], [IsDefault], [Description], [CreatedBy], [CreationDate], [LastUpdatedBy], [LastUpdateDate]) VALUES (@Name, @IsDefault, @Description, @CreatedBy, @CreationDate, @LastUpdatedBy, @LastUpdateDate);
	
SELECT GroupId, Name, IsDefault, Description, CreatedBy, CreationDate, LastUpdatedBy, LastUpdateDate FROM tGroups WHERE (GroupId = SCOPE_IDENTITY())')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GroupsInsertCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GroupsInsertCommand Procedure'
END
GO

--
-- Script To Create dbo.GroupsSelectCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GroupsSelectCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.GroupsSelectCommand
AS
	SET NOCOUNT ON;
SELECT        tGroups.*
FROM            tGroups')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GroupsSelectCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GroupsSelectCommand Procedure'
END
GO

--
-- Script To Create dbo.GroupsUpdateCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.GroupsUpdateCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.GroupsUpdateCommand
(
	@Name varchar(50),
	@IsDefault char(1),
	@Description varchar(100),
	@CreatedBy int,
	@CreationDate datetime,
	@LastUpdatedBy int,
	@LastUpdateDate datetime,
	@Original_GroupId int,
	@GroupId int
)
AS
	SET NOCOUNT OFF;
UPDATE [tGroups] SET [Name] = @Name, [IsDefault] = @IsDefault, [Description] = @Description, [CreatedBy] = @CreatedBy, [CreationDate] = @CreationDate, [LastUpdatedBy] = @LastUpdatedBy, [LastUpdateDate] = @LastUpdateDate WHERE (([GroupId] = @Original_GroupId));
	
SELECT GroupId, Name, IsDefault, Description, CreatedBy, CreationDate, LastUpdatedBy, LastUpdateDate FROM tGroups WHERE (GroupId = @GroupId)')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.GroupsUpdateCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.GroupsUpdateCommand Procedure'
END
GO

--
-- Script To Create dbo.InsertStudyDefaultGroup Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.InsertStudyDefaultGroup Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE procedure [dbo].[InsertStudyDefaultGroup](@studyId int,@userId int)
AS
insert
  into tStudyGroups(StudyId,GroupId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
select @studyId,GroupId,@userId,getDate(),@userId,getDate()
  from tGroups
 where IsDefault = ''Y'';')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.InsertStudyDefaultGroup Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.InsertStudyDefaultGroup Procedure'
END
GO

--
-- Script To Create dbo.ModalitiesDeleteCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ModalitiesDeleteCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.ModalitiesDeleteCommand
(
	@Original_ModalityId int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [tModalities] WHERE (([ModalityId] = @Original_ModalityId))')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ModalitiesDeleteCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ModalitiesDeleteCommand Procedure'
END
GO

--
-- Script To Create dbo.ModalitiesInsertCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ModalitiesInsertCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[ModalitiesInsertCommand]
(
	@Name varchar(64),
	@CreatedBy int
)
AS
	SET NOCOUNT OFF;
INSERT INTO [tModalities] ([Name], [CreatedBy], [CreationDate], [LastUpdatedBy], [LastUpdateDate]) VALUES (@Name, @CreatedBy, GETDATE(), @CreatedBy, GETDATE());
	
SELECT ModalityId, Name, CreatedBy, CreationDate, LastUpdatedBy, LastUpdateDate FROM tModalities WHERE (ModalityId = SCOPE_IDENTITY());')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ModalitiesInsertCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ModalitiesInsertCommand Procedure'
END
GO

--
-- Script To Create dbo.ModalitiesSelectCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ModalitiesSelectCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.ModalitiesSelectCommand
AS
	SET NOCOUNT ON;
SELECT        tModalities.*
FROM            tModalities')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ModalitiesSelectCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ModalitiesSelectCommand Procedure'
END
GO

--
-- Script To Create dbo.ModalitiesUpdateCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ModalitiesUpdateCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[ModalitiesUpdateCommand]
(
	@Name varchar(64),
	@LastUpdatedBy int,
	@Original_ModalityId int,
	@ModalityId int
)
AS
	SET NOCOUNT OFF;
UPDATE [tModalities] SET [Name] = @Name, [LastUpdatedBy] = @LastUpdatedBy, [LastUpdateDate] = GETDATE() WHERE (([ModalityId] = @Original_ModalityId));
	
SELECT ModalityId, Name, CreatedBy, CreationDate, LastUpdatedBy, LastUpdateDate FROM tModalities WHERE (ModalityId = @ModalityId)')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ModalitiesUpdateCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ModalitiesUpdateCommand Procedure'
END
GO

--
-- Script To Create dbo.ProcedureDeleteCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ProcedureDeleteCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.ProcedureDeleteCommand
(
	@original_ProcedureId int
)
AS
	SET NOCOUNT OFF;
DELETE FROM tProcedures WHERE ProcedureId = @original_ProcedureId')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ProcedureDeleteCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ProcedureDeleteCommand Procedure'
END
GO

--
-- Script To Create dbo.ProcedureInsertCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ProcedureInsertCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.ProcedureInsertCommand
(
	@ModalityId int,
	@Name varchar(100),
	@CPTCode varchar(50),
	@CreatedBy int
)
AS
	SET NOCOUNT OFF;
INSERT INTO tProcedures(ModalityId,Name,CPTCode,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
VALUES(@ModalityId,@Name,@CPTCode,@CreatedBy,GETDATE(),@CreatedBy,GETDATE())')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ProcedureInsertCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ProcedureInsertCommand Procedure'
END
GO

--
-- Script To Create dbo.ProcedureSelectCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ProcedureSelectCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[ProcedureSelectCommand]
AS
	SET NOCOUNT ON;
SELECT        tProcedures.ProcedureId, tProcedures.Name AS ProcedureName, tProcedures.CPTCode, tProcedures.CreatedBy, tProcedures.CreationDate, 
                         tProcedures.LastUpdatedBy, tProcedures.LastUpdateDate, tModalities.ModalityId, tModalities.Name AS ModalityName
FROM            tProcedures INNER JOIN
                         tModalities ON tProcedures.ModalityId = tModalities.ModalityId')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ProcedureSelectCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ProcedureSelectCommand Procedure'
END
GO

--
-- Script To Create dbo.ProcedureUpdateCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.ProcedureUpdateCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[ProcedureUpdateCommand]
(
	@Name varchar(100),
	@ModalityId int,
	@CPTCode varchar(50),
	@LastUpdatedBy int,
	@original_ProcedureId int
)
AS
	SET NOCOUNT OFF;
UPDATE tProcedures 
        SET Name = @Name,
               ModalityId = @ModalityId,
               CPTCode = @CPTCode,
               LastUpdatedBy = @LastUpdatedBy,
               LastUpdateDate = GETDATE()
WHERE ProcedureId = @original_ProcedureId')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.ProcedureUpdateCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.ProcedureUpdateCommand Procedure'
END
GO

--
-- Script To Create dbo.UsersDeleteCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.UsersDeleteCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.UsersDeleteCommand
(
	@Original_UserId int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [tUsers] WHERE (([UserId] = @Original_UserId))')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.UsersDeleteCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.UsersDeleteCommand Procedure'
END
GO

--
-- Script To Create dbo.UsersInsertCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.UsersInsertCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.UsersInsertCommand
(
	@LoginName varchar(20),
	@Password varchar(20),
	@Name varchar(50),
	@IsActive char(1),
	@CreationDate datetime,
	@CreatedBy int,
	@LastUpdatedBy int,
	@LastUpdateDate datetime,
	@LastLoginDate datetime
)
AS
	SET NOCOUNT OFF;
INSERT INTO [tUsers] ([LoginName], [Password], [Name], [IsActive], [CreationDate], [CreatedBy], [LastUpdatedBy], [LastUpdateDate], [LastLoginDate]) VALUES (@LoginName, @Password, @Name, @IsActive, @CreationDate, @CreatedBy, @LastUpdatedBy, @LastUpdateDate, @LastLoginDate);
	
SELECT UserId, LoginName, Password, Name, IsActive, CreationDate, CreatedBy, LastUpdatedBy, LastUpdateDate, LastLoginDate FROM tUsers WHERE (UserId = SCOPE_IDENTITY())')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.UsersInsertCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.UsersInsertCommand Procedure'
END
GO

--
-- Script To Create dbo.UsersSelectCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.UsersSelectCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.UsersSelectCommand
AS
	SET NOCOUNT ON;
SELECT        tUsers.*
FROM            tUsers')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.UsersSelectCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.UsersSelectCommand Procedure'
END
GO

--
-- Script To Create dbo.UsersUpdateCommand Procedure In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.UsersUpdateCommand Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.UsersUpdateCommand
(
	@LoginName varchar(20),
	@Password varchar(20),
	@Name varchar(50),
	@IsActive char(1),
	@CreationDate datetime,
	@CreatedBy int,
	@LastUpdatedBy int,
	@LastUpdateDate datetime,
	@LastLoginDate datetime,
	@Original_UserId int,
	@UserId int
)
AS
	SET NOCOUNT OFF;
UPDATE [tUsers] SET [LoginName] = @LoginName, [Password] = @Password, [Name] = @Name, [IsActive] = @IsActive, [CreationDate] = @CreationDate, [CreatedBy] = @CreatedBy, [LastUpdatedBy] = @LastUpdatedBy, [LastUpdateDate] = @LastUpdateDate, [LastLoginDate] = @LastLoginDate WHERE (([UserId] = @Original_UserId));
	
SELECT UserId, LoginName, Password, Name, IsActive, CreationDate, CreatedBy, LastUpdatedBy, LastUpdateDate, LastLoginDate FROM tUsers WHERE (UserId = @UserId)')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.UsersUpdateCommand Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.UsersUpdateCommand Procedure'
END
GO

--
-- Script To Update dbo.tFindings Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tFindings Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tStudies')
      ALTER TABLE [dbo].[tFindings] DROP CONSTRAINT [FK_tFindings_tStudies]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tStudies')
      ALTER TABLE [dbo].[tFindings] ADD CONSTRAINT [FK_tFindings_tStudies] FOREIGN KEY ([StudyId]) REFERENCES [dbo].[tStudies] ([StudyId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tUsers_Rad')
      ALTER TABLE [dbo].[tFindings] DROP CONSTRAINT [FK_tFindings_tUsers_Rad]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tUsers_Rad')
      ALTER TABLE [dbo].[tFindings] ADD CONSTRAINT [FK_tFindings_tUsers_Rad] FOREIGN KEY ([AudioUserId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tUsers_Tran')
      ALTER TABLE [dbo].[tFindings] DROP CONSTRAINT [FK_tFindings_tUsers_Tran]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tFindings_tUsers_Tran')
      ALTER TABLE [dbo].[tFindings] ADD CONSTRAINT [FK_tFindings_tUsers_Tran] FOREIGN KEY ([TranscriptUserId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tFindings Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tFindings Table'
END
GO


--
-- Script To Update dbo.tGroupRelations Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tGroupRelations Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tGroupRelations_tGroups')
      ALTER TABLE [dbo].[tGroupRelations] DROP CONSTRAINT [FK_tGroupRelations_tGroups]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tGroupRelations_tGroups')
      ALTER TABLE [dbo].[tGroupRelations] ADD CONSTRAINT [FK_tGroupRelations_tGroups] FOREIGN KEY ([FirstGroupId]) REFERENCES [dbo].[tGroups] ([GroupId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tGroupRelations_tGroups1')
      ALTER TABLE [dbo].[tGroupRelations] DROP CONSTRAINT [FK_tGroupRelations_tGroups1]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tGroupRelations_tGroups1')
      ALTER TABLE [dbo].[tGroupRelations] ADD CONSTRAINT [FK_tGroupRelations_tGroups1] FOREIGN KEY ([SecondGroupId]) REFERENCES [dbo].[tGroups] ([GroupId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tGroupRelations Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tGroupRelations Table'
END
GO


--
-- Script To Update dbo.tImages Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tImages Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tImages_tSeries')
      ALTER TABLE [dbo].[tImages] DROP CONSTRAINT [FK_tImages_tSeries]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tImages_tSeries')
      ALTER TABLE [dbo].[tImages] ADD CONSTRAINT [FK_tImages_tSeries] FOREIGN KEY ([SeriesId]) REFERENCES [dbo].[tSeries] ([SeriesId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tImages Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tImages Table'
END
GO


--
-- Script To Update dbo.tLog Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tLog Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tPatients')
      ALTER TABLE [dbo].[tLog] DROP CONSTRAINT [FK_tLog_tPatients]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tPatients')
      ALTER TABLE [dbo].[tLog] ADD CONSTRAINT [FK_tLog_tPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[tPatients] ([PatientId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tStudies')
      ALTER TABLE [dbo].[tLog] DROP CONSTRAINT [FK_tLog_tStudies]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tStudies')
      ALTER TABLE [dbo].[tLog] ADD CONSTRAINT [FK_tLog_tStudies] FOREIGN KEY ([StudyId]) REFERENCES [dbo].[tStudies] ([StudyId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tUsers')
      ALTER TABLE [dbo].[tLog] DROP CONSTRAINT [FK_tLog_tUsers]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tLog_tUsers')
      ALTER TABLE [dbo].[tLog] ADD CONSTRAINT [FK_tLog_tUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tLog Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tLog Table'
END
GO


--
-- Script To Update dbo.tModalityDetails Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tModalityDetails Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tModalityDetails_tModalities')
      ALTER TABLE [dbo].[tModalityDetails] DROP CONSTRAINT [FK_tModalityDetails_tModalities]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tModalityDetails_tModalities')
      ALTER TABLE [dbo].[tModalityDetails] ADD CONSTRAINT [FK_tModalityDetails_tModalities] FOREIGN KEY ([ModalityId]) REFERENCES [dbo].[tModalities] ([ModalityId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tModalityDetails Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tModalityDetails Table'
END
GO


--
-- Script To Update dbo.tProcedures Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tProcedures Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tProcedures_tModalities')
      ALTER TABLE [dbo].[tProcedures] DROP CONSTRAINT [FK_tProcedures_tModalities]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tProcedures_tModalities')
      ALTER TABLE [dbo].[tProcedures] ADD CONSTRAINT [FK_tProcedures_tModalities] FOREIGN KEY ([ModalityId]) REFERENCES [dbo].[tModalities] ([ModalityId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tProcedures Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tProcedures Table'
END
GO


--
-- Script To Update dbo.tSeries Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tSeries Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tSeries_tStudies')
      ALTER TABLE [dbo].[tSeries] DROP CONSTRAINT [FK_tSeries_tStudies]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tSeries_tStudies')
      ALTER TABLE [dbo].[tSeries] ADD CONSTRAINT [FK_tSeries_tStudies] FOREIGN KEY ([StudyId]) REFERENCES [dbo].[tStudies] ([StudyId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tSeries Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tSeries Table'
END
GO


--
-- Script To Update dbo.tStations Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStations Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStations_tModalities')
      ALTER TABLE [dbo].[tStations] DROP CONSTRAINT [FK_tStations_tModalities]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStations_tModalities')
      ALTER TABLE [dbo].[tStations] ADD CONSTRAINT [FK_tStations_tModalities] FOREIGN KEY ([ModalityId]) REFERENCES [dbo].[tModalities] ([ModalityId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStations Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStations Table'
END
GO


--
-- Script To Update dbo.tStudies Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStudies Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tModalities')
      ALTER TABLE [dbo].[tStudies] DROP CONSTRAINT [FK_tStudies_tModalities]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tModalities')
      ALTER TABLE [dbo].[tStudies] ADD CONSTRAINT [FK_tStudies_tModalities] FOREIGN KEY ([ModalityId]) REFERENCES [dbo].[tModalities] ([ModalityId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tPatients')
      ALTER TABLE [dbo].[tStudies] DROP CONSTRAINT [FK_tStudies_tPatients]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tPatients')
      ALTER TABLE [dbo].[tStudies] ADD CONSTRAINT [FK_tStudies_tPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[tPatients] ([PatientId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tProcedures')
      ALTER TABLE [dbo].[tStudies] DROP CONSTRAINT [FK_tStudies_tProcedures]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tProcedures')
      ALTER TABLE [dbo].[tStudies] ADD CONSTRAINT [FK_tStudies_tProcedures] FOREIGN KEY ([ProcedureId]) REFERENCES [dbo].[tProcedures] ([ProcedureId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tStations')
      ALTER TABLE [dbo].[tStudies] DROP CONSTRAINT [FK_tStudies_tStations]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tStations')
      ALTER TABLE [dbo].[tStudies] ADD CONSTRAINT [FK_tStudies_tStations] FOREIGN KEY ([StationId]) REFERENCES [dbo].[tStations] ([StationId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tUsers')
      ALTER TABLE [dbo].[tStudies] DROP CONSTRAINT [FK_tStudies_tUsers]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudies_tUsers')
      ALTER TABLE [dbo].[tStudies] ADD CONSTRAINT [FK_tStudies_tUsers] FOREIGN KEY ([ReferringPhysicianId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStudies Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStudies Table'
END
GO


--
-- Script To Update dbo.tStudyGroups Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tStudyGroups Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudyGroups_tGroups')
      ALTER TABLE [dbo].[tStudyGroups] DROP CONSTRAINT [FK_tStudyGroups_tGroups]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudyGroups_tGroups')
      ALTER TABLE [dbo].[tStudyGroups] ADD CONSTRAINT [FK_tStudyGroups_tGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[tGroups] ([GroupId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudyGroups_tStudies')
      ALTER TABLE [dbo].[tStudyGroups] DROP CONSTRAINT [FK_tStudyGroups_tStudies]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tStudyGroups_tStudies')
      ALTER TABLE [dbo].[tStudyGroups] ADD CONSTRAINT [FK_tStudyGroups_tStudies] FOREIGN KEY ([StudyId]) REFERENCES [dbo].[tStudies] ([StudyId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tStudyGroups Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tStudyGroups Table'
END
GO


--
-- Script To Update dbo.tUserGroups Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tUserGroups Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserGroups_tGroups')
      ALTER TABLE [dbo].[tUserGroups] DROP CONSTRAINT [FK_tUserGroups_tGroups]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserGroups_tGroups')
      ALTER TABLE [dbo].[tUserGroups] ADD CONSTRAINT [FK_tUserGroups_tGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[tGroups] ([GroupId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserGroups_tUsers')
      ALTER TABLE [dbo].[tUserGroups] DROP CONSTRAINT [FK_tUserGroups_tUsers]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserGroups_tUsers')
      ALTER TABLE [dbo].[tUserGroups] ADD CONSTRAINT [FK_tUserGroups_tUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tUserGroups Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tUserGroups Table'
END
GO


--
-- Script To Update dbo.tUserRoles Table In localhost\SQLEXPRESS.RIS_Prod
-- Generated Saturday, March 28, 2009, at 04:39 PM
--
-- Please backup localhost\SQLEXPRESS.RIS_Prod before executing this script
--


BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.tUserRoles Table'
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserRoles_tRoles')
      ALTER TABLE [dbo].[tUserRoles] DROP CONSTRAINT [FK_tUserRoles_tRoles]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserRoles_tRoles')
      ALTER TABLE [dbo].[tUserRoles] ADD CONSTRAINT [FK_tUserRoles_tRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[tRoles] ([RoleId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserRoles_tUsers')
      ALTER TABLE [dbo].[tUserRoles] DROP CONSTRAINT [FK_tUserRoles_tUsers]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = N'FK_tUserRoles_tUsers')
      ALTER TABLE [dbo].[tUserRoles] ADD CONSTRAINT [FK_tUserRoles_tUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[tUsers] ([UserId])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.tUserRoles Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.tUserRoles Table'
END
GO
