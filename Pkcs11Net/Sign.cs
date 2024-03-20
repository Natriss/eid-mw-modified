using Be.Belgium.Net.Internal;
using Be.Belgium.Net.Internal.Objects;
using Be.Belgium.Net.Internal.Wrapper;
using System.Linq;
using System.Text;

namespace Be.Belgium.Net
{
	class Sign
	{
		private Module _module = null;
		private readonly string _mFileName = "beidpkcs11.dll";

		public Sign() { }
		public Sign(string moduleFileName)
		{
			_mFileName = moduleFileName;
		}
		/// <summary>
		/// Sign data with a named private key
		/// </summary>
		/// <param name="data">Data to be signed</param>
		/// <param name="privatekeylabel">Label for private key. Can be "Signature" or "Authentication"</param>
		/// <returns>Signed data.</returns>
		public byte[] DoSign(byte[] data, string privatekeylabel)
		{
			if (_module == null)
			{
				_module = Module.GetInstance(_mFileName);
			}

			byte[] encryptedData = null;
			try
			{
				Slot slot = _module.GetSlotList(true).First();
				Session session = slot.Token.OpenSession(true);
				ObjectClassAttribute classAttribute = new(CKO.PRIVATE_KEY);
				ByteArrayAttribute keyLabelAttribute = new(CKA.LABEL)
				{
					Value = Encoding.UTF8.GetBytes(privatekeylabel)
				};

				session.FindObjectsInit(new P11Attribute[] { classAttribute, keyLabelAttribute });
				P11Object[] privatekeys = session.FindObjects(1);
				session.FindObjectsFinal();

				if (privatekeys.Length >= 1)
				{
					session.SignInit(new Mechanism(CKM.SHA1_RSA_PKCS), (PrivateKey)privatekeys[0]);
					encryptedData = session.Sign(data);
				}
			}
			finally
			{
				_module.Dispose();
				_module = null;
			}
			return encryptedData;
		}

	}
}
