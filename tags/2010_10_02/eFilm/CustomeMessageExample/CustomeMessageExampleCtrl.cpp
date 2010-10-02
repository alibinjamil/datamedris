// CustomeMessageExampleCtrl.cpp : Implementation of the CCustomeMessageExampleCtrl ActiveX Control class.

#include "stdafx.h"
#include "CustomeMessageExample.h"
#include "CustomeMessageExampleCtrl.h"
#include "CustomeMessageExamplePropPage.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


IMPLEMENT_DYNCREATE(CCustomeMessageExampleCtrl, COleControl)



// Message map

BEGIN_MESSAGE_MAP(CCustomeMessageExampleCtrl, COleControl)
	ON_OLEVERB(AFX_IDS_VERB_PROPERTIES, OnProperties)
END_MESSAGE_MAP()



// Dispatch map

BEGIN_DISPATCH_MAP(CCustomeMessageExampleCtrl, COleControl)
	DISP_FUNCTION_ID(CCustomeMessageExampleCtrl, "AboutBox", DISPID_ABOUTBOX, AboutBox, VT_EMPTY, VTS_NONE)
//	DISP_FUNCTION_ID(CCustomeMessageExampleCtrl, "getStudy", dispidgetStudy, getStudy, VT_BOOL, VTS_VARIANT)
	DISP_FUNCTION_ID(CCustomeMessageExampleCtrl, "openStudy", dispidopenStudy, openStudy, VT_BOOL, VTS_NONE)
//	DISP_FUNCTION_ID(CCustomeMessageExampleCtrl, "openStudy1", dispidopenStudy1, openStudy1, VT_BOOL, VTS_VARIANT VTS_VARIANT VTS_BOOL VTS_BOOL VTS_I2 VTS_I2 VTS_I2 VTS_I2 VTS_BOOL VTS_BOOL)
END_DISPATCH_MAP()



// Event map

BEGIN_EVENT_MAP(CCustomeMessageExampleCtrl, COleControl)
END_EVENT_MAP()



// Property pages

// TODO: Add more property pages as needed.  Remember to increase the count!
BEGIN_PROPPAGEIDS(CCustomeMessageExampleCtrl, 1)
	PROPPAGEID(CCustomeMessageExamplePropPage::guid)
END_PROPPAGEIDS(CCustomeMessageExampleCtrl)



// Initialize class factory and guid

IMPLEMENT_OLECREATE_EX(CCustomeMessageExampleCtrl, "CUSTOMEMESSAGEEX.CustomeMessageExCtrl.1",
	0x23e9fae, 0x9641, 0x49b6, 0x95, 0xa0, 0x24, 0xf1, 0x9e, 0x43, 0x69, 0x8d)



// Type library ID and version

IMPLEMENT_OLETYPELIB(CCustomeMessageExampleCtrl, _tlid, _wVerMajor, _wVerMinor)



// Interface IDs

const IID BASED_CODE IID_DCustomeMessageExample =
		{ 0x9F9EDA34, 0x703E, 0x4F18, { 0xA9, 0xBB, 0x28, 0xDD, 0xFC, 0x65, 0x7D, 0x5D } };
const IID BASED_CODE IID_DCustomeMessageExampleEvents =
		{ 0x408EF484, 0xF278, 0x4BE5, { 0x8C, 0x74, 0xBD, 0xBA, 0x41, 0x12, 0xF8, 0xE5 } };



// Control type information

static const DWORD BASED_CODE _dwCustomeMessageExampleOleMisc =
	OLEMISC_ACTIVATEWHENVISIBLE |
	OLEMISC_SETCLIENTSITEFIRST |
	OLEMISC_INSIDEOUT |
	OLEMISC_CANTLINKINSIDE |
	OLEMISC_RECOMPOSEONRESIZE;

IMPLEMENT_OLECTLTYPE(CCustomeMessageExampleCtrl, IDS_CUSTOMEMESSAGEEXAMPLE, _dwCustomeMessageExampleOleMisc)


// CCustomeMessageExampleCtrl::CCustomeMessageExampleCtrlFactory::UpdateRegistry -
// Adds or removes system registry entries for CCustomeMessageExampleCtrl

