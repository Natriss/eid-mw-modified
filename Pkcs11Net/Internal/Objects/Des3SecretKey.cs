﻿using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	public class Des3SecretKey : SecretKey
	{
		ByteArrayAttribute value_ = new ByteArrayAttribute(CKA.VALUE);

		public ByteArrayAttribute Value
		{
			get { return value_; }
		}

		public Des3SecretKey()
		{
			KeyType.KeyType = CKK.DES3;
		}

		public Des3SecretKey(Session session, uint hObj) : base(session, hObj)
		{
		}

		public override void ReadAttributes(Session session)
		{
			base.ReadAttributes(session);

			value_ = ReadAttribute(session, HObj, new ByteArrayAttribute(CKA.VALUE));
		}

		public static new P11Object GetInstance(Session session, uint hObj)
		{
			return new Des3SecretKey(session, hObj);
		}
	}
}
