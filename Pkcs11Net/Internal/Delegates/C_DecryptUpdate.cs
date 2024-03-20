using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_DecryptUpdate(
	   uint hSession,
	   byte[] pEncryptedPart,
	   uint ulEncryptedPartLen,
	   byte[] pPart,
	   ref uint pulPartLen
   );
}
