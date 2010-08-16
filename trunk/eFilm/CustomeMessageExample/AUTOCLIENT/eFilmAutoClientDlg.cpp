// eFilmAutoClientDlg.cpp : implementation file
//
//
// IEFilm is instantiated within OnInitDialog() and the
// Automation calls are made at the bottom of this file.
//


#include "stdafx.h"
#include "eFilmAutoClient.h"
#include "eFilmAutoClientDlg.h"
#include "DlgProxy.h"
#include ".\efilmautoclientdlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlg dialog

IMPLEMENT_DYNAMIC(CEFilmAutoClientDlg, CDialog);

CEFilmAutoClientDlg::CEFilmAutoClientDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CEFilmAutoClientDlg::IDD, pParent)
	, m_strDomainNameOrWSDLFile(_T(""))
	, m_strUsername(_T(""))
	, m_strPassword(_T(""))
	, m_strToken(_T(""))
	, m_strStudyUIDs(_T(""))
	, m_bFindRelatedStudies(FALSE)
	, m_nNumPriors(0)
	, m_strProtocols(_T(""))
	, m_bApplyProtocol(FALSE)
	, mstrExportPath(_T(""))
	, mBitmapFormat(0)
	, m_bSuppressSearch(FALSE)
	, m_strSelectServers(_T(""))
	, m_bIncludeLayoutInfo(FALSE)
{
	//{{AFX_DATA_INIT(CEFilmAutoClientDlg)
	m_strAccNums = _T("");
	m_strPatientID = _T("");
	m_nLeft = 0;
	m_nTop = 0;
	m_nBottom = 0;
	m_nRight = 0;
	m_nSeriesCols = 0;
	m_nSeriesRows = 0;
	m_nImageCols = 0;
	m_nImageRows = 0;
	m_bAddToWindow = FALSE;
	m_bCloseCurrentWindow = FALSE;
	m_bImageFormat = FALSE;
	m_bSeriesFormat = FALSE;
	m_strImageSourceUIDs = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pAutoProxy = NULL;
}

CEFilmAutoClientDlg::~CEFilmAutoClientDlg()
{
	// If there is an automation proxy for this dialog, set
	//  its back pointer to this dialog to NULL, so it knows
	//  the dialog has been deleted.
	if (m_pAutoProxy != NULL)
		m_pAutoProxy->m_pDialog = NULL;
}

