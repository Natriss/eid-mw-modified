
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_CopyObject(
	   uint hSession,
	   uint hObject,
	   CK_ATTRIBUTE[] hTemplate,
	   uint ulCount,
	   ref uint phNewObject
   );
}
