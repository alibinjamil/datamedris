alter procedure sp_get_users_for_role_in_group(@roleId int,@groupId int,@addText varchar(20))
as 
select *
from
(
select tUsers.UserId,tUsers.Name
  from tUserGroups
 inner join tUsers on tUserGroups.UserId = tUsers.UserId
 inner join tUserRoles on tUserRoles.UserId = tUsers.UserId
 where GroupId = @groupId
   and RoleId = @roleId
union
select 0,'[' + @addText + ']'
 where len(@addText) > 0
) as ttUsers
order by ttUsers.Name;       