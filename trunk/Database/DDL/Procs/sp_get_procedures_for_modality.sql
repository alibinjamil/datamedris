alter procedure sp_get_procedures_for_modality(@addText varchar(20),@modalityId int)
as 
select *
from
(
select tProcedures.ProcedureId,tProcedures.Name
  from tProcedures
 where tProcedures.ModalityId = @modalityId 
union
select 0,'[' + @addText + ']'
 where len(@addText) > 0
) as ttProcedures
order by ttProcedures.Name;