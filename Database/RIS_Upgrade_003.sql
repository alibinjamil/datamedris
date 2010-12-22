Update tFindings SET AudioUserName = tUsers.Name
FROM tFindings
INNER JOIN tUsers ON tFindings.AudioUserId = tUsers.UserId;
