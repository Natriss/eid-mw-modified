
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of ObjectClassAttribute.
	/// </summary>
	public class ObjectClassAttribute : UIntAttribute
	{
		public ObjectClassAttribute() : base((uint)CKA.CLASS)
		{
		}

		public ObjectClassAttribute(CK_ATTRIBUTE ckAttr) : base(ckAttr)
		{
		}
		public ObjectClassAttribute(CKO objectType) : base((uint)CKA.CLASS)
		{
			ObjectType = objectType;
		}

		public CKO ObjectType
		{
			get { return (CKO)Value; }
			internal set { Value = (uint)value; }
		}

		public override string ToString()
		{
			return string.Format("[ObjectClassAttribute ObjectType={0}]", ObjectType);
		}


		protected override P11Attribute GetCkLoadedCopy()
		{
			return new ObjectClassAttribute(CK_ATTRIBUTE);
		}
	}
}
