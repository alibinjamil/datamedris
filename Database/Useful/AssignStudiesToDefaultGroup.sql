declare @m_groupId int;

declare group_cursor cursor for 
select groupid from tGroups where isdefault = 'Y';

open group_cursor;

fetch next from group_cursor into @m_groupid;

while @@FETCH_STATUS = 0 
begin
	insert into tStudyGroups(StudyId,GroupId,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate)
	select StudyId,@m_groupid,0,getdate(),0,getdate() from tStudies where studyid not in (select studyid from tStudyGroups where groupid = @m_groupid);
	fetch next from group_cursor into @m_groupid;
end;

close group_cursor;
deallocate group_cursor;