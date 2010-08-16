truncate table [tRoles];
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Admin', 'Admin',0,getdate(),0,getdate());
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Radiologist', 'Radiologist',0,getdate(),0,getdate());
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Referring Physician', 'Referring Physician',0,getdate(),0,getdate());
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Technologist', 'Technologist',0,getdate(),0,getdate());
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Transcriptionist', 'Transcriptionist',0,getdate(),0,getdate());
INSERT INTO [tRoles]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])VALUES('Performing Physician', 'Performing Physician',0,getdate(),0,getdate());

truncate table [tStudyStatusTypes];
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('New',1);
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('Dictated',2);
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('Transcribed',3);
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('PendingVerification',4);
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('Verified',5);
INSERT INTO [tStudyStatusTypes]([Status],[ColumnOrder]) VALUES('MarkForRetranscription',6);
