
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_Sign(
	   uint hSession,
	   byte[] pData,
	   uint ulDataLen,
	   byte[] pSignature,
	   ref uint pulSignatureLen
   );
}
