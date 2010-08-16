alter procedure sp_get_stations_for_modality(@addText varchar(20),@modalityId int)
as
select *
from
(
select tStations.StationId,tStations.StationName
  from tStations
 where tStations.ModalityId = @modalityId 
union
select 0,'[' + @addText + ']'
 where len(@addText) > 0
) as ttStations
order by ttStations.StationName;