bool CEFilmAutoClientDlg::PackageOpenStudyInfoXML(const CString& strStudyUIDs,
												  const CString& strPatientID,
												  const CString& strAccNums,
												  const CString& strImageSourceUIDs,
												  MSXML2::IXMLDOMDocument2Ptr& rspOpenStudyInfo)
{
	try
	{
		// Create the DOM document
		if (FAILED(::CoInitialize(NULL)))
			throw 0;
		if (FAILED(rspOpenStudyInfo.CreateInstance(__uuidof(MSXML2::DOMDocument40))))
			throw 0;

		// Load HP-AutomationOpenStudyInfo.xml
		_variant_t varXMLConf(_T("HP-AutomationOpenStudyInfo.xml"));
		if (rspOpenStudyInfo->load(varXMLConf) == VARIANT_FALSE)
		{
			AfxMessageBox("Could not open HP-AutomationOpenStudyInfo.xml");
			throw 0;
		}

		rspOpenStudyInfo->setProperty(_T("SelectionLanguage"), _T("XPath"));
		rspOpenStudyInfo->setProperty(_T("SelectionNamespaces"), _T("xmlns:hp='urn:schemas-mergeefilm-com:hanging-protocol'"));

		// Populate the rspOpenStudyInfo XML document
		MSXML2::IXMLDOMElementPtr spDOMRoot = (MSXML2::IXMLDOMElementPtr)rspOpenStudyInfo->documentElement;
		if (spDOMRoot == NULL)
			throw 0;

		// Study information
		std::vector<CString> vecAccNums = ParseDelimitedString(strAccNums);

		if (!m_strPatientID.IsEmpty() || vecAccNums.size() > 0)
		{
			// Use patient IDs + accession number to identify the study

			// PatientID
			MSXML2::IXMLDOMAttributePtr spAttribute = spDOMRoot->getAttributeNode(_T("PatientID"));
			if (spAttribute != NULL)
				spDOMRoot->setAttribute(_bstr_t(_T("PatientID")), variant_t((LPCTSTR)strPatientID));
			
			// Accession number(s)
			for (unsigned int i = 0 ; i < vecAccNums.size() ; ++i)
			{
				// Package the study information
				MSXML2::IXMLDOMDocument2Ptr spStudy = NULL;

				if (!PackageStudyXML(_T(""), vecAccNums[i], spStudy))
					throw 0;

				// Add the study document to the list
				spDOMRoot->appendChild(spStudy->documentElement);

				// Free the study XML document
				spStudy = NULL;
			}
		}
		else
		{
			// Use study UIDs to identify the study
			std::vector<CString> vecStudyUIDs = ParseDelimitedString(strStudyUIDs);

			// Study UID(s)
			for (unsigned int i = 0 ; i < vecStudyUIDs.size() ; ++i)
			{
				// Package the study information
				MSXML2::IXMLDOMDocument2Ptr spStudy = NULL;

				if (!PackageStudyXML(vecStudyUIDs[i], _T(""), spStudy))
					throw 0;

				// Add the study document to the list
				spDOMRoot->appendChild(spStudy->documentElement);

				// Free the study XML document
				spStudy = NULL;
			}
		}

		// Image sources
        std::vector<CString> vecImageSourceUIDs = ParseDelimitedString(strImageSourceUIDs);
		for (unsigned int i = 0 ; i < vecImageSourceUIDs.size() ; ++i)
		{
			// Package the image source information
			MSXML2::IXMLDOMDocument2Ptr spImageSource = NULL;

			if (!PackageImageSourceXML(vecImageSourceUIDs[i], spImageSource))
				throw 0;

			// Add the image source document to the list
			spDOMRoot->appendChild(spImageSource->documentElement);

			// Free the image source XML document
			spImageSource = NULL;
		}
	}
	catch (...)
	{
		rspOpenStudyInfo = NULL;
		return false;
	}

	return true;
}

bool CEFilmAutoClientDlg::PackageStudyXML(const CString& strStudyUID,
										  const CString& strAccessionNumber, 
										  MSXML2::IXMLDOMDocument2Ptr& rspStudy)
{
	try
	{
		// Create the DOM document
		if (FAILED(::CoInitialize(NULL)))
			throw 0;
		if (FAILED(rspStudy.CreateInstance(__uuidof(MSXML2::DOMDocument40))))
			throw 0;

		// Load HP-AutomationStudy.xml
		_variant_t varXMLConf(_T("HP-AutomationStudy.xml"));
		if (rspStudy->load(varXMLConf) == VARIANT_FALSE)
		{
			AfxMessageBox("Could not open HP-AutomationStudy.xml");
			throw 0;
		}

		rspStudy->setProperty(_T("SelectionLanguage"), _T("XPath"));

		// Populate the rspStudy XML document
		MSXML2::IXMLDOMElementPtr spDOMRoot = (MSXML2::IXMLDOMElementPtr)rspStudy->documentElement;
		if (spDOMRoot == NULL)
			throw 0;

		// StudyUID
		MSXML2::IXMLDOMAttributePtr spAttribute = spDOMRoot->getAttributeNode(_T("StudyUID"));
		if (spAttribute != NULL)
			spDOMRoot->setAttribute(_bstr_t(_T("StudyUID")), variant_t((LPCTSTR)strStudyUID));

		// AccessionNumber
		spAttribute = spDOMRoot->getAttributeNode(_T("AccessionNumber"));
		if (spAttribute != NULL)
			spDOMRoot->setAttribute(_bstr_t(_T("AccessionNumber")), variant_t((LPCTSTR)strAccessionNumber));
	}
	catch (...)
	{
		rspStudy = NULL;
		return false;
	}

	return true;
}

