
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of Info.
	/// </summary>
	public class Info
	{
		protected Version cryptokiVersion_;

		public Version CryptokiVersion
		{
			get { return cryptokiVersion_; }
		}
		protected string manufacturerID_;

		public string ManufacturerID
		{
			get { return manufacturerID_; }
		}
		protected string libraryDescription_;

		public string LibraryDescription
		{
			get { return libraryDescription_; }
		}
		protected Version libraryVersion_;

		public Version LibraryVersion
		{
			get { return libraryVersion_; }
		}

		internal Info(CK_INFO ckInfo)
		{
			cryptokiVersion_ = new Version(ckInfo.cryptokiVersion);
			manufacturerID_ = P11Util.ConvertToUtf8String(ckInfo.manufacturerID);
			libraryDescription_ = P11Util.ConvertToUtf8String(ckInfo.libraryDescription);
			libraryVersion_ = new Version(ckInfo.libraryVersion);
		}

		public override string ToString()
		{
			return string.Format("[Info CryptokiVersion={0} ManufacturerID={1} LibraryDescription={2} LibraryVersion={3}]", cryptokiVersion_, manufacturerID_, libraryDescription_, libraryVersion_);
		}

	}
}
