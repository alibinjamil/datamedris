// DlgProxy.cpp : implementation file
//

#include "stdafx.h"
#include "eFilmAutoClient.h"
#include "DlgProxy.h"
#include "eFilmAutoClientDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlgAutoProxy

IMPLEMENT_DYNCREATE(CEFilmAutoClientDlgAutoProxy, CCmdTarget)

CEFilmAutoClientDlgAutoProxy::CEFilmAutoClientDlgAutoProxy()
{
	EnableAutomation();
	
	// To keep the application running as long as an automation 
	//	object is active, the constructor calls AfxOleLockApp.
	AfxOleLockApp();

	// Get access to the dialog through the application's
	//  main window pointer.  Set the proxy's internal pointer
	//  to point to the dialog, and set the dialog's back pointer to
	//  this proxy.
	ASSERT (AfxGetApp()->m_pMainWnd != NULL);
	ASSERT_VALID (AfxGetApp()->m_pMainWnd);
	ASSERT_KINDOF(CEFilmAutoClientDlg, AfxGetApp()->m_pMainWnd);
	m_pDialog = (CEFilmAutoClientDlg*) AfxGetApp()->m_pMainWnd;
	m_pDialog->m_pAutoProxy = this;
}

CEFilmAutoClientDlgAutoProxy::~CEFilmAutoClientDlgAutoProxy()
{
	// To terminate the application when all objects created with
	// 	with automation, the destructor calls AfxOleUnlockApp.
	//  Among other things, this will destroy the main dialog
	if (m_pDialog != NULL)
		m_pDialog->m_pAutoProxy = NULL;
	AfxOleUnlockApp();
}

void CEFilmAutoClientDlgAutoProxy::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

	CCmdTarget::OnFinalRelease();
}

BEGIN_MESSAGE_MAP(CEFilmAutoClientDlgAutoProxy, CCmdTarget)
	//{{AFX_MSG_MAP(CEFilmAutoClientDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CEFilmAutoClientDlgAutoProxy, CCmdTarget)
	//{{AFX_DISPATCH_MAP(CEFilmAutoClientDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_DISPATCH_MAP
END_DISPATCH_MAP()

// Note: we add support for IID_IEFilmAutoClient to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .ODL file.

// {B9C9813B-B211-43F3-92C9-3575CCA9FBF3}
static const IID IID_IEFilmAutoClient =
{ 0xb9c9813b, 0xb211, 0x43f3, { 0x92, 0xc9, 0x35, 0x75, 0xcc, 0xa9, 0xfb, 0xf3 } };

BEGIN_INTERFACE_MAP(CEFilmAutoClientDlgAutoProxy, CCmdTarget)
	INTERFACE_PART(CEFilmAutoClientDlgAutoProxy, IID_IEFilmAutoClient, Dispatch)
END_INTERFACE_MAP()

// The IMPLEMENT_OLECREATE2 macro is defined in StdAfx.h of this project
// {17AE9AFA-1C3B-4714-8189-E62124E04A25}
IMPLEMENT_OLECREATE2(CEFilmAutoClientDlgAutoProxy, "EFilmAutoClient.Application", 0x17ae9afa, 0x1c3b, 0x4714, 0x81, 0x89, 0xe6, 0x21, 0x24, 0xe0, 0x4a, 0x25)

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlgAutoProxy message handlers