bool CEFilmAutoClientDlg::PackageImageSourceXML(const CString& strGUID,
												MSXML2::IXMLDOMDocument2Ptr& rspImageSource)
{
	try
	{
		// Create the DOM document
		if (FAILED(::CoInitialize(NULL)))
			throw 0;
		if (FAILED(rspImageSource.CreateInstance(__uuidof(MSXML2::DOMDocument40))))
			throw 0;

		// Load HP-AutomationImageSource.xml
		_variant_t varXMLConf(_T("HP-AutomationImageSource.xml"));
		if (rspImageSource->load(varXMLConf) == VARIANT_FALSE)
		{
			AfxMessageBox("Could not open HP-AutomationImageSource.xml");
			throw 0;
		}

		rspImageSource->setProperty(_T("SelectionLanguage"), _T("XPath"));

		// Populate the rspImageSource XML document
		MSXML2::IXMLDOMElementPtr spDOMRoot = (MSXML2::IXMLDOMElementPtr)rspImageSource->documentElement;
		if (spDOMRoot == NULL)
			throw 0;

		// GUID
		MSXML2::IXMLDOMAttributePtr spAttribute = spDOMRoot->getAttributeNode(_T("GUID"));
		if (spAttribute != NULL)
			spDOMRoot->setAttribute(_bstr_t(_T("GUID")), variant_t((LPCTSTR)strGUID));
	}
	catch (...)
	{
		rspImageSource = NULL;
		return false;
	}

	return true;
}

bool CEFilmAutoClientDlg::PackageProtocolListXML(const CString& strProtocolFilenames,
												 MSXML2::IXMLDOMDocument2Ptr& rspProtocolList)
{
	try
	{
		// Create the DOM document
		if (FAILED(::CoInitialize(NULL)))
			throw 0;
		if (FAILED(rspProtocolList.CreateInstance(__uuidof(MSXML2::DOMDocument40))))
			throw 0;

		// Load HP-ProtocolList.xml
		_variant_t varXMLConf(_T("HP-ProtocolList.xml"));
		if (rspProtocolList->load(varXMLConf) == VARIANT_FALSE)
		{
			AfxMessageBox("Could not open HP-ProtocolList.xml");
			throw 0;
		}

		rspProtocolList->setProperty(_T("SelectionLanguage"), _T("XPath"));
		rspProtocolList->setProperty(_T("SelectionNamespaces"), _T("xmlns:hp='urn:schemas-mergeefilm-com:hanging-protocol'"));

		// Populate the rspProtocolList XML document
		MSXML2::IXMLDOMElementPtr spDOMRoot = (MSXML2::IXMLDOMElementPtr)rspProtocolList->documentElement;
		if (spDOMRoot == NULL)
			throw 0;

		// Remove the "AssociatedList" element
		MSXML2::IXMLDOMNodePtr spAssociatedList = spDOMRoot->selectSingleNode(_T("hp:AssociatedList"));
		if (spAssociatedList != NULL)
			spDOMRoot->removeChild(spAssociatedList);

		// Protocols
        std::vector<CString> vecProtocolFilenames = ParseDelimitedString(strProtocolFilenames);
		for (unsigned int i = 0 ; i < vecProtocolFilenames.size() ; ++i)
		{
			// Package the protocol information
			MSXML2::IXMLDOMDocument2Ptr spProtocol = NULL;

			// Create the protocol DOM document
			if (FAILED(::CoInitialize(NULL)))
				throw 0;
			if (FAILED(spProtocol.CreateInstance(__uuidof(MSXML2::DOMDocument40))))
				throw 0;

			// Load the protocol
			_variant_t varXMLConf(vecProtocolFilenames[i]);
			if (spProtocol->load(varXMLConf) == VARIANT_FALSE)
			{
				AfxMessageBox(CString("Could not open protocol: ") + vecProtocolFilenames[i]);
				throw 0;
			}

			spProtocol->setProperty(_T("SelectionLanguage"), _T("XPath"));
			spProtocol->setProperty(_T("SelectionNamespaces"), _T("xmlns:hp='urn:schemas-mergeefilm-com:hanging-protocol'"));

			// Add the protocol document to the list
			spDOMRoot->appendChild(spProtocol->documentElement);

			// Free the protocol XML document
			spProtocol = NULL;
		}
	}
	catch (...)
	{
		rspProtocolList = NULL;
		return false;
	}

	return true;
}