BOOL CCustomeMessageExampleCtrl::CCustomeMessageExampleCtrlFactory::UpdateRegistry(BOOL bRegister)
{
	// TODO: Verify that your control follows apartment-model threading rules.
	// Refer to MFC TechNote 64 for more information.
	// If your control does not conform to the apartment-model rules, then
	// you must modify the code below, changing the 6th parameter from
	// afxRegApartmentThreading to 0.

	if (bRegister)
		return AfxOleRegisterControlClass(
			AfxGetInstanceHandle(),
			m_clsid,
			m_lpszProgID,
			IDS_CUSTOMEMESSAGEEXAMPLE,
			IDB_CUSTOMEMESSAGEEXAMPLE,
			afxRegApartmentThreading,
			_dwCustomeMessageExampleOleMisc,
			_tlid,
			_wVerMajor,
			_wVerMinor);
	else
		return AfxOleUnregisterClass(m_clsid, m_lpszProgID);
}



// CCustomeMessageExampleCtrl::CCustomeMessageExampleCtrl - Constructor

CCustomeMessageExampleCtrl::CCustomeMessageExampleCtrl()
{
	InitializeIIDs(&IID_DCustomeMessageExample, &IID_DCustomeMessageExampleEvents);
	// TODO: Initialize your control's instance data here.
	//initEFilm();
}

/*void CCustomeMessageExampleCtrl:: initEFilm()
{
	eFilm = new CEFilm;

	COleException *e = new COleException;

	try 
	{
		// Create instance of Microsoft System Information Control 
		// by using ProgID.
		if (FALSE == eFilm->CreateDispatch(_T("EFilm.Document"), e))
			throw e;
	}
	//Catch control-specific exceptions.
	catch (COleDispatchException * e) 
	{
		/*CString cStr;

		if (!e->m_strSource.IsEmpty())
			cStr = e->m_strSource + " - ";
		if (!e->m_strDescription.IsEmpty())
			cStr += e->m_strDescription;
		else
			cStr += "unknown error";

		AfxMessageBox(L"Unable to init", MB_OK, 0);

		e->Delete();
	}
	//Catch all MFC exceptions, including COleExceptions.
	// OS exceptions will not be caught.
	catch (CException *e) 
	{
		/*CString cStr;
		cStr.Format("%s(%d): OLE Execption caught: SCODE = %x", 
			__FILE__, __LINE__, COleException::Process(e));

		AfxMessageBox(L"Unable to init", MB_OK, 0);
		e->Delete();
	}
}*/


// CCustomeMessageExampleCtrl::~CCustomeMessageExampleCtrl - Destructor

CCustomeMessageExampleCtrl::~CCustomeMessageExampleCtrl()
{
	// TODO: Cleanup your control's instance data here.
	//AfxMessageBox(L"Closing again",0,0);
}



// CCustomeMessageExampleCtrl::OnDraw - Drawing function

void CCustomeMessageExampleCtrl::OnDraw(
			CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid)
{
		if (!pdc)
			return;

}



// CCustomeMessageExampleCtrl::DoPropExchange - Persistence support

void CCustomeMessageExampleCtrl::DoPropExchange(CPropExchange* pPX)
{
	ExchangeVersion(pPX, MAKELONG(_wVerMinor, _wVerMajor));
	COleControl::DoPropExchange(pPX);

	// TODO: Call PX_ functions for each persistent custom property.
	PX_String(pPX,_T("patientId"),patientId);
	PX_String(pPX,_T("accessionNo"),accessionNo);
}



// CCustomeMessageExampleCtrl::OnResetState - Reset control to default state

void CCustomeMessageExampleCtrl::OnResetState()
{
	COleControl::OnResetState();  // Resets defaults found in DoPropExchange

	// TODO: Reset any other control state here.
}



DWORD CCustomeMessageExampleCtrl::GetControlFlags()
{
	return COleControl::GetControlFlags() | windowlessActivate;
}

// CCustomeMessageExampleCtrl::AboutBox - Display an "About" box to the user

void CCustomeMessageExampleCtrl::AboutBox()
{
	CDialog dlgAbout(IDD_ABOUTBOX_CUSTOMEMESSAGEEXAMPLE);
	dlgAbout.DoModal();
}



// CCustomeMessageExampleCtrl message handlers

/*VARIANT_BOOL CCustomeMessageExampleCtrl::getStudy(VARIANT &id)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// TODO: Add your dispatch handler code here

	LPCTSTR strPatientID1=CString(id);

	return VARIANT_TRUE;
}*/

