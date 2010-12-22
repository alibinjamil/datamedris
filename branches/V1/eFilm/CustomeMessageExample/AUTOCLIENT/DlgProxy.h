// DlgProxy.h : header file
//

#if !defined(AFX_DLGPROXY_H__F6C77E4B_8CB2_4A25_BD0C_559E717ADCB4__INCLUDED_)
#define AFX_DLGPROXY_H__F6C77E4B_8CB2_4A25_BD0C_559E717ADCB4__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CEFilmAutoClientDlg;

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlgAutoProxy command target

class CEFilmAutoClientDlgAutoProxy : public CCmdTarget
{
	DECLARE_DYNCREATE(CEFilmAutoClientDlgAutoProxy)

	CEFilmAutoClientDlgAutoProxy();           // protected constructor used by dynamic creation

// Attributes
public:
	CEFilmAutoClientDlg* m_pDialog;

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEFilmAutoClientDlgAutoProxy)
	public:
	virtual void OnFinalRelease();
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CEFilmAutoClientDlgAutoProxy();

	// Generated message map functions
	//{{AFX_MSG(CEFilmAutoClientDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
	DECLARE_OLECREATE(CEFilmAutoClientDlgAutoProxy)

	// Generated OLE dispatch map functions
	//{{AFX_DISPATCH(CEFilmAutoClientDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_DISPATCH
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DLGPROXY_H__F6C77E4B_8CB2_4A25_BD0C_559E717ADCB4__INCLUDED_)
