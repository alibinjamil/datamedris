alter procedure sp_get_groups_for_study(@studyId int,@assigned int)
as
select * 
from
(select tGroups.GroupId as StudyGroupId,tGroups.Name 
  from tGroups
 where 0 = @assigned 
   and tGroups.GroupId NOT IN (select tStudyGroups.GroupId from tStudyGroups where tStudyGroups.StudyId = @studyId)
union
select 0,'[Please Select]'
 where 0 = @assigned 
union 
select tStudyGroups.StudyGroupId,tGroups.Name 
  from tStudyGroups
inner join tGroups on tGroups.GroupId = tStudyGroups.GroupId
 where 1 = @assigned 
   and tStudyGroups.StudyId = @studyId) as ttGroups
order by ttGroups.Name
