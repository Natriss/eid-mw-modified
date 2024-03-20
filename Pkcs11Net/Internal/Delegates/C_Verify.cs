using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_Verify(
	   uint hSession,
	   byte[] pData,
	   uint ulDataLen,
	   byte[] pSignature,
	   uint pulSignatureLen
   );
}
