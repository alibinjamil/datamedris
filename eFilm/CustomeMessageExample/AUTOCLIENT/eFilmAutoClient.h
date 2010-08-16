// eFilmAutoClient.h : main header file for the EFILMAUTOCLIENT application
//

#if !defined(AFX_EFILMAUTOCLIENT_H__DB0F6667_80E2_4A77_8BDB_986CE1469987__INCLUDED_)
#define AFX_EFILMAUTOCLIENT_H__DB0F6667_80E2_4A77_8BDB_986CE1469987__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientApp:
// See eFilmAutoClient.cpp for the implementation of this class
//

class CEFilmAutoClientApp : public CWinApp
{
public:
	CEFilmAutoClientApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEFilmAutoClientApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CEFilmAutoClientApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EFILMAUTOCLIENT_H__DB0F6667_80E2_4A77_8BDB_986CE1469987__INCLUDED_)
