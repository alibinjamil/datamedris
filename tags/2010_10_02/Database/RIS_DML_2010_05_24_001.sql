/**************************************************************************
-- -Generated by xSQL SDK for Data Comparison and Synchronization
-- -Date/Time: May 24, 2010 20:43:15

-- -Summary: Synchronization script that makes the content of the database .\SQL2005.RIS_PROD 
    the same as the content of the database .\SQL2005.RIS.

    BACKUP the database .\SQL2005.RIS_PROD before running this script. 

-- -Action:  Execute this script on .\SQL2005.RIS_PROD

-- -SQL Server version: 9.00.4035
**************************************************************************/

SET XACT_ABORT ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF


BEGIN TRANSACTION

DECLARE @error INT
DECLARE @ptrBinary varbinary(16)

-- --Table: [dbo].[tStudyStatusTypes]
-- --Insert
SET IDENTITY_INSERT [dbo].[tStudyStatusTypes] ON
INSERT INTO [dbo].[tStudyStatusTypes] ([StudyStatusTypeId], [Status], [ColumnOrder]) VALUES(9, 'QA Done', 7)
INSERT INTO [dbo].[tStudyStatusTypes] ([StudyStatusTypeId], [Status], [ColumnOrder]) VALUES(10, 'Rejected', 8)

SET IDENTITY_INSERT [dbo].[tStudyStatusTypes] OFF


COMMIT TRANSACTION