// Parses a string containing substrings delimited by a given delimiter
std::vector<CString> CEFilmAutoClientDlg::ParseDelimitedString(CString str, TCHAR delimiter)
{
	std::vector<CString> vecSubstrings;

	str.TrimLeft();
	str.TrimRight();

	while(1)
	{
		int nIndex = str.Find(delimiter);
		if (nIndex != -1)
		{
			CString strSub = str.Left(nIndex);
			str = str.Right(str.GetLength() - nIndex - 1);

			str.TrimLeft();
			strSub.TrimRight();

			vecSubstrings.push_back(strSub);
		}
		else 
		{
			if (!str.IsEmpty())
				vecSubstrings.push_back(str);

			break;
		}
	}

	return vecSubstrings;
}

void CEFilmAutoClientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CEFilmAutoClientDlg)
	DDX_Control(pDX, IDC_SPINSERIESROW, m_spinSeriesRow);
	DDX_Control(pDX, IDC_SPINSERIESCOL, m_spinSeriesCol);
	DDX_Control(pDX, IDC_SPINIMAGEROW, m_spinImageRow);
	DDX_Control(pDX, IDC_SPINIMAGECOL, m_spinImageCol);
	DDX_Control(pDX, IDC_SPINTOP, m_spinTop);
	DDX_Control(pDX, IDC_SPINRIGHT, m_spinRight);
	DDX_Control(pDX, IDC_SPINLEFT, m_spinLeft);
	DDX_Control(pDX, IDC_SPINBOTTOM, m_spinBottom);
	DDX_Text(pDX, IDC_ACCNUM, m_strAccNums);
	DDX_Text(pDX, IDC_PATIENTID, m_strPatientID);
	DDX_Text(pDX, IDC_LEFT, m_nLeft);
	DDX_Text(pDX, IDC_TOP, m_nTop);
	DDX_Text(pDX, IDC_BOTTOM, m_nBottom);
	DDX_Text(pDX, IDC_RIGHT, m_nRight);
	DDX_Text(pDX, IDC_SERIESCOLS, m_nSeriesCols);
	DDX_Text(pDX, IDC_SERIESROWS, m_nSeriesRows);
	DDX_Text(pDX, IDC_IMAGECOLS, m_nImageCols);
	DDX_Text(pDX, IDC_IMAGEROWS, m_nImageRows);
	DDX_Check(pDX, IDC_CHECKADD, m_bAddToWindow);
	DDX_Check(pDX, IDC_CHECKCLOSE, m_bCloseCurrentWindow);
	DDX_Check(pDX, IDC_CHECKIMAGE, m_bImageFormat);
	DDX_Check(pDX, IDC_CHECKSERIES, m_bSeriesFormat);
	DDX_Text(pDX, IDC_IMAGE_SOURCE, m_strImageSourceUIDs);
	//}}AFX_DATA_MAP
	DDX_Text(pDX, IDC_DOMAIN_NAME_OR_WSDL_FILE, m_strDomainNameOrWSDLFile);
	DDX_Text(pDX, IDC_USERNAME, m_strUsername);
	DDX_Text(pDX, IDC_PASSWORD, m_strPassword);
	DDX_Text(pDX, IDC_TOKEN, m_strToken);
	DDX_Text(pDX, IDC_STUDY_UIDS, m_strStudyUIDs);
	DDX_Check(pDX, IDC_FIND_RELATED_STUDIES, m_bFindRelatedStudies);
	DDX_Text(pDX, IDC_NUM_PRIORS, m_nNumPriors);
	DDX_Text(pDX, IDC_PROTOCOLS, m_strProtocols);
	DDX_Control(pDX, IDC_SPIN_PRIORS, m_spinPriors);
	DDX_Check(pDX, IDC_APPLY_PROTOCOL, m_bApplyProtocol);
	DDX_Text(pDX, IDC_EXPORT_PATH, mstrExportPath);
	DDX_Text(pDX, IDC_BITMAP_FORMAT, mBitmapFormat);
	DDV_MinMaxInt(pDX, mBitmapFormat, 0, 10);
	DDX_Check(pDX, IDC_SUPPRESS_SEARCH, m_bSuppressSearch);
	DDX_Text(pDX, IDC_EDIT_SELECT_SERVERS, m_strSelectServers);
	DDX_Check(pDX, IDC_INCLUDE_LAYOUT_INFO, m_bIncludeLayoutInfo);
}

