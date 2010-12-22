#include "stdafx.h"
#include "ExportFunctions.h"

int WINAPI ViewReport(EFILMSTUDYINFO si)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	CString strOut;
	strOut.Format("HISRISInterface - ViewReport\nWindow Handle: %i\n"
				"Patient ID: %s\n"
				"Accession Number: %s\n"
				"Study UID: %s\n",
				si.hWnd, si.strPatientID, si.strAccessionNo, si.strStudyUID);
	AfxMessageBox(strOut);

	return 1;
}

int WINAPI CreateReport(EFILMSTUDYINFO si)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	CString strOut;
	strOut.Format("HISRISInterface - CreateReport\nWindow Handle: %i\n"
				"Patient ID: %s\n"
				"Accession Number: %s\n"
				"Study UID: %s\n",
				si.hWnd, si.strPatientID, si.strAccessionNo, si.strStudyUID);
	AfxMessageBox(strOut);

	return 1;
}

int WINAPI SetProperties(HWND hWnd)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	CString strOut;
	strOut.Format("HISRISInterface - SetProperties\nWindow Handle: %i\n", hWnd);
	AfxMessageBox(strOut);

	return 1;
}

int WINAPI GetStudyStatus(EFILMSTUDYINFO si, LPTSTR strStatus, LPTSTR strUser, COLORREF* pcr)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	strcpy(strStatus,"Reported");
	strcpy(strUser,"NewUser");

	return 1;
}

int WINAPI OnStudyOpen(EFILMSTUDYINFO si)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	CString strOut;
	strOut.Format("HISRISInterface - OnStudyOpen\nWindow Handle: %i\n"
				"Patient ID: %s\n"
				"Accession Number: %s\n"
				"Study UID: %s\n",
				si.hWnd, si.strPatientID, si.strAccessionNo, si.strStudyUID);
	AfxMessageBox(strOut);

	return 1;
}

int WINAPI OnStudyClose(EFILMSTUDYINFO si)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
	CString strOut;
	strOut.Format("HISRISInterface - OnStudyClose\nWindow Handle: %i\n"
				"Patient ID: %s\n"
				"Accession Number: %s\n"
				"Study UID: %s\n",
				si.hWnd, si.strPatientID, si.strAccessionNo, si.strStudyUID);
	AfxMessageBox(strOut);

	return 1;
}

int WINAPI OnActivateApp(BOOL bActive)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// Replace this with your own code
/*	CString strOut;
	strOut.Format("HISRISInterface - OnActivateApp\nbActive: %d\n", bActive);
	AfxMessageBox(strOut);
*/
	return 1;
}

int WINAPI CanExit()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox("HISRISInterface - CanExit");
	return 1;
}

// This function is optional
int WINAPI FilterDllMsg(MSG* pMsg)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	try
	{
		return (int) AfxGetThread()->PreTranslateMessage(pMsg);
	}
	catch(CException* e)
	{
		TRACE("Exception thrown in FilterDllMsg\n");

		e->Delete();
		return 0;
	}
}
