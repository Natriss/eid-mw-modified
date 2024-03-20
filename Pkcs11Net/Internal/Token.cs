
using System;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of Token.
	/// </summary>
	public class Token
	{
		protected Slot slot_;
		public Slot Slot
		{
			get { return slot_; }
		}

		public Module Module
		{
			get { return slot_.Module; }
		}
		public Token(Slot slot)
		{
			slot_ = slot;
		}

		public uint TokenId
		{
			get { return slot_.SlotId; }
		}

		public TokenInfo TokenInfo
		{
			get
			{
				return new TokenInfo(slot_.Module.P11Module.GetTokenInfo(slot_.SlotId));
			}
		}

		public CKM[] MechanismList
		{
			get
			{
				return Module.P11Module.GetMechanismList(TokenId);
			}
		}

		public MechanismInfo GetMechanismInfo(CKM ckm)
		{
			return new MechanismInfo(Module.P11Module.GetMechanismInfo(TokenId, ckm));
		}

		public Session OpenSession(bool readOnly)
		{
			return new Session(this, slot_.Module.P11Module.OpenSession(slot_.SlotId, 0, readOnly));
		}

		public void InitToken(string pin, string label)
		{
			Module.P11Module.InitToken(slot_.SlotId, pin, label);
		}
	}
}
