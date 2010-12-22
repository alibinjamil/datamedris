select * 
  from tStudies 
inner join 
(select tUserGroups.UserId 
 from tUserGroups 
inner join 
(Select tGroupRelations.SecondGroupId AS SecondGroupId  
	   from tUserGroups inner join tGroupRelations on tGroupRelations.FirstGroupId = tUserGroups.GroupId 
      where userId = 1
      union 
     Select tGroupRelations.FirstGroupId AS SecondGroupId 
	   from tUserGroups inner join tGroupRelations on tGroupRelations.SecondGroupId = tUserGroups.GroupId 
	  where userId = 1) ttGroups on ttGroups.SecondGroupId = GroupId
inner join tUserRoles on tUserRoles.UserId = tUserGroups.UserId
where tUserRoles.RoleId = 3) ttUsers on ttUsers.UserId = tStudies.ReferringPhysicianId