
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of KeyTypeAttribute.
	/// </summary>
	public class KeyTypeAttribute : UIntAttribute
	{
		public KeyTypeAttribute() : base((uint)CKA.KEY_TYPE)
		{
		}

		public KeyTypeAttribute(CKK keyType) : base((uint)CKA.KEY_TYPE)
		{
			KeyType = keyType;
		}


		public KeyTypeAttribute(CK_ATTRIBUTE ckAttr) : base(ckAttr)
		{
		}

		public CKK KeyType
		{
			get { return (CKK)Value; }
			set { Value = (uint)value; }
		}

		protected override P11Attribute GetCkLoadedCopy()
		{
			return new KeyTypeAttribute(CK_ATTRIBUTE);
		}
	}
}
