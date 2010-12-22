create procedure sp_get_num_user_roles (@userId int) as
select count(0) 
  from tUserRoles
 where tUserRoles.UserId = @userId;