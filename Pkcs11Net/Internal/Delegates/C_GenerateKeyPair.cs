using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_GenerateKeyPair(
		uint hSession,
		ref CK_MECHANISM pMechanism,
		CK_ATTRIBUTE[] pPublicKeyTemplate,
		uint ulPublicKeyAttributeCount,
		CK_ATTRIBUTE[] pPrivateKeyTemplate,
		uint ulPrivateKeyAttributeCount,
		ref uint phPublicKey,
		ref uint phPrivateKey
	);
}