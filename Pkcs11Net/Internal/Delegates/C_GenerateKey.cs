﻿using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Delegates
{
	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl)]
	internal delegate CKR C_GenerateKey(
		uint hSession,
		ref CK_MECHANISM pMechanism,
		CK_ATTRIBUTE[] pTemplate,
		uint ulCount,
		ref uint phKey
	);
}