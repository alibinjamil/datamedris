using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.RISLibrary.Utilities
{
    public static class RISUtility
    {
        public static String FileSeperator
        {
            get
            {
                return "/";
            }
        }

        public static String ClassFileExtension
        {
            get
            {
                return ".cs";
            }
        }

        public static String TabCharacter
        {
            get
            {
                return "\t";
            }
        }

        public static string GetFullName(string firstName, string lastName)
        {
            StringBuilder sb = new StringBuilder(lastName);
            sb.Append(", ");
            sb.Append(firstName);
            return sb.ToString();
        }
        public static string GetDICOMDate(DateTime date)
        {
            StringBuilder sb = new StringBuilder() ;
            sb.Append(date.Year);
            if (date.Month < 10)
                sb.Append("0");
            sb.Append(date.Month);
            if (date.Day < 10)
                sb.Append("0");
            sb.Append(date.Day);
            return sb.ToString();
        }
        public static void GetFirstLastName(string FullName, ref string First, ref string Last)
        {
            string[] seperator ={ "," };
            string[] arr = FullName.Split(seperator, StringSplitOptions.None);
            if (arr.Length > 1)
                First = arr[1];
            else
                First = "";
            Last = arr[0];
        }
        public static string GetUSADate(DateTime date)
        {
            StringBuilder sb = new StringBuilder();
            if (date.Month < 10)
                sb.Append("0");
            sb.Append(date.Month);
            sb.Append("/");
            if (date.Day < 10)
                sb.Append("0");
            sb.Append(date.Day);
            sb.Append("/");

            sb.Append(date.Year);


            return sb.ToString();
        }
    }
}