BEGIN_MESSAGE_MAP(CEFilmAutoClientDlg, CDialog)
	//{{AFX_MSG_MAP(CEFilmAutoClientDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CLOSE()
	ON_BN_CLICKED(ID_OPENSTUDY, OnOpenStudy)
	ON_BN_CLICKED(ID_SHOW, OnShowEFilm)
	ON_BN_CLICKED(ID_MINIMIZE, OnMinimizeEFilm)
	ON_BN_CLICKED(ID_POSITION, OnPosition)
	ON_BN_CLICKED(ID_SHOWSEARCH, OnShowSearch)
	ON_BN_CLICKED(ID_HIDESEARCH, OnHideSearch)
	ON_BN_CLICKED(ID_HIDE, OnHideEFilm)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_LOGIN, OnLogin)
	ON_BN_CLICKED(IDC_LOGIN_WITH_TOKEN, OnLoginWithToken)
	ON_BN_CLICKED(IDC_SEARCH, OnSearch)
	ON_BN_CLICKED(ID_EXPORT, OnBnClickedExport)
	ON_BN_CLICKED(IDC_LOGOUT, OnLogout)
	ON_BN_CLICKED(IDC_LOCK, OnLock)
	ON_BN_CLICKED(IDC_UNLOCK, OnUnlock)
	ON_BN_CLICKED(IDC_ISLOCKED, OnIsLocked)
	ON_BN_CLICKED(ID_SELECTSERVERS, OnBnClickedSelectservers)
	ON_BN_CLICKED(IDC_APPLY_PROTOCOL, OnBnClickedApplyProtocol)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CEFilmAutoClientDlg message handlers

BOOL CEFilmAutoClientDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	((CButton*)GetDlgItem(IDC_DOMAIN))->SetCheck(1);
	((CButton*)GetDlgItem(IDC_FUSION))->SetCheck(0);

	// Set range for the spin controls
	m_spinTop.SetRange(0, 1200);
	m_spinBottom.SetRange(0, 1200);
	m_spinLeft.SetRange(0, 1600);
	m_spinRight.SetRange(0, 1600);

	m_spinSeriesRow.SetRange(0, 50);
	m_spinSeriesCol.SetRange(0, 50);
	m_spinImageRow.SetRange(0, 50);
	m_spinImageCol.SetRange(0, 50);

	m_spinPriors.SetRange(0, 100);

	// Set acceleration for the spin controls
	const int nNumAccel = 2;
	UDACCEL accel[nNumAccel];

	accel[0].nSec = 0;
	accel[0].nInc = 10;

	accel[1].nSec = 1;
	accel[1].nInc = 50;

	m_spinTop.SetAccel(nNumAccel, accel);
	m_spinBottom.SetAccel(nNumAccel, accel);
	m_spinLeft.SetAccel(nNumAccel, accel);
	m_spinRight.SetAccel(nNumAccel, accel);

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	char buffer[1025];
	GetCurrentDirectory(1025, buffer);

	//AfxMessageBox(buffer);

	// Create the dispatch connection
	m_pEfilm = new IEFilm;

	COleException *e = new COleException;

	try 
	{
		// Create instance of Microsoft System Information Control 
		// by using ProgID.
		if (FALSE == m_pEfilm->CreateDispatch("EFilm.Document", e))
			throw e;
	}
	//Catch control-specific exceptions.
	catch (COleDispatchException * e) 
	{
		CString cStr;

		if (!e->m_strSource.IsEmpty())
			cStr = e->m_strSource + " - ";
		if (!e->m_strDescription.IsEmpty())
			cStr += e->m_strDescription;
		else
			cStr += "unknown error";

		AfxMessageBox(cStr, MB_OK, 
			(e->m_strHelpFile.IsEmpty())? 0:e->m_dwHelpContext);

		e->Delete();
	}
	//Catch all MFC exceptions, including COleExceptions.
	// OS exceptions will not be caught.
	catch (CException *e) 
	{
		CString cStr;
		cStr.Format("%s(%d): OLE Execption caught: SCODE = %x", 
			__FILE__, __LINE__, COleException::Process(e));

		AfxMessageBox(cStr, MB_OK);
		e->Delete();
	}
	
	m_strImageSourceUIDs = "{0CBB4846-0868-4f42-8AC3-63F5B8822AF6}";
	UpdateData(FALSE);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CEFilmAutoClientDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CEFilmAutoClientDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CEFilmAutoClientDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

