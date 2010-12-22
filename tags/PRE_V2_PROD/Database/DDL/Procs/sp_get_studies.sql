USE [RIS]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_studies]    Script Date: 11/24/2007 01:34:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_get_studies](
@userId int,@roleId int,@orderBy int,@isAsc int,@pageNum int, @pageSize int,@patientName varchar(64),
@patientId varchar(64),@status int,@performed int,@modality varchar(30),@procedure varchar(100),
@radiologist varchar(64),@physician varchar(64),@doCount int, @doLog int)
as
SET NOCOUNT ON;
declare @query varchar(8000);
declare @orderByQuery varchar(100);
begin

set @orderByQuery = ' order by ';
if @orderBy = 0 
	set @orderByQuery = @orderByQuery  + ' tStudyStatusTypes.ColumnOrder,convert(varchar(10),tStudies.StudyDate,101)';
else if @orderBy = 1 
	set @orderByQuery = @orderByQuery + 'tPatients.Name';
else if @orderBy = 2
	set @orderByQuery = @orderByQuery + 'tPatients.ExternalPatientId';
else if @orderBy = 3 
	set @orderByQuery = @orderByQuery + 'tStudyStatusTypes.Status';
else if @orderBy = 4 
	set @orderByQuery = @orderByQuery + 'tStudies.StudyDate';
else if @orderBy = 5 
	set @orderByQuery = @orderByQuery + 'tModalities.Name';
else if @orderBy = 6 
	set @orderByQuery = @orderByQuery + 'tProcedures.Name';
else if @orderBy = 7 
	set @orderByQuery = @orderByQuery + 'ttFindings.RadiologistName';
else if @orderBy = 8 
	set @orderByQuery = @orderByQuery + 'tUsers.Name';
else
	set @orderByQuery = @orderByQuery  + ' StudyStatusTypes.ColumnOrder,convert(varchar(10),tStudies.StudyDate,101)';

if @isAsc =	0
	set @orderByQuery = @orderByQuery + ' desc';

set @query = '';

if @doCount = 0
	set @query = 'select top ' + CAST(@pageSize as varchar(10)) + ' * from (';

if @doCount = 1 
	set @query = 'select count(0) from tStudies ';
else 
	set @query = @query + 
		'select row_number() over (' + @orderByQuery + ') as rowNum, tStudies.StudyId,tPatients.Name as PatientName,tPatients.ExternalPatientId,tStudies.StudyStatusId,tStudyStatusTypes.Status,convert(varchar(10),tStudies.StudyDate,101) as StudyDate,tModalities.Name as Modality,tProcedures.Name as ProcedureName, ttFindings.RadiologistName, tUsers.Name as ReferringPhysicianName,ttFindings.FindingId,ttPatients.PatRecCount from tStudies ';

if @roleId != 1 --Not Admin
	set @query = @query + 
	' inner join (select distinct tStudies.StudyId as UniqueStudyId from tStudies 
     inner join tStudyGroups on tStudyGroups.StudyId = tStudies.StudyId 
	 inner join tUserGroups on tStudyGroups.GroupId = tUserGroups.GroupId  
	 where tUserGroups.UserId = ' + cast(@userId as varchar(10)) + ')ttStudies  on ttStudies.UniqueStudyId = tStudies.StudyId ';

set @query = @query +
'inner join tPatients on tPatients.PatientId = tStudies.PatientId
inner join (select count(0) as PatRecCount,PatientId from tStudies group by PatientId) ttPatients on ttPatients.PatientId=tStudies.PatientId
inner join tStudyStatusTypes on tStudyStatusTypes.StudyStatusTypeId = tStudies.StudyStatusId
inner join tModalities on tStudies.ModalityId = tModalities.ModalityId
left outer join tProcedures on tProcedures.ProcedureId = tStudies.ProcedureId 
left outer join 
(select tFindings.FindingId,tFindings.AudioUserId as RadiologistId, ttRadiolgists.Name as RadiologistName, tFindings.AudioReportPath,tFindings.TextualTranscript,ttTranscriptionists.UserId as TranscriptionistUserId,ttTranscriptionists.Name as TranscriptionistName
  from tFindings
left outer join tUsers as ttRadiolgists on ttRadiolgists.UserId = tFindings.AudioUserId
left outer join tUsers as ttTranscriptionists on ttTranscriptionists.UserId = TranscriptUserId
)ttFindings on ttFindings.FindingId = tStudies.LatestFindingId
left outer join tUsers on tStudies.ReferringPhysicianId = tUsers.UserId 
where 1=1';

if LEN(@patientName) > 0
	set @query = @query + ' and upper(tPatients.Name) LIKE ''%' + upper(@patientName) + '%''';

if LEN(@patientId) > 0
	set @query = @query + ' and upper(tPatients.ExternalPatientId) LIKE ''%' + upper(@patientId) + '%''';

if (LEN(@modality) > 0 and @modality != '0') -- ALL case
	set @query = @query + ' and tStudies.ModalityId IN (' + @modality + ')';

if @status > 0
	set @query = @query + ' and tStudies.StudyStatusId =' + cast(@status as varchar(4));

if LEN(@procedure) > 0
	set @query = @query + ' and upper(tProcedures.Name) LIKE ''%' + upper(@procedure) + '%''';

if LEN(@radiologist) > 0
	set @query = @query + ' and upper(ttFindings.RadiologistName) LIKE ''%' + upper(@radiologist) + '%''';

if LEN(@physician) > 0
	set @query = @query + ' and upper(tUsers.Name) LIKE ''%' + upper(@physician) + '%''';

if @performed >= 0
begin
	if @performed = 0 or @performed = 1
		set @query = @query + ' and datediff(day,tStudies.StudyDate,getdate()) = ' + cast(@performed as varchar(2));
	else 
		set @query = @query + 'and datediff(day,tStudies.StudyDate,getdate()) >= 0 and datediff(day,tStudies.StudyDate,getdate()) <= ' + cast(@performed as varchar(2));
end;
--set @query = @query + @orderByQuery;
if(@doCount = 0)
	set @query = @query + ')as a where rowNum > ' + CAST(( @pageNum-1)*@pageSize as varchar(10));

--print(@query);
exec(@query);

SET NOCOUNT OFF;

end;