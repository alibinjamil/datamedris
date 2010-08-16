truncate table tFindings;
truncate table tGroupRelations;
truncate table tGroups;
truncate table tImages;
truncate table tModalities;
truncate table tModalityDetails;
truncate table tPatients;
truncate table tProcedures;
truncate table tStations;
truncate table tStudies;
truncate table tStudyGroups;
truncate table tSeries;
truncate table tWorkLists;
truncate table tUsers;
truncate table tUserRoles;
truncate table tUserGroups;

INSERT INTO [tGroups]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES ('Madison','Madison',-1,getdate(),-1,getdate());
INSERT INTO [tGroups]([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES ('Scott','Scott',-1,getdate(),-1,getdate());

INSERT INTO [tGroupRelations]([FirstGroupId],[SecondGroupId],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES (1,2,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('rad','123','Radiologist','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (1,2,-1,getdate(),-1,getdate());
INSERT INTO [tUserGroups]([UserId],[GroupId],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES (1,1,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('tran','123','Transcriptionist','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (2,5,-1,getdate(),-1,getdate());
INSERT INTO [tUserGroups]([UserId],[GroupId],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES (2,1,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('tech','123','Technologist','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (3,4,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('admin','123','Administrator','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (4,1,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('rphy','123','Referring Physician','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (5,3,-1,getdate(),-1,getdate());
INSERT INTO [tUserGroups]([UserId],[GroupId],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES (5,2,-1,getdate(),-1,getdate());

INSERT INTO [tUsers]([LoginName],[Password],[Name],[IsActive],[CreationDate],[CreatedBy],[LastUpdatedBy],[LastUpdateDate])
     VALUES('arain','123','ARAIN, M.','Y',getdate(),-1,-1,getdate());
INSERT INTO [tUserRoles]([UserId],[RoleId],[CreatedBy],[CreationDate],[LastUpdatedBy] ,[LastUpdateDate])
     VALUES (6,3,-1,getdate(),-1,getdate());
INSERT INTO [tUserGroups]([UserId],[GroupId],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES (6,2,-1,getdate(),-1,getdate());