// Automation servers should not exit when a user closes the UI
//  if a controller still holds on to one of its objects.  These
//  message handlers make sure that if the proxy is still in use,
//  then the UI is hidden but the dialog remains around if it
//  is dismissed.

void CEFilmAutoClientDlg::OnClose() 
{
	if (CanExit())
		CDialog::OnClose();
}

void CEFilmAutoClientDlg::OnOK() 
{
	if (CanExit())
		CDialog::OnOK();
}

void CEFilmAutoClientDlg::OnCancel() 
{
	delete m_pEfilm;

	if (CanExit())
		CDialog::OnCancel();
}

BOOL CEFilmAutoClientDlg::CanExit()
{
	// If the proxy object is still around, then the automation
	//  controller is still holding on to this application.  Leave
	//  the dialog around, but hide its UI.
	if (m_pAutoProxy != NULL)
	{
		ShowWindow(SW_HIDE);
		return FALSE;
	}

	return TRUE;
}



////////////////////////////////////////////////////////////////////
//  The following functions make the Automation calls
////////////////////////////////////////////////////////////////////

void CEFilmAutoClientDlg::OnOpenStudy() 
{
	UpdateData();

//	if(!m_pEfilm->oleLoginViaFusion(_T("admin"), _T("password"), _T("http://abolourianxp/fusionservices/profiles.asmx?wsdl")))
//	{
//		AfxMessageBox("failed to login");
//		return;
//	}

	// Determine what oleOpenStudy function to call
	if (m_bApplyProtocol)
	{
		// oleOpenStudy4
		if (m_strImageSourceUIDs == "" || (m_strStudyUIDs == "" && m_strPatientID == "" && m_strAccNums == ""))
		{
			AfxMessageBox("Please supply more study information");
			return;
		}

		// Package the AutomationOpenStudyInfo XML document
		MSXML2::IXMLDOMDocument2Ptr spOpenStudyInfo;
		if (!PackageOpenStudyInfoXML(m_strStudyUIDs, m_strPatientID, m_strAccNums, m_strImageSourceUIDs, spOpenStudyInfo))
		{
			AfxMessageBox("Failed to build study list.");
			return;
		}

		// Package the ProtocolList XML document
		MSXML2::IXMLDOMDocument2Ptr spProtocolList;
		if (!PackageProtocolListXML(m_strProtocols, spProtocolList))
		{
			AfxMessageBox("Failed to build protocol list.");
			return;
		}

		BSTR strOpenStudyInfoXML = NULL;
		BSTR strProtocolListXML = NULL;

		try
		{
			spOpenStudyInfo->get_xml(&strOpenStudyInfoXML);
			spProtocolList->get_xml(&strProtocolListXML);
		}
		catch (...)
		{
			if (strOpenStudyInfoXML != NULL)
				SysFreeString(strOpenStudyInfoXML);
			if (strProtocolListXML != NULL)
				SysFreeString(strProtocolListXML);

			AfxMessageBox("Failed to get study/protocol list XML.");
			return;
		}
		
		if (!m_bIncludeLayoutInfo)
		{
			if (!m_pEfilm->oleOpenStudy4((LPCTSTR)_bstr_t(strOpenStudyInfoXML), 
										m_bCloseCurrentWindow, 
										m_bFindRelatedStudies, 
										m_nNumPriors, 
										(LPCTSTR)_bstr_t(strProtocolListXML)))
			{
				AfxMessageBox("oleOpenStudy4 failed.");
			}
		}
		else
		{
			if (!m_pEfilm->oleOpenStudy5((LPCTSTR)_bstr_t(strOpenStudyInfoXML), 
										m_nSeriesRows, m_nSeriesCols, m_nImageRows, m_nImageCols,
										m_bSuppressSearch, m_bCloseCurrentWindow, m_bFindRelatedStudies, m_nNumPriors,
										m_bApplyProtocol, (LPCTSTR)_bstr_t(strProtocolListXML)))
			{
				AfxMessageBox("oleOpenStudy5 failed.");
			}
		}

		SysFreeString(strOpenStudyInfoXML);
		SysFreeString(strProtocolListXML);
	}
	else if (m_strImageSourceUIDs != "" && m_strStudyUIDs != "")
	{
		// oleOpenStudy3
		if (!m_pEfilm->oleOpenStudy3(m_strPatientID, m_strAccNums, m_strStudyUIDs, 
									 m_bCloseCurrentWindow, m_bAddToWindow,
									 m_nSeriesRows, m_nSeriesCols, m_nImageRows, m_nImageCols, 
									 m_bSeriesFormat, m_bImageFormat, m_strImageSourceUIDs))
		{
			AfxMessageBox("oleOpenStudy3 failed.");
		}
	}
	else if (m_strImageSourceUIDs != "" && (m_strPatientID != "" || m_strAccNums != ""))
	{
		// oleOpenStudy2
		if (!m_pEfilm->oleOpenStudy2(m_strPatientID, m_strAccNums, 
									 m_bCloseCurrentWindow, m_bAddToWindow,
									 m_nSeriesRows, m_nSeriesCols, m_nImageRows, m_nImageCols, 
									 m_bSeriesFormat, m_bImageFormat, m_strImageSourceUIDs))
		{
			AfxMessageBox("oleOpenStudy2 failed.");
		}
	}
	else if (m_strPatientID != "" || m_strAccNums != "")
	{	
		// oleOpenStudy
		if (!m_pEfilm->oleOpenStudy(m_strPatientID, m_strAccNums, 
									m_bCloseCurrentWindow, m_bAddToWindow,
									m_nSeriesRows, m_nSeriesCols, m_nImageRows, m_nImageCols, 
									m_bSeriesFormat, m_bImageFormat))
		{
			AfxMessageBox("oleOpenStudy failed.");
		}
	}
	else
		AfxMessageBox("Please supply more study information");
}

