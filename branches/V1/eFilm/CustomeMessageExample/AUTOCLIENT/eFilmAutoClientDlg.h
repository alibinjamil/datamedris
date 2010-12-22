// eFilmAutoClientDlg.h : header file
//

#if !defined(AFX_EFILMAUTOCLIENTDLG_H__2F982BA4_375D_4071_A17D_AC0A4AD4D76A__INCLUDED_)
#define AFX_EFILMAUTOCLIENTDLG_H__2F982BA4_375D_4071_A17D_AC0A4AD4D76A__INCLUDED_


// Include the Class generated from the Type Library
#include "IEFilm.h"
#include "afxcmn.h"
#include <vector>

#import "msxml4.dll"

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CEFilmAutoClientDlgAutoProxy;

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlg dialog

class CEFilmAutoClientDlg : public CDialog
{
	DECLARE_DYNAMIC(CEFilmAutoClientDlg);
	friend class CEFilmAutoClientDlgAutoProxy;

// Construction
public:
	CEFilmAutoClientDlg(CWnd* pParent = NULL);	// standard constructor
	virtual ~CEFilmAutoClientDlg();

// Dialog Data
	//{{AFX_DATA(CEFilmAutoClientDlg)
	enum { IDD = IDD_EFILMAUTOCLIENT_DIALOG };
	CSpinButtonCtrl	m_spinSeriesRow;
	CSpinButtonCtrl	m_spinSeriesCol;
	CSpinButtonCtrl	m_spinImageRow;
	CSpinButtonCtrl	m_spinImageCol;
	CSpinButtonCtrl	m_spinTop;
	CSpinButtonCtrl	m_spinRight;
	CSpinButtonCtrl	m_spinLeft;
	CSpinButtonCtrl	m_spinBottom;
	CString	m_strAccNums;
	CString	m_strPatientID;
	short	m_nLeft;
	short	m_nTop;
	short	m_nBottom;
	short	m_nRight;
	short	m_nSeriesCols;
	short	m_nSeriesRows;
	short	m_nImageCols;
	short	m_nImageRows;
	BOOL	m_bAddToWindow;
	BOOL	m_bCloseCurrentWindow;
	BOOL	m_bImageFormat;
	BOOL	m_bSeriesFormat;
	CString	m_strImageSourceUIDs;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CEFilmAutoClientDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	bool PackageOpenStudyInfoXML(const CString& strStudyUIDs,
								 const CString& strPatientID,
								 const CString& strAccNums,
								 const CString& strImageSourceUIDs,
								 MSXML2::IXMLDOMDocument2Ptr& rspStudyList);

	bool PackageStudyXML(const CString& strStudyUID,
						 const CString& strAccessionNumber, 
						 MSXML2::IXMLDOMDocument2Ptr& rspStudy);

	bool PackageImageSourceXML( const CString& strGUID, 
								MSXML2::IXMLDOMDocument2Ptr& rspImageSource);

	bool PackageProtocolListXML(const CString& strProtocolFilenames,
								MSXML2::IXMLDOMDocument2Ptr& rspProtocolList);

	std::vector<CString> ParseDelimitedString(CString str, TCHAR delimiter = _TCHAR(';'));

	CEFilmAutoClientDlgAutoProxy* m_pAutoProxy;
	HICON m_hIcon;

	IEFilm* m_pEfilm;

	BOOL CanExit();

	// Generated message map functions
	//{{AFX_MSG(CEFilmAutoClientDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnClose();
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnOpenStudy();
	afx_msg void OnShowEFilm();
	afx_msg void OnMinimizeEFilm();
	afx_msg void OnPosition();
	afx_msg void OnShowSearch();
	afx_msg void OnHideSearch();
	afx_msg void OnHideEFilm();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnLogin();
	afx_msg void OnLoginWithToken();
	CString m_strDomainNameOrWSDLFile;
	CString m_strUsername;
	CString m_strPassword;
	CString m_strToken;
	CString m_strStudyUIDs;
	BOOL m_bFindRelatedStudies;
	int m_nNumPriors;
	CString m_strProtocols;
	CSpinButtonCtrl m_spinPriors;
	BOOL m_bApplyProtocol;
	afx_msg void OnSearch();
private:
	CString mstrExportPath;
public:
	int mBitmapFormat;
	afx_msg void OnBnClickedExport();
	afx_msg void OnLogout();
	afx_msg void OnLock();
	afx_msg void OnUnlock();
	afx_msg void OnIsLocked();

	BOOL m_bSuppressSearch;
	BOOL m_bIncludeLayoutInfo;
	afx_msg void OnBnClickedApplyProtocol();

	CString m_strSelectServers;
	afx_msg void OnBnClickedSelectservers();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_EFILMAUTOCLIENTDLG_H__2F982BA4_375D_4071_A17D_AC0A4AD4D76A__INCLUDED_)
