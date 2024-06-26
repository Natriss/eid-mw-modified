﻿
using System;
using Be.Belgium.Net.Internal;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of GostPublicKey.
	/// </summary>
	public class GostPublicKey : PublicKey
	{
		/// <summary>
		/// Params.
		/// </summary>
		protected ByteArrayAttribute params_ = new ByteArrayAttribute((uint)CKA.GOSTR3410PARAMS);
		public ByteArrayAttribute Params
		{
			get { return params_; }
		}

		public GostPublicKey()
		{
			KeyType.KeyType = CKK.GOST;
			params_.Value = PKCS11Constants.SC_PARAMSET_GOSTR3410_A;
		}

		public GostPublicKey(Session session, uint hObj)
			: base(session, hObj)
		{
			params_.Value = PKCS11Constants.SC_PARAMSET_GOSTR3410_A;
		}

		public static new P11Object GetInstance(Session session, uint hObj)
		{
			return new GostPublicKey(session, hObj);
		}

		public override void ReadAttributes(Session session)
		{
			base.ReadAttributes(session);

			params_ = ReadAttribute(session, HObj, params_);
		}
	}
}