void CEFilmAutoClientDlg::OnShowEFilm() 
{
	m_pEfilm->oleShowMainWindow(SW_SHOWNORMAL);
}

void CEFilmAutoClientDlg::OnHideEFilm() 
{
	m_pEfilm->oleShowMainWindow(SW_HIDE);
}

void CEFilmAutoClientDlg::OnMinimizeEFilm() 
{
	m_pEfilm->oleShowMainWindow(SW_MINIMIZE);
}

void CEFilmAutoClientDlg::OnPosition() 
{
	UpdateData(TRUE);
	m_pEfilm->olePositionMainWindow(m_nLeft,m_nTop,m_nRight,m_nBottom);
}

void CEFilmAutoClientDlg::OnShowSearch() 
{
	m_pEfilm->oleShowSearchWindow(SW_SHOWNORMAL);
}

void CEFilmAutoClientDlg::OnHideSearch() 
{
	m_pEfilm->oleShowSearchWindow(SW_HIDE);
}


void CEFilmAutoClientDlg::OnLogin()
{
	UpdateData();

	if(((CButton*)GetDlgItem(IDC_DOMAIN))->GetCheck())
	{
		if(!m_pEfilm->oleLoginViaDomain(m_strUsername, m_strPassword, m_strDomainNameOrWSDLFile))
			AfxMessageBox(_T("Login failed."));
	}
	else if(((CButton*)GetDlgItem(IDC_FUSION))->GetCheck())
	{
		if(!m_pEfilm->oleLoginViaFusion(m_strUsername, m_strPassword, m_strDomainNameOrWSDLFile))
			AfxMessageBox(_T("Login failed."));
	}
	else
		AfxMessageBox(_T("Please select a login type (i.e. Domain or FUSION)"));
}

