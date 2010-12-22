#pragma once

// CustomeMessageExampleCtrl.h : Declaration of the CCustomeMessageExampleCtrl ActiveX Control class.
#include "CEFilm.h"

// CCustomeMessageExampleCtrl : See CustomeMessageExampleCtrl.cpp for implementation.

class CCustomeMessageExampleCtrl : public COleControl
{
	DECLARE_DYNCREATE(CCustomeMessageExampleCtrl)

// Constructor
public:
	CCustomeMessageExampleCtrl();

// Overrides
public:
	virtual void OnDraw(CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid);
	virtual void DoPropExchange(CPropExchange* pPX);
	virtual void OnResetState();
	virtual DWORD GetControlFlags();
	virtual void OnClose(DWORD dwSaveOption);
	//virtual void OnInit
	//void closeSearchWindow();

// Implementation
protected:
	~CCustomeMessageExampleCtrl();

	DECLARE_OLECREATE_EX(CCustomeMessageExampleCtrl)    // Class factory and guid
	DECLARE_OLETYPELIB(CCustomeMessageExampleCtrl)      // GetTypeInfo
	DECLARE_PROPPAGEIDS(CCustomeMessageExampleCtrl)     // Property page IDs
	DECLARE_OLECTLTYPE(CCustomeMessageExampleCtrl)		// Type name and misc status

// Message maps
	DECLARE_MESSAGE_MAP()

// Dispatch maps
	DECLARE_DISPATCH_MAP()

	afx_msg void AboutBox();

// Event maps
	DECLARE_EVENT_MAP()

// Dispatch and event IDs
public:
	enum {
		dispidopenStudy1 = 3L,
		dispidopenStudy = 2L,
		dispidgetStudy = 1L
	};
protected:
//	VARIANT_BOOL getStudy(VARIANT &id);
	VARIANT_BOOL openStudy();
//	VARIANT_BOOL openStudy1(VARIANT &strPatientID, VARIANT &strAccessionNo, VARIANT_BOOL bCloseCurWindow, VARIANT_BOOL bAddToWindow, SHORT nSeriesRows, SHORT nSeriesCols, SHORT nImageRows, SHORT nImageCols, VARIANT_BOOL bAutoSeriesFormat, VARIANT_BOOL bAutoImageFormat);
	
public:
//	STDMETHOD(closeSearchWindow)(void);
//	CString FileName;
protected: 
	//CEFilm *eFilm;
	//void initEFilm();
	CString patientId, accessionNo;
};

