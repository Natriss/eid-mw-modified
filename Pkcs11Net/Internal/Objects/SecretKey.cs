
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal.Objects
{
	/// <summary>
	/// Description of SecretKey.
	/// </summary>
	public class SecretKey : Key
	{
		ByteArrayAttribute subject = new ByteArrayAttribute(CKA.SUBJECT),
		checkValue = new ByteArrayAttribute(CKA.CHECK_VALUE);

		BooleanAttribute sensitive = new BooleanAttribute(CKA.SENSITIVE),
		decrypt = new BooleanAttribute(CKA.DECRYPT),
		encrypt = new BooleanAttribute(CKA.ENCRYPT),
		sign = new BooleanAttribute(CKA.SIGN),
		verify = new BooleanAttribute(CKA.VERIFY),
		wrap = new BooleanAttribute(CKA.WRAP),
		unwrap = new BooleanAttribute(CKA.UNWRAP),
		extractable = new BooleanAttribute(CKA.EXTRACTABLE),
		alwaysSensitive = new BooleanAttribute(CKA.ALWAYS_SENSITIVE),
		neverExtractable = new BooleanAttribute(CKA.NEVER_EXTRACTABLE),
		wrapWithTrusted = new BooleanAttribute(CKA.WRAP_WITH_TRUSTED),
		trusted = new BooleanAttribute(CKA.TRUSTED);

		public ByteArrayAttribute Subject
		{
			get { return subject; }
		}

		public ByteArrayAttribute CheckValue
		{
			get { return checkValue; }
		}

		public BooleanAttribute Sensitive
		{
			get { return sensitive; }
		}

		public BooleanAttribute Decrypt
		{
			get { return decrypt; }
		}

		public BooleanAttribute Encrypt
		{
			get { return encrypt; }
		}

		public BooleanAttribute Sign
		{
			get { return sign; }
		}

		public BooleanAttribute Verify
		{
			get { return verify; }
		}

		public BooleanAttribute Wrap
		{
			get { return wrap; }
		}

		public BooleanAttribute Unwrap
		{
			get { return unwrap; }
		}

		public BooleanAttribute Extractable
		{
			get { return extractable; }
		}

		public BooleanAttribute AlwaysSensitive
		{
			get { return alwaysSensitive; }
		}

		public BooleanAttribute NeverExtractable
		{
			get { return neverExtractable; }
		}

		public BooleanAttribute WrapWithTrusted
		{
			get { return wrapWithTrusted; }
		}

		public BooleanAttribute Trusted
		{
			get { return trusted; }
		}


		public SecretKey()
		{
			Class.ObjectType = CKO.SECRET_KEY;
		}
		public SecretKey(Session session, uint hObj)
			: base(session, hObj)
		{

		}

		public static new P11Object GetInstance(Session session, uint hObj)
		{
			KeyTypeAttribute keyType = ReadAttribute(session, hObj, new KeyTypeAttribute());

			switch (keyType.KeyType)
			{
				case CKK.DES:
					return GetInstance(session, hObj);
				case CKK.DES2:
					return GetInstance(session, hObj);
				case CKK.DES3:
					return GetInstance(session, hObj);
				default:
					return new SecretKey(session, hObj); // Return at least some info about the secret key.
			}
		}

		public override void ReadAttributes(Session session)
		{
			base.ReadAttributes(session);

			subject = ReadAttribute(session, HObj, new ByteArrayAttribute(CKA.SUBJECT));
			checkValue = ReadAttribute(session, HObj, new ByteArrayAttribute(CKA.CHECK_VALUE));

			sensitive = ReadAttribute(session, HObj, new BooleanAttribute(CKA.SENSITIVE));
			decrypt = ReadAttribute(session, HObj, new BooleanAttribute(CKA.DECRYPT));
			encrypt = ReadAttribute(session, HObj, new BooleanAttribute(CKA.ENCRYPT));
			sign = ReadAttribute(session, HObj, new BooleanAttribute(CKA.SIGN));
			verify = ReadAttribute(session, HObj, new BooleanAttribute(CKA.VERIFY));
			wrap = ReadAttribute(session, HObj, new BooleanAttribute(CKA.WRAP));
			unwrap = ReadAttribute(session, HObj, new BooleanAttribute(CKA.UNWRAP));
			extractable = ReadAttribute(session, HObj, new BooleanAttribute(CKA.EXTRACTABLE));
			alwaysSensitive = ReadAttribute(session, HObj, new BooleanAttribute(CKA.ALWAYS_SENSITIVE));
			neverExtractable = ReadAttribute(session, HObj, new BooleanAttribute(CKA.NEVER_EXTRACTABLE));
			wrapWithTrusted = ReadAttribute(session, HObj, new BooleanAttribute(CKA.WRAP_WITH_TRUSTED));
			trusted = ReadAttribute(session, HObj, new BooleanAttribute(CKA.TRUSTED));

		}

		public override string ToString()
		{
			if (subject.Value != null)
				return "SecretKey object: " + System.Text.Encoding.ASCII.GetString(subject.Value);
			else
				return "SecretKey: " + base.ToString();
		}

	}
}
