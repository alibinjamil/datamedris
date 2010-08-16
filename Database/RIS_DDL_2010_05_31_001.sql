insert into tUserHospitals(UserId,HospitalId)
select UserId,HospitalId
  from tUsers
 where hospitalid is not null;

 alter table tUsers drop column HospitalId;
 
 