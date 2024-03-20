
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_EncryptUpdate(
	   uint hSession,
	   byte[] pPart,
	   uint ulPartLen,
	   byte[] pEncryptedPart,
	   ref uint pulEncryptedPartLen
   );
}
