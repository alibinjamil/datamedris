alter procedure sp_get_users_for_role (@roleId int, @addText varchar(20)) as
select *
from
(select 0 as UserId,'[' + @addText +']' as [Name]
 where len(@addText) > 0
union
select tUsers.UserId,tUsers.Name
  from tUsers
inner join tUserRoles on tUsers.UserId = tUserRoles.UserId
 where tUserRoles.RoleId = @roleId) as ttUsers
order by ttUsers.Name;