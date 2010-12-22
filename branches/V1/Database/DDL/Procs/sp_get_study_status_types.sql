create procedure sp_get_study_status_types (@addText varchar(20)) as
select ttStudyStatusTypes.StudyStatusTypeId,ttStudyStatusTypes.Status
from
(
select tStudyStatusTypes.StudyStatusTypeId,tStudyStatusTypes.Status,tStudyStatusTypes.ColumnOrder
  from tStudyStatusTypes
union
select 0,'[' + @addText + ']',0
 where len(@addText) > 0
) as ttStudyStatusTypes
order by ttStudyStatusTypes.ColumnOrder;