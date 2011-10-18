using System;
using System.Collections.Generic;
using System.Text;

namespace RIS.RISLibrary.Utilities
{
    public static class Constants
    {        
        public static class Roles
        {
            public static int Admin = 1;
            public static int Radiologist = 2;
            public static int ReferringPhysician = 3;
            public static int Technologist = 4;
            public static int Transcriptionist = 5;
            public static int PerformingPhysician = 6;
            public static int ChiefTechnologist = 7;
            public static int ClientAdmin = 8;
            public static int ClientTechnologist = 9;
            public static int HospitalAdmin = 10;
            public static int HospitalStaff = 11;
        }

        public static class ExamAccessLevel
        {
            public static int Hospital = 1;
            public static int User = 2;
        }

        public static class StudyStatusTypes
        {
            public static int New = 1;
            public static int Dictated = 2;
            public static int Transcribed = 3;
            public static int PendingVerification = 4;
            public static int Verified = 5;
            public static int MarkForRetranscription = 6;
            public static int Redictated = 7;
            public static int PreRelease = 8;
            public static int Qaed = 9;
            public static int Rejected = 10;
        }
        public static class Database
        {
            public static int NullUserId = -1;
            public static int SystemUserId = 0;
        }
        public static class LogActions
        {
            public static string ViewedStudy = "Viewed Study";
            public static string ViewedExam = "Viewed Exam";
            public static string Login = "Login";
            public static string Logout = "Logout";
            public static string DictatedStudy = "Dictated Study";
            public static string MarkedStudyForVerification = "Marked Study for Verification";
            public static string VerifiedStudy = "Verified Study";
            public static string RedictatedStudy = "Re-dictated Study";
            public static string Created = "Created";
            public static string Revised = "Revised";
            public static string Updated = "Updated";
            public static string Qaed = "Qaed";
            public static string ReleasedToRad = "Released to Radiologist";
            public static string AppliedTemplate = "Applied Template";
            public static string AddedAttachment = "Added Attachment";
            public static string CallbackExam = "Called back Exam";
            public static string RejectedExam = "Exam was rejected";
        }
        public static class ReportTypes
        {
            public static byte Manual = 1;
            public static byte Upload = 2;
            public static byte Scan = 3;
        }
    }
}
