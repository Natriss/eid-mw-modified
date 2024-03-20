
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_GetAttributeValue(
	   uint hSession,
	   uint hObject,
	   ref CK_ATTRIBUTE pTemplate,
	   uint ulCount
   );
}
