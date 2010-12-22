BEGIN

INSERT INTO tRoles (Name,Description,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
VALUES ('Chief Technologist','Chief Technologist',-1,GETDATE(),-1,GETDATE());

DECLARE @UserId int;

INSERT INTO tUsers (LoginName,Password,Name,IsActive,CreationDate,CreatedBy,LastUpdatedBy,LastUpdateDate)
VALUES ('chief','123','Chief Technologist','Y',GetDate(),-1,-1,GetDate());

SELECT @UserId = @@IDENTITY;

INSERT INTO tUserRoles (UserId,RoleId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
VALUES (@UserId,7,-1,GetDate(),-1,GetDate());

INSERT INTO tUserGroups (UserId,GroupId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
VALUES (@UserId,2,-1,GetDate(),-1,GetDate());

DELETE
  FROM tLog WHERE StudyId NOT IN ( SELECT StudyId FROM tStudies);

DELETE FROM tLog WHERE UserId NOT IN ( SELECT UserId FROM tUsers);

DELETE FROM tImages WHERE SeriesId IN (SELECT SeriesId FROM tSeries WHERE StudyId NOT IN (SELECT StudyId FROM tStudies));
DELETE FROM tSeries WHERE StudyId NOT IN (SELECT StudyId FROM tStudies);

DELETE FROM tStudyGroups WHERE StudyId NOT IN (SELECT StudyId FROM tStudies);

UPDATE tUsers SET IsActive = '1' WHERE IsActive = 'Y';
UPDATE tUsers SET IsActive = '0' WHERE IsActive = 'N';

END;