using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Be.Belgium.Net
{
	/// Example Integrity checking class
	/** Some examples on how to verify certificates and signatures
     */
	public static class Integrity
	{
		/// <summary>
		/// Verify a signature with a given certificate. It is assumed that
		/// the signature is made from a SHA1 hash of the data.
		/// </summary>
		/// <param name="data">Signed data</param>
		/// <param name="signature">Signature to be verified</param>
		/// <param name="certificate">Certificate containing the public key used to verify the code</param>
		/// <returns>True if the verification succeeds</returns>
		public static bool Verify(byte[] data, byte[] signature, byte[] certificate)
		{
			try
			{
				X509Certificate2 x509Certificate = new(certificate);
				RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509Certificate.GetRSAPublicKey();
				return rsa.VerifyData(data, "SHA256", signature);
			}
			catch (Exception e)
			{
				Debug.WriteLine(string.Format("Error: {0}", e.Message));
				return false;
			}
		}

		/// <summary>
		/// Check a certificate chain. In order to trust the certficate, the root certificate must be in
		/// stored in the trusted root certificates store. An online CRL check of the chain will be carried out.
		/// </summary>
		/// <param name="CACertificates">CA certificates</param>
		/// <param name="leafCertificate">The certificate whose chain will be checked</param>
		/// <returns>True if the certificate is trusted according the system settings</returns>
		public static bool CheckCertificateChain(List<byte[]> CACertificates, byte[] leafCertificate)
		{
			X509Chain chain = new();
			chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;

			foreach (byte[] CACert in CACertificates)
			{
				chain.ChainPolicy.ExtraStore.Add(new X509Certificate2(CACert));
			}

			bool chainIsValid = chain.Build(new X509Certificate2(leafCertificate));

			for (int i = 0; i < chain.ChainStatus.Length; i++)
			{
				Debug.WriteLine(string.Format("Chain status: {0} ({1})", chain.ChainStatus[i].Status, chain.ChainStatus[i].StatusInformation));
			}
			return chainIsValid;
		}

	}
}
