using System;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of X509AttributeCertificate.
	/// </summary>
	public class X509AttributeCertificate : Certificate
	{
		public X509AttributeCertificate()
		{
		}

		public static new P11Object GetInstance(Session session, uint hObj)
		{
			return null;
		}
	}
}
