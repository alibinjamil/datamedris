// CustomeMessageExample.cpp : Implementation of CCustomeMessageExampleApp and DLL registration.

#include "stdafx.h"
#include "CustomeMessageExample.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


CCustomeMessageExampleApp theApp;

const GUID CDECL BASED_CODE _tlid =
		{ 0xD5D93889, 0xF92, 0x4927, { 0x87, 0x60, 0xF6, 0x56, 0x4F, 0x54, 0x9, 0xA1 } };
const WORD _wVerMajor = 1;
const WORD _wVerMinor = 0;



// CCustomeMessageExampleApp::InitInstance - DLL initialization

BOOL CCustomeMessageExampleApp::InitInstance()
{
	BOOL bInit = COleControlModule::InitInstance();

	if (bInit)
	{
		// TODO: Add your own module initialization code here.
	}

	return bInit;
}



// CCustomeMessageExampleApp::ExitInstance - DLL termination

int CCustomeMessageExampleApp::ExitInstance()
{
	// TODO: Add your own module termination code here.

	return COleControlModule::ExitInstance();
}



// DllRegisterServer - Adds entries to the system registry

STDAPI DllRegisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleRegisterTypeLib(AfxGetInstanceHandle(), _tlid))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(TRUE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}



// DllUnregisterServer - Removes entries from the system registry

STDAPI DllUnregisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleUnregisterTypeLib(_tlid, _wVerMajor, _wVerMinor))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(FALSE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}
