/**************************************************************************
-- -Generated by xSQL SDK for Schema Comparison and Synchronization
-- -Date/Time: June 04, 2010 00:55:17

-- -Summary:
    Contains the T-SQL script that makes the database 
    .\SQL2005.RIS_PROD the same as the database .\SQL2005.RIS

-- -Action:
    Execute this script on .\SQL2005.RIS_PROD

-- -SQL Server version: 9.00.4035
**************************************************************************/

BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	SET QUOTED_IDENTIFIER ON
	SET ANSI_NULLS ON
	SET ANSI_PADDING ON
	SET ANSI_WARNINGS ON
	SET ARITHABORT ON
	SET NUMERIC_ROUNDABORT OFF
	SET CONCAT_NULL_YIELDS_NULL ON
	SET XACT_ABORT ON
COMMIT TRANSACTION
GO

IF EXISTS (select * from tempdb..sysobjects where id = OBJECT_ID('tempdb..#ErrorLog')) 
	DROP TABLE #ErrorLog 
CREATE TABLE #ErrorLog 
( pkid [int] IDENTITY(1,1) NOT NULL, errno [int] NOT NULL, errdescr [varchar](100) NULL )
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[tUsers] DROP CONSTRAINT [FK_tUsers_tHospitals]
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to drop foreign key dbo.tUsers.FK_tUsers_tHospitals')
END
GO
IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUserHospitals] (
	[UserHospitalId] [int] IDENTITY (1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[HospitalId] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add table dbo.tUserHospitals')
END
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[UsersSelectById]
	@userId int
AS
	SET NOCOUNT ON;
SELECT        tUsers.*
FROM            tUsers
WHERE		UserId = @userId
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add stored procedure dbo.UsersSelectById')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
ALTER procedure [dbo].[AssignHospitalToUser](@userId int,@hospitalId int,@adminUserId int)
AS

IF @hospitalId IS NOT NULL
insert
  into tUserGroups(UserId,GroupId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
select @userId,GroupId,@adminUserId,getDate(),@adminUserId,getDate()
  from tHospitalGroups
 where hospitalId = @hospitalId;
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to alter stored procedure dbo.AssignHospitalToUser')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[HospitalSelectByUserId]
	@userId int
AS
	SET NOCOUNT ON;
SELECT        tHospitals.*
FROM            tHospitals
inner join tUserHospitals ON tUserHospitals.HospitalId = tHospitals.HospitalId
WHERE tUserHospitals.UserId = @userId
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add stored procedure dbo.HospitalSelectByUserId')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[HospitalSelectNotByUserId]
	@userId int,
	@clientId int
AS
	SET NOCOUNT ON;
SELECT        tHospitals.*
FROM            tHospitals
WHERE HospitalId NOT IN (
SELECT HospitalId From tUserHospitals WHERE UserId = @userId)
AND ClientID = @clientId
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add stored procedure dbo.HospitalSelectNotByUserId')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE procedure [dbo].[UnassignHospitalFromUser](@userId int,@hospitalId int)
AS

IF @hospitalId IS NOT NULL
delete from tUserGroups
WHERE UserId = @userId 
  AND GroupId IN (Select GroupId From tHospitalGroups
 where hospitalId = @hospitalId);
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add stored procedure dbo.UnassignHospitalFromUser')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
ALTER PROCEDURE [dbo].[UsersSelectByHospitalId]
	@clientId int,
	@hospitalId int,
	@roleId int
AS
	SET NOCOUNT ON;
SELECT      DISTINCT  tUsers.*
FROM            tUsers
inner join tUserRoles on tUsers.UserId = tUserRoles.UserId
inner join tUserHospitals on tUsers.UserId = tUserHospitals.UserId
WHERE (@clientId = 0 OR tUsers.ClientId = @clientId)
  AND (@hospitalId =0 OR tUserHospitals.HospitalId = @hospitalId)
  AND (@roleId = 0 OR tUserRoles.RoleId = @roleId)
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to alter stored procedure dbo.UsersSelectByHospitalId')
END
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[tUserHospitals] ADD 
	CONSTRAINT [PK_tUserHospitals] PRIMARY KEY CLUSTERED 
	(
		[UserHospitalId]
	) ON [PRIMARY];
GO

IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add primary key dbo.tUserHospitals.PK_tUserHospitals')
END
GO
IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[tUserHospitals] ADD 
	CONSTRAINT [FK_tUserHospitals_tHospitals] FOREIGN KEY 
	(
		[HospitalId]
	)REFERENCES [dbo].[tHospitals](
		[HospitalId]
	)ON UPDATE NO ACTION ON DELETE NO ACTION 
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add foreign key dbo.tUserHospitals.FK_tUserHospitals_tHospitals')
END
GO
IF @@TRANCOUNT=0 BEGIN TRANSACTION
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[tUserHospitals] ADD 
	CONSTRAINT [FK_tUserHospitals_tUsers] FOREIGN KEY 
	(
		[UserId]
	)REFERENCES [dbo].[tUsers](
		[UserId]
	)ON UPDATE NO ACTION ON DELETE NO ACTION 
GO
IF @@ERROR<>0 
Begin
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
	INSERT INTO #ErrorLog (errno,errdescr) values(@@ERROR,'Failed to add foreign key dbo.tUserHospitals.FK_tUserHospitals_tUsers')
END
GO
IF EXISTS (Select * from #ErrorLog)
BEGIN
	IF @@TRANCOUNT>0 ROLLBACK TRANSACTION
END
ELSE
BEGIN
	IF @@TRANCOUNT>0 COMMIT TRANSACTION
END
IF EXISTS (Select * from #ErrorLog)
BEGIN
	Print 'Database synchronization script failed'
	GOTO QuitWithErrors
END
ELSE
BEGIN
	Print 'Database synchronization completed successfully'
END



QuitWithErrors:


