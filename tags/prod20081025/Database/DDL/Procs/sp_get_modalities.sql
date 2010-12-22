alter procedure sp_get_modalities (@addText varchar(20)) as
select *
from
(
select tModalities.ModalityId,tModalities.Name
  from tModalities
union
select 0,'[' + @addText + ']'
 where len(@addText) > 0
) as ttModalities
order by ttModalities.Name;