VARIANT_BOOL CCustomeMessageExampleCtrl::openStudy()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// TODO: Add your dispatch handler code here
	BOOL flag = FALSE;
	CEFilm *eFilm = new CEFilm;
	

	COleException *e = new COleException;

	try 
	{
		// Create instance of Microsoft System Information Control 
		// by using ProgID.
		if (FALSE == eFilm->CreateDispatch(_T("EFilm.Document"), e))
			throw e;
		//AfxMessageBox(L"Hello there1234",0,0);


		//VariantChangeType(&strPatientID, &strPatientID, VARIANT_NOUSEROVERRIDE, VT_BSTR);
		//CString strPatientID1 = L"123";

		//VariantChangeType(&strAccessionNo, &strAccessionNo, VARIANT_NOUSEROVERRIDE, VT_BSTR);
		//CString  strAccessionNo1 = L"456";

		BOOL bCloseCurWindow=FALSE; 
		BOOL bAddToWindow=FALSE; 
		short nSeriesRows=0;
		short nSeriesCols=0; 
		short nImageRows=0; 
		short nImageCols=0; 
		BOOL bAutoSeriesFormat=TRUE; 
		BOOL bAutoImageFormat=TRUE;

		flag = eFilm->oleOpenStudy(patientId,accessionNo,bCloseCurWindow,bAddToWindow,
			nSeriesRows,nSeriesCols,nImageRows,nImageCols,bAutoSeriesFormat,bAutoImageFormat);
		
		//eFlim.oleShowSearchWindow(SW_HIDE);
		eFilm->oleShowMainWindow(SW_SHOWMAXIMIZED);
		
		

		//delete strPatientID1;
		//delete strAccessionNo1;
	}
	//Catch control-specific exceptions.
	catch (COleDispatchException * e) 
	{
		/*CString cStr;

		if (!e->m_strSource.IsEmpty())
			cStr = e->m_strSource + " - ";
		if (!e->m_strDescription.IsEmpty())
			cStr += e->m_strDescription;
		else
			cStr += "unknown error";*/

		AfxMessageBox(L"Unable to init", MB_OK, 0);

		e->Delete();
	}
	//Catch all MFC exceptions, including COleExceptions.
	// OS exceptions will not be caught.
	catch (CException *e) 
	{
		/*CString cStr;
		cStr.Format("%s(%d): OLE Execption caught: SCODE = %x", 
			__FILE__, __LINE__, COleException::Process(e));*/

		AfxMessageBox(L"Unable to init", MB_OK, 0);
		e->Delete();
	}


	delete eFilm;
	//eFlim.oleShowMainWindow(SW_RESTORE);	

	return flag;
}

/*VARIANT_BOOL CCustomeMessageExampleCtrl::openStudy1(VARIANT &strPatientID, VARIANT &strAccessionNo, VARIANT_BOOL bCloseCurWindow, VARIANT_BOOL bAddToWindow, SHORT nSeriesRows, SHORT nSeriesCols, SHORT nImageRows, SHORT nImageCols, VARIANT_BOOL bAutoSeriesFormat, VARIANT_BOOL bAutoImageFormat)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	// TODO: Add your dispatch handler code here

	CEFilm eFlim;

	eFlim.CreateDispatch(_T("efilm.document"));

	VariantChangeType(&strPatientID, &strPatientID, VARIANT_NOUSEROVERRIDE, VT_BSTR);
	CString strPatientID1((wchar_t*)strPatientID.bstrVal);

	VariantChangeType(&strAccessionNo, &strAccessionNo, VARIANT_NOUSEROVERRIDE, VT_BSTR);
	CString strAccessionNo1((wchar_t*)strAccessionNo.bstrVal);

	BOOL flag = eFlim.oleOpenStudy(strPatientID1,strAccessionNo1,bCloseCurWindow,bAddToWindow,
		nSeriesRows,nSeriesCols,nImageRows,nImageCols,bAutoSeriesFormat,bAutoImageFormat);

	return flag;
}*/
void CCustomeMessageExampleCtrl::OnClose(DWORD dwSaveOption)
{
	/*if(eFilm != NULL)
	{
		eFilm->DetachDispatch();
	}
	delete eFilm;*/
	//AfxMessageBox(L"Hello Closing",0,0);
}

/*
void CCustomeMessageExampleCtrl::closeSearchWindow()
{
	CEFilm eFlim;

	eFlim.CreateDispatch(_T("efilm.document"));

	eFlim.oleShowMainWindow(SW_MAXIMIZE);

}*/

/*STDMETHODIMP CCustomeMessageExampleCtrl::closeSearchWindow(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CEFilm eFlim;

	eFlim.CreateDispatch(_T("efilm.document"));

	eFlim.oleShowSearchWindow(SW_HIDE);

	return S_OK;
}*/
