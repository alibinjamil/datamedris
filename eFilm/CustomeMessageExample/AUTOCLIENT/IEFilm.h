// Machine generated IDispatch wrapper class(es) created with Add Class from Typelib Wizard

// IEFilm wrapper class

class IEFilm : public COleDispatchDriver
{
public:
	IEFilm(){} // Calls COleDispatchDriver default constructor
	IEFilm(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	IEFilm(const IEFilm& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

	// Attributes
public:

	// Operations
public:


	// IEFilm methods
public:
	BOOL oleOpenStudy(LPCTSTR strPatientID, LPCTSTR strAccessionNo, BOOL bCloseCurWindow, BOOL bAddToWindow, short nSeriesRows, short nSeriesCols, short nImageRows, short nImageCols, BOOL bAutoSeriesFormat, BOOL bAutoImageFormat)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BOOL VTS_BOOL VTS_I2 VTS_I2 VTS_I2 VTS_I2 VTS_BOOL VTS_BOOL ;
		InvokeHelper(0x1, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strPatientID, strAccessionNo, bCloseCurWindow, bAddToWindow, nSeriesRows, nSeriesCols, nImageRows, nImageCols, bAutoSeriesFormat, bAutoImageFormat);
		return result;
	}
	BOOL oleShowMainWindow(long nCmdShow)
	{
		BOOL result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x2, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, nCmdShow);
		return result;
	}
	BOOL oleShowSearchWindow(long nCmdShow)
	{
		BOOL result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x3, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, nCmdShow);
		return result;
	}
	BOOL olePositionMainWindow(short left, short top, short right, short bottom)
	{
		BOOL result;
		static BYTE parms[] = VTS_I2 VTS_I2 VTS_I2 VTS_I2 ;
		InvokeHelper(0x4, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, left, top, right, bottom);
		return result;
	}
	BOOL oleSetForegroundWindow()
	{
		BOOL result;
		InvokeHelper(0x5, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleOpenSearchRemote(LPCTSTR strPatientID, LPCTSTR strAccessionNo)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR ;
		InvokeHelper(0x6, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strPatientID, strAccessionNo);
		return result;
	}
	BOOL oleOpenStudy2(LPCTSTR strPatientID, LPCTSTR strAccessionNo, BOOL bCloseCurWindow, BOOL bAddToWindow, short nSeriesRows, short nSeriesCols, short nImageRows, short nImageCols, BOOL bAutoSeriesFormat, BOOL bAutoImageFormat, LPCTSTR strImageSource)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BOOL VTS_BOOL VTS_I2 VTS_I2 VTS_I2 VTS_I2 VTS_BOOL VTS_BOOL VTS_BSTR ;
		InvokeHelper(0x7, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strPatientID, strAccessionNo, bCloseCurWindow, bAddToWindow, nSeriesRows, nSeriesCols, nImageRows, nImageCols, bAutoSeriesFormat, bAutoImageFormat, strImageSource);
		return result;
	}
	BOOL oleCloseAllWindows()
	{
		BOOL result;
		InvokeHelper(0x8, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleCloseCurrentWindow()
	{
		BOOL result;
		InvokeHelper(0x9, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleOpenStudy3(LPCTSTR strPatientID, LPCTSTR strAccessionNo, LPCTSTR strStudyInstanceUID, BOOL bCloseCurWindow, BOOL bAddToWindow, short nSeriesRows, short nSeriesCols, short nImageRows, short nImageCols, BOOL bAutoSeriesFormat, BOOL bAutoImageFormat, LPCTSTR strImageSource)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BSTR VTS_BOOL VTS_BOOL VTS_I2 VTS_I2 VTS_I2 VTS_I2 VTS_BOOL VTS_BOOL VTS_BSTR ;
		InvokeHelper(0xa, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strPatientID, strAccessionNo, strStudyInstanceUID, bCloseCurWindow, bAddToWindow, nSeriesRows, nSeriesCols, nImageRows, nImageCols, bAutoSeriesFormat, bAutoImageFormat, strImageSource);
		return result;
	}
	BOOL oleSearch(LPCTSTR searchParams, VARIANT * searchResults, LPCTSTR lpszImageSource)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_PVARIANT VTS_BSTR ;
		InvokeHelper(0xb, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, searchParams, searchResults, lpszImageSource);
		return result;
	}
	BOOL oleOpenSearch(LPCTSTR strPatientID, LPCTSTR strAccessionNo, LPCTSTR lpszImageSource)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BSTR ;
		InvokeHelper(0xc, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strPatientID, strAccessionNo, lpszImageSource);
		return result;
	}
	BOOL oleLoginViaDomain(LPCTSTR strUsername, LPCTSTR strPassword, LPCTSTR strDomainName)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BSTR ;
		InvokeHelper(0xd, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strUsername, strPassword, strDomainName);
		return result;
	}
	BOOL oleLoginViaFusion(LPCTSTR strUsername, LPCTSTR strPassword, LPCTSTR strWSDLFile)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BSTR ;
		InvokeHelper(0xe, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strUsername, strPassword, strWSDLFile);
		return result;
	}
	BOOL oleLoginViaFusionWithToken(LPCTSTR strUsername, LPCTSTR strToken, LPCTSTR strWSDLFile)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR VTS_BSTR ;
		InvokeHelper(0xf, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strUsername, strToken, strWSDLFile);
		return result;
	}
	BOOL oleSaveCurrentUserProfile()
	{
		BOOL result;
		InvokeHelper(0x10, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleOpenStudy4(LPCTSTR strOpenStudyInfoXML, BOOL bCloseCurWindow, BOOL bFindRelatedStudies, unsigned long nNumPriors, LPCTSTR strProtocolListXML)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BOOL VTS_BOOL VTS_UI4 VTS_BSTR ;
		InvokeHelper(0x11, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strOpenStudyInfoXML, bCloseCurWindow, bFindRelatedStudies, nNumPriors, strProtocolListXML);
		return result;
	}
	BOOL oleExportAsBitmap(LPCTSTR destinationDirectory, short bitmapFormat)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_I2 ;
		InvokeHelper(0x12, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, destinationDirectory, bitmapFormat);
		return result;
	}
	BOOL oleLogout()
	{
		BOOL result;
		InvokeHelper(0x13, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleOpenStudy5(LPCTSTR strOpenStudyInfoXML, short nRows, short nCols, short nImageRows, short nImageColumns, BOOL bShowStudyManager, BOOL bCloseCurWindow, BOOL bFindRelatedStudies, unsigned long nNumPriors, BOOL bApplyHP, LPCTSTR strProtocolListXML)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_I2 VTS_I2 VTS_I2 VTS_I2 VTS_BOOL VTS_BOOL VTS_BOOL VTS_UI4 VTS_BOOL VTS_BSTR ;
		InvokeHelper(0x14, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strOpenStudyInfoXML, nRows, nCols, nImageRows, nImageColumns, bShowStudyManager, bCloseCurWindow, bFindRelatedStudies, nNumPriors, bApplyHP, strProtocolListXML);
		return result;
	}
	BOOL oleLock()
	{
		BOOL result;
		InvokeHelper(0x15, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleUnlock()
	{
		BOOL result;
		InvokeHelper(0x16, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}
	BOOL oleIsLocked()
	{
		BOOL result;
		InvokeHelper(0x17, DISPATCH_METHOD, VT_BOOL, (void*)&result, NULL);
		return result;
	}

	BOOL oleSelectServers(LPCTSTR strImageSourceGUID, LPCTSTR strServerList)
	{
		BOOL result;
		static BYTE parms[] = VTS_BSTR VTS_BSTR;
		InvokeHelper(0x18, DISPATCH_METHOD, VT_BOOL, (void*)&result, parms, strImageSourceGUID, strServerList);
		return result;
	}
	// IEFilm properties
public:

};
