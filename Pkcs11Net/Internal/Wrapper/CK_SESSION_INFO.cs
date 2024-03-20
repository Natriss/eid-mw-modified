using System.Runtime.InteropServices;

namespace Be.Belgium.Net.Internal.Wrapper
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
	public struct CK_SESSION_INFO
	{

		public uint slotID;

		public uint state;

		public uint flags;

		public uint ulDeviceError;
	}
}
