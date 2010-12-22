

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


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


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, LIBID_CustomeMessageExampleLib,0xD5D93889,0x0F92,0x4927,0x87,0x60,0xF6,0x56,0x4F,0x54,0x09,0xA1);


MIDL_DEFINE_GUID(IID, DIID__DCustomeMessageExample,0x9F9EDA34,0x703E,0x4F18,0xA9,0xBB,0x28,0xDD,0xFC,0x65,0x7D,0x5D);


MIDL_DEFINE_GUID(IID, DIID__DCustomeMessageExampleEvents,0x408EF484,0xF278,0x4BE5,0x8C,0x74,0xBD,0xBA,0x41,0x12,0xF8,0xE5);


MIDL_DEFINE_GUID(CLSID, CLSID_CustomeMessageExample,0x023E9FAE,0x9641,0x49B6,0x95,0xA0,0x24,0xF1,0x9E,0x43,0x69,0x8D);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



