
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of Version.
	/// </summary>
	public class Version
	{
		public byte major;

		public byte Major
		{
			get { return major; }
		}
		public byte minor;

		public byte Minor
		{
			get { return minor; }
		}

		internal Version(CK_VERSION ckVersion)
		{
			minor = ckVersion.minor[0];
			major = ckVersion.major[0];
		}

		public override string ToString()
		{
			return string.Format("[Version Major={0} Minor={1}]", major, minor);
		}


	}
}
