#ifndef EXPORTFUNCTIONS_H
#define EXPORTFUNCTIONS_H

#ifdef __cplusplus
extern "C" {  /* Assume C declarations for C++ */
#endif        /* __cplusplus */

struct EFILMSTUDYINFO{
	HWND hWnd;
	LPCTSTR strPatientID;
	LPCTSTR strPatientName;
	LPCTSTR strSex;
	LPCTSTR strAccessionNo;
	LPCTSTR strModality;
	LPCTSTR strStudyDate;
	LPCTSTR strStudyTime;
	LPCTSTR strStudyDescription;
	LPCTSTR strReferringPhysician;
	LPCTSTR strReadingPhysician;
	LPCTSTR strStudyUID;
	LPCTSTR strUser;
	LPCTSTR strPassword;
	BOOL	bSelected;
};

int WINAPI ViewReport(EFILMSTUDYINFO si);
int WINAPI CreateReport(EFILMSTUDYINFO si);
int WINAPI SetProperties(HWND hWnd);
int WINAPI GetStudyStatus(EFILMSTUDYINFO si, LPTSTR strStatus, LPTSTR strUser, COLORREF* pcr);
int WINAPI OnStudyOpen(EFILMSTUDYINFO si);
int WINAPI OnStudyClose(EFILMSTUDYINFO si);
int WINAPI OnActivateApp(BOOL bActive);
int WINAPI CanExit();
int WINAPI FilterDllMsg(MSG* pMsg);

#ifdef __cplusplus
}       /* End of extern "C" { */
#endif  /* __cplusplus */

#endif
