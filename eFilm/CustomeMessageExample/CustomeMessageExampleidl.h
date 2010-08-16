

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* at Mon Jun 22 02:36:16 2009
 */
/* Compiler settings for .\CustomeMessageExample.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __CustomeMessageExampleidl_h__
#define __CustomeMessageExampleidl_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef ___DCustomeMessageExample_FWD_DEFINED__
#define ___DCustomeMessageExample_FWD_DEFINED__
typedef interface _DCustomeMessageExample _DCustomeMessageExample;
#endif 	/* ___DCustomeMessageExample_FWD_DEFINED__ */


#ifndef ___DCustomeMessageExampleEvents_FWD_DEFINED__
#define ___DCustomeMessageExampleEvents_FWD_DEFINED__
typedef interface _DCustomeMessageExampleEvents _DCustomeMessageExampleEvents;
#endif 	/* ___DCustomeMessageExampleEvents_FWD_DEFINED__ */


#ifndef __CustomeMessageExample_FWD_DEFINED__
#define __CustomeMessageExample_FWD_DEFINED__

#ifdef __cplusplus
typedef class CustomeMessageExample CustomeMessageExample;
#else
typedef struct CustomeMessageExample CustomeMessageExample;
#endif /* __cplusplus */

#endif 	/* __CustomeMessageExample_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 


#ifndef __CustomeMessageExampleLib_LIBRARY_DEFINED__
#define __CustomeMessageExampleLib_LIBRARY_DEFINED__

/* library CustomeMessageExampleLib */
/* [control][helpstring][helpfile][version][uuid] */ 


EXTERN_C const IID LIBID_CustomeMessageExampleLib;

#ifndef ___DCustomeMessageExample_DISPINTERFACE_DEFINED__
#define ___DCustomeMessageExample_DISPINTERFACE_DEFINED__

/* dispinterface _DCustomeMessageExample */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__DCustomeMessageExample;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("9F9EDA34-703E-4F18-A9BB-28DDFC657D5D")
    _DCustomeMessageExample : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DCustomeMessageExampleVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DCustomeMessageExample * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DCustomeMessageExample * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DCustomeMessageExample * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DCustomeMessageExample * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DCustomeMessageExample * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DCustomeMessageExample * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DCustomeMessageExample * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _DCustomeMessageExampleVtbl;

    interface _DCustomeMessageExample
    {
        CONST_VTBL struct _DCustomeMessageExampleVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DCustomeMessageExample_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _DCustomeMessageExample_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _DCustomeMessageExample_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _DCustomeMessageExample_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define _DCustomeMessageExample_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define _DCustomeMessageExample_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define _DCustomeMessageExample_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DCustomeMessageExample_DISPINTERFACE_DEFINED__ */


#ifndef ___DCustomeMessageExampleEvents_DISPINTERFACE_DEFINED__
#define ___DCustomeMessageExampleEvents_DISPINTERFACE_DEFINED__

/* dispinterface _DCustomeMessageExampleEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__DCustomeMessageExampleEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("408EF484-F278-4BE5-8C74-BDBA4112F8E5")
    _DCustomeMessageExampleEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DCustomeMessageExampleEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DCustomeMessageExampleEvents * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DCustomeMessageExampleEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DCustomeMessageExampleEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DCustomeMessageExampleEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DCustomeMessageExampleEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DCustomeMessageExampleEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DCustomeMessageExampleEvents * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _DCustomeMessageExampleEventsVtbl;

    interface _DCustomeMessageExampleEvents
    {
        CONST_VTBL struct _DCustomeMessageExampleEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DCustomeMessageExampleEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _DCustomeMessageExampleEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _DCustomeMessageExampleEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _DCustomeMessageExampleEvents_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define _DCustomeMessageExampleEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define _DCustomeMessageExampleEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define _DCustomeMessageExampleEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DCustomeMessageExampleEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_CustomeMessageExample;

#ifdef __cplusplus

class DECLSPEC_UUID("023E9FAE-9641-49B6-95A0-24F19E43698D")
CustomeMessageExample;
#endif
#endif /* __CustomeMessageExampleLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


