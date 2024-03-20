
using System;
using Be.Belgium.Net.Internal.Objects;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of KeyPait.
	/// </summary>
	public class KeyPair
	{
		PublicKey pubKey;
		PrivateKey privKey;

		public PublicKey PublicKey
		{
			get { return pubKey; }
		}

		public PrivateKey PrivateKey
		{
			get { return privKey; }
		}


		public KeyPair(PublicKey publicKey, PrivateKey privateKey)
		{
			pubKey = publicKey;
			privKey = privateKey;
		}
	}
}
