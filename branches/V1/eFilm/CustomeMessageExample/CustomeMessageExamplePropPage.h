#pragma once

// CustomeMessageExamplePropPage.h : Declaration of the CCustomeMessageExamplePropPage property page class.


// CCustomeMessageExamplePropPage : See CustomeMessageExamplePropPage.cpp for implementation.

class CCustomeMessageExamplePropPage : public COlePropertyPage
{
	DECLARE_DYNCREATE(CCustomeMessageExamplePropPage)
	DECLARE_OLECREATE_EX(CCustomeMessageExamplePropPage)

// Constructor
public:
	CCustomeMessageExamplePropPage();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_CUSTOMEMESSAGEEXAMPLE };

// Implementation
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Message maps
protected:
	DECLARE_MESSAGE_MAP()
};