void CEFilmAutoClientDlg::OnLoginWithToken()
{
	UpdateData();

	if(((CButton*)GetDlgItem(IDC_DOMAIN))->GetCheck())
	{
		AfxMessageBox(_T("Logging in with a token using a domain account is not permitted."));
	}
	else if(((CButton*)GetDlgItem(IDC_FUSION))->GetCheck())
	{
		if(!m_pEfilm->oleLoginViaFusionWithToken(m_strUsername, m_strToken, m_strDomainNameOrWSDLFile))
			AfxMessageBox(_T("Login failed."));
	}
	else
		AfxMessageBox(_T("Please select a login type (i.e. Domain or FUSION)"));
}

void CEFilmAutoClientDlg::OnSearch()
{
	UpdateData();

	char* searchParams = "<SEARCH PatientID = \"\" PatientName = \"\" PatientSex = \"\" BirthDate = \"\" AccessionNumber = \"\" StudyInstanceUID = \"\"/>";
	CString strSearchParams = "<SEARCH PatientID = \"" + m_strPatientID + "\" PatientName = \"\" PatientSex = \"\" BirthDate = \"\" AccessionNumber = \"" + m_strAccNums + "\" StudyInstanceUID = \"" + m_strStudyUIDs + "\"/>";

	VARIANT v_results;
	VariantInit(&v_results);

	m_pEfilm->oleSearch(strSearchParams, &v_results, m_strImageSourceUIDs);
	if (V_VT(&v_results) == VT_BSTR)

	{
		CString display(V_BSTR(&v_results));
		AfxMessageBox(display);
		SysFreeString(V_BSTR(&v_results));
	}
}

void CEFilmAutoClientDlg::OnBnClickedExport()
{
	// TODO: Add your control notification handler code here
	UpdateData();

//	CString msg;
//	msg.Format("path: %s\tformat: %d", mstrExportPath, mBitmapFormat);
//	AfxMessageBox(msg);

	BOOL retval = m_pEfilm->oleExportAsBitmap(mstrExportPath, mBitmapFormat);
	if (!retval)
	{
		AfxMessageBox("Export failed");
	}
}

void CEFilmAutoClientDlg::OnLogout()
{
	UpdateData();

	if(!m_pEfilm->oleLogout())
		AfxMessageBox(_T("Logout failed."));
}

void CEFilmAutoClientDlg::OnLock()
{
	if(!m_pEfilm->oleLock())
		AfxMessageBox(_T("Lock failed."));
}

void CEFilmAutoClientDlg::OnUnlock()
{
	if(!m_pEfilm->oleUnlock())
		AfxMessageBox(_T("Unlock failed."));
}

void CEFilmAutoClientDlg::OnIsLocked()
{
	if(m_pEfilm->oleIsLocked())
		AfxMessageBox(_T("eFilm is locked."));
	else
		AfxMessageBox(_T("eFilm is not locked."));
}

void CEFilmAutoClientDlg::OnBnClickedApplyProtocol()
{
	UpdateData();
	GetDlgItem(IDC_INCLUDE_LAYOUT_INFO)->EnableWindow(m_bApplyProtocol);
	if (!m_bApplyProtocol)
	{
		m_bIncludeLayoutInfo = FALSE;
		UpdateData(FALSE);
	}
}

void CEFilmAutoClientDlg::OnBnClickedSelectservers()
{
	UpdateData();

	if (m_strImageSourceUIDs.Find(_T(';')) == 0)
	{
		AfxMessageBox("Please specify a single image source!");
	}

	if (!m_pEfilm->oleSelectServers(m_strImageSourceUIDs, m_strSelectServers))
	{
		AfxMessageBox("oleSelectServers failed!");
	}
}
