
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of TokenException.
	/// </summary>
	public class TokenException : Exception
	{
		public TokenException()
		{
		}

		public TokenException(CKR errorCode) : base(errorCode.ToString())
		{
			this.errorCode = errorCode;
		}

		CKR errorCode;

		public CKR ErrorCode
		{
			get { return errorCode; }
		}

	}
}
