using System;
using System.Collections.Generic;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
namespace RIS.RISLibrary.Database
{
    public static class DatabaseUtility
    {
        public static object GetDateTime(object dateObj, object timeObj)
        {
            string timeStr = null;
            string dateStr = null;
            if(timeObj != null)
                timeStr = (string)timeObj;
            if(dateObj != null)
                dateStr = (string)dateObj;
            if (dateStr != null && timeStr != null)
                return new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(4, 2)), int.Parse(dateStr.Substring(6, 2)), int.Parse(timeStr.Substring(0, 2)), int.Parse(timeStr.Substring(2, 2)), int.Parse(timeStr.Substring(4, 2)));
            else if (dateStr != null)
                return new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(4, 2)), int.Parse(dateStr.Substring(6, 2)));
            else
                return null;
        }

        public static UserObject CreateUser(string name)
        {
            string[] names = name.Split(',');
            StringBuilder userId = new StringBuilder();
            if (names.Length > 1)
            {
                if (names[1].Trim().Length > 0)
                    userId.Append(names[1].Trim().ToLower()[0]);
                userId.Append(names[0].Trim().ToLower());
            }
            else if (names.Length == 1)
            {
                userId.Append(names[0].Trim().ToLower());
            }
            else
            {
                userId.Append("risuser");
            }
            bool quit = false;
            int count = 1;            
            UserObject user = null;
            do
            {
                user = new UserObject();
                user.LoginName.Value = userId.ToString();
                user.Load();
                if (!user.IsLoaded)
                {
                    quit = true;
                }
                else
                {
                    userId.Append(count.ToString());
                    count++;                    
                }
            }
            while (!quit);
            user.Name.Value = name;
            user.Password.Value = "password";
            user.IsActive.Value = 1;
            user.Save();
            return user;
        }
   
        public static string GetDICOMDate(DateTime date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(date.Year);
            if (date.Month < 10)
                sb.Append("0");
            sb.Append(date.Month);
            if (date.Day < 10)
                sb.Append("0");
            sb.Append(date.Day);
            return sb.ToString();
        }

    }
}
