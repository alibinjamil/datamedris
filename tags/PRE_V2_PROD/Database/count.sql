select 'image',count(0) from Image
union
select 'patient',count(0) from patient
union
select 'studies',count(0) from study
union 
select 'series',count(0) from series