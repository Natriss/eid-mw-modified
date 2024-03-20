
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of CertificateTypeAttribute.
	/// </summary>
	public class CertificateTypeAttribute : UIntAttribute
	{
		public CertificateTypeAttribute() : base((uint)CKA.CERTIFICATE_TYPE)
		{
		}

		public CertificateTypeAttribute(CK_ATTRIBUTE ckAttr) : base(ckAttr)
		{
		}

		public CKC CertificateType
		{
			get { return (CKC)Value; }
			set
			{
				Value = (uint)value;
				IsAssigned = true;
			}
		}

		public override string ToString()
		{
			return string.Format("[CertificateTypeAttribute CertificateType={0}]", CertificateType);
		}


		protected override P11Attribute GetCkLoadedCopy()
		{
			return new CertificateTypeAttribute(CK_ATTRIBUTE);
		}
	}
}
