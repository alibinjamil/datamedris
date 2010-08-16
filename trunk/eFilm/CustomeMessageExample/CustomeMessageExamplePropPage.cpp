// CustomeMessageExamplePropPage.cpp : Implementation of the CCustomeMessageExamplePropPage property page class.

#include "stdafx.h"
#include "CustomeMessageExample.h"
#include "CustomeMessageExamplePropPage.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


IMPLEMENT_DYNCREATE(CCustomeMessageExamplePropPage, COlePropertyPage)



// Message map

BEGIN_MESSAGE_MAP(CCustomeMessageExamplePropPage, COlePropertyPage)
END_MESSAGE_MAP()



// Initialize class factory and guid

IMPLEMENT_OLECREATE_EX(CCustomeMessageExamplePropPage, "CUSTOMEMESSAGE.CustomeMessagePropPage.1",
	0x5fb87d32, 0x17e4, 0x45b9, 0x84, 0x92, 0x9c, 0x4a, 0x2a, 0xdd, 0xdf, 0x55)



// CCustomeMessageExamplePropPage::CCustomeMessageExamplePropPageFactory::UpdateRegistry -
// Adds or removes system registry entries for CCustomeMessageExamplePropPage

BOOL CCustomeMessageExamplePropPage::CCustomeMessageExamplePropPageFactory::UpdateRegistry(BOOL bRegister)
{
	if (bRegister)
		return AfxOleRegisterPropertyPageClass(AfxGetInstanceHandle(),
			m_clsid, IDS_CUSTOMEMESSAGEEXAMPLE_PPG);
	else
		return AfxOleUnregisterClass(m_clsid, NULL);
}



// CCustomeMessageExamplePropPage::CCustomeMessageExamplePropPage - Constructor

CCustomeMessageExamplePropPage::CCustomeMessageExamplePropPage() :
	COlePropertyPage(IDD, IDS_CUSTOMEMESSAGEEXAMPLE_PPG_CAPTION)
{
}



// CCustomeMessageExamplePropPage::DoDataExchange - Moves data between page and properties

void CCustomeMessageExamplePropPage::DoDataExchange(CDataExchange* pDX)
{
	DDP_PostProcessing(pDX);
}



// CCustomeMessageExamplePropPage message handlers
