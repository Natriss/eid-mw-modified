
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_GetOperationState(
	   uint hSession,
	   byte[] pOperationState,
	   ref uint pulOperationStateLen
   );
}
