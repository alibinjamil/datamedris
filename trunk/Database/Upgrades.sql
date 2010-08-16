ALTER TABLE [RIS].[dbo].[tStudies] ADD [IsManual] CHAR(1);

INSERT INTO [RIS].[dbo].[tRoles]
           ([Name],[Description],[CreatedBy],[CreationDate],[LastUpdatedBy],[LastUpdateDate])
     VALUES('Chief Technologist','Chief Technologist',1,1/1/1,1,1/1/1);
