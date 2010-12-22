alter procedure sp_insert_study_group(@studyId int,@userId int,@adminUserId int)
AS
insert
  into tStudyGroups(StudyId,GroupId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
(select @studyId,SecondGroupId as GroupId,@adminUserId,getDate(),@adminUserId,getDate()
  from tUserGroups
inner join tGroupRelations on tUserGroups.GroupId = tGroupRelations.FirstGroupId
 where userId = @userId
union 
select @studyId,FirstGroupId as GroupId , @adminUserId,getDate(),@adminUserId,getDate()
  from tUserGroups
inner join tGroupRelations on tUserGroups.GroupId = tGroupRelations.SecondGroupId
 where userId = @userId
union
select @studyId,GroupId, @adminUserId,getDate(),@adminUserId,getDate()
  from tUserGroups
 where tUserGroups.UserId = @userId
union 
select @studyId,GroupId,@adminUserId,getDate(),@adminUserId,getDate()
  from tGroups
 where IsDefault = 'Y'
)
