
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of SlotInfo.
	/// </summary>
	public class SlotInfo
	{
		CK_SLOT_INFO ckSlotInfo;

		public string SlotDescription
		{
			get { return P11Util.ConvertToUtf8String(ckSlotInfo.slotDescription); }
		}

		public string ManufacturerID
		{
			get { return P11Util.ConvertToUtf8String(ckSlotInfo.manufacturerID); }
		}

		public Version FirmwareVersion
		{
			get { return new Version(ckSlotInfo.firmwareVersion); }
		}

		public Version HardwareVersion
		{
			get { return new Version(ckSlotInfo.hardwareVersion); }
		}

		public bool IsTokenPresent
		{
			get { return (ckSlotInfo.flags & 1L) != 0L; }
		}


		public bool IsRemovableDevice
		{
			get { return (ckSlotInfo.flags & 0x2) != 0L; }
		}


		public bool IsHwSlot
		{
			get { return (ckSlotInfo.flags & 0x4) != 0L; }
		}


		internal SlotInfo(CK_SLOT_INFO ckSlotInfo)
		{
			this.ckSlotInfo = ckSlotInfo;
		}
	}
}
