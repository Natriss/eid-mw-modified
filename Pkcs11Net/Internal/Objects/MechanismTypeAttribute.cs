
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of MechanismTypeAttribute.
	/// </summary>
	public class MechanismTypeAttribute : UIntAttribute
	{

		internal MechanismTypeAttribute(uint type) : base(type)
		{

		}
		internal MechanismTypeAttribute(CKA type) : base((uint)type)
		{
		}

		public MechanismTypeAttribute(CKA type, CKM mechanismType) : base((uint)type)
		{
			MechanismType = mechanismType;
		}


		public MechanismTypeAttribute(CK_ATTRIBUTE ckAttr) : base(ckAttr)
		{
		}

		public CKM MechanismType
		{
			get { return (CKM)Value; }
			set { Value = (uint)value; }
		}

		protected override P11Attribute GetCkLoadedCopy()
		{
			return new MechanismTypeAttribute(CK_ATTRIBUTE);
		}
	}
}
