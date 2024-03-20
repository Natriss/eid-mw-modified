using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	public class UIntAttribute : P11Attribute
	{
		uint val_;

		public uint Value
		{
			get { return val_; }
			set
			{
				val_ = value;
				IsAssigned = true;
			}
		}

		public override byte[] Encode()
		{
			return BitConverter.GetBytes(Value);
		}

		public override void Decode(byte[] val)
		{
			Value = BitConverter.ToUInt32(val, 0);
		}
		internal UIntAttribute(CK_ATTRIBUTE attr) : base(attr)
		{

		}

		internal UIntAttribute(uint type) : base(type)
		{

		}

		public override string ToString()
		{
			return Value.ToString();
		}

		protected override P11Attribute GetCkLoadedCopy()
		{
			return new UIntAttribute(CK_ATTRIBUTE);
		}

	}
}
