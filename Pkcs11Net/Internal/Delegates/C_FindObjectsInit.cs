
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_FindObjectsInit(
	   uint hSession,
	   CK_ATTRIBUTE[] pTemplate,
	   uint ulCount
   );
}
