using Be.Belgium.Net.Internal;
using Be.Belgium.Net.Internal.Objects;
using Be.Belgium.Net.Internal.Wrapper;
using Microsoft.UI.Xaml.Data;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Be.Belgium.Net
{
	public class EIdDataReader
	{
		private Module _module = null;
		private readonly string _mFileName = "beidpkcs11.dll";
		private readonly Dictionary<string, string> _cardInfo = new()
		{
			{"CARD_DATA", "the extra card data as a file"},
			{"carddata_serialnumber", "the extra card data as a file"},
			{"carddata_comp_code", ""},
			{"carddata_os_number", ""},
			{"carddata_os_version", ""},
			{"carddata_soft_mask_number", ""},
			{"carddata_soft_mask_version", ""},
			{"carddata_appl_version", "O0x11 0x00 for applet v1.1 cards\r\n0x17 for applet v1.7 cards"},
			{"carddata_glob_os_version", "Only available on applet v1.7 cards"},
			{"carddata_appl_int_version", ""},
			{"carddata_pkcs1_support", "Only available on applet v1.7 cards"},
			{"carddata_appl_lifecycle", "Only available on applet v1.7 cards"},
			{"carddata_pkcs15_version", "Only available on applet v1.1 cards"},
			{"carddata_key_exchange_version", ""},
			{"carddata_signature", "Not yet available"},
			{"ATR", "the answer to reset of the card"},
		};
		private readonly Dictionary<string, string> _groupedCardData = new()
		{
			{"id", "Parsed and unparsed data from the identity data file"},
			{"address", "Parsed and unparsed data from the identity data file"},
			{"photo", "The photo file"},
			{"carddata", "Parsed and unparsed data from the card data file and the ATR"},
			{"rncert", "The RN Certificate"},
			{"sign_data_file", "the signature of the identity file"},
			{"sign_address_file", "the signature of the address file"},
		};

		public EIdDataReader() { }
		public EIdDataReader(string moduleFileName)
		{
			_mFileName = moduleFileName;
		}

		/// <summary>
		/// Gets the description of the first slot (cardreader) found.
		/// </summary>
		/// <returns>Description of the first slot found</returns>
		public string GetSlotDescription()
		{
			string slotID;
			SetModule();
			try
			{
				Slot[] slots = _module.GetSlotList(false);
				if (slots.Length == 0)
					slotID = "";
				else
					slotID = slots.First().SlotInfo.SlotDescription.Trim();
			}
			finally
			{
				DisposeModule();
			}
			return slotID;
		}

		/// <summary>
		/// Tries to create a Session, returns NULL in case of failure.
		/// </summary>
		/// <returns></returns>
		private Session CreateSession(Slot slot)
		{
			try
			{
				return slot.Token.OpenSession(true);
			}
			catch
			{
				return null;
			}
		}

		private void SetModule()
		{
			if (_module == null)
			{
				_module = Module.GetInstance(_mFileName);
			}
		}

		private void DisposeModule()
		{
			_module.Dispose();
			_module = null;
		}

		/// <summary>
		/// Gets label of token found in the first non-empty slot (cardreader).
		/// </summary>
		/// <returns></returns>
		public string GetTokenInfoLabel()
		{
			string tokenInfoLabel;
			SetModule();
			try
			{
				tokenInfoLabel = _module.GetSlotList(true)[0].Token.TokenInfo.Label.Trim();
			}
			finally
			{
				DisposeModule();
			}
			return tokenInfoLabel;

		}

		/// <summary>
		/// Generic function to get string data objects from label.
		/// </summary>
		/// <param name="label">Value of label attribute of the object</param>
		/// <returns></returns>
		public string GetData(string label)
		{
			string value = "";
			SetModule();
			try
			{

				Slot[] slotlist = _module.GetSlotList(true);

				if (slotlist.Length <= 0)
				{
					throw new IndexOutOfRangeException("No smartcard has been found.");
				}

				Slot slot = slotlist.First();
				Session session = CreateSession(slot);

				if (session == null)
				{
					throw new NullReferenceException("Session cannot be NULL.");
				}

				ByteArrayAttribute classAttribute = new(CKA.CLASS)
				{
					Value = BitConverter.GetBytes((uint)CKO.DATA)
				};

				ByteArrayAttribute labelAttribute = new(CKA.LABEL)
				{
					Value = Encoding.UTF8.GetBytes(label)
				};

				session.FindObjectsInit(new P11Attribute[] { classAttribute, labelAttribute });
				P11Object[] foundObjects = session.FindObjects(50);
				int counter = foundObjects.Length;
				Data data = null;
				while (counter > 0)
				{
					data = foundObjects[counter - 1] as Data;
					label = data.Label.ToString();
					if (data.Value.Value != null)
					{
						value = Encoding.UTF8.GetString(data.Value.Value);
					}
					counter--;
				}

				session.FindObjectsFinal();
				session.Dispose();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			finally
			{
				DisposeModule();
			}
			return value;
		}

		#region All GetData v1.5
		public string CardNumber { get { return GetData("card_number"); } }
		public string ChipNumber { get { return GetData("chip_number"); } }
		/// <summary>
		/// the card validity begin date
		/// </summary>
		public string ValidityBeginDate { get { return GetData("validity_begin_date"); } }
		/// <summary>
		/// the card validity end date
		/// </summary>
		public string ValidityEndDate { get { return GetData("validity_end_date"); } }
		/// <summary>
		/// the card delivery municipality
		/// </summary>
		public string IssuingMunicipality { get { return GetData("issuing_municipality"); } }
		public string NationalNumber { get { return GetData("national_number"); } }
		public string Surname { get { return GetData("surname"); } }
		public string Firstnames { get { return GetData("firstnames"); } }
		public string FirstLetterOfThirdGivenName { get { return GetData("first_letter_of_third_given_name"); } }
		public string Nationality { get { return GetData("nationality"); } }
		public string LocationOfBirth { get { return GetData("location_of_birth"); } }
		/// <summary>
		/// "Date_Of_Birth" : Birth date, encoded as DD mmmm YYYY (Dutch and French card) or DD.mmm.YYYY (German card)
		/// French: JAN FEV MARS AVR MAI JUIN JUIL AOUT SEPT OCT NOV DEC
		/// Dutch: JAN FEB MAAR APR MEI JUN JUL AUG SEP OKT NOV DEC
		/// German: JAN FEB MӒR APR MAI JUN JUL AUG SEP OKT NOV DEZ
		/// </summary>
		public string DateOfBirth { get { return GetData("date_of_birth"); } }
		/// <summary>
		/// M: man / F/V/W: woman
		/// </summary>
		public string Gender { get { return GetData("gender"); } }
		/// <summary>
		/// noble condition
		/// </summary>
		public string Nobility { get { return GetData("nobility"); } }
		/// <summary>
		/// "Document_Type" : type of document, can be one of the following values:
		/// 1: Belgian citizen
		/// 6: Kids card(< 12 year)
		/// 7: Bootstrap card
		/// 8: “Habilitation / Machtigings-” card
		/// 11: Foreigner card type A
		/// 12: Foreigner card type B
		/// 13: Foreigner card type C
		/// 14: Foreigner card type D
		/// 15: Foreigner card type E
		/// 16: Foreigner card type E+
		/// 17: Foreigner card type F
		/// 18: Foreigner card type F+
		/// 19: Foreigner card type H
		/// 20: Foreigner card type I
		/// 21: Foreigner card type J
		/// </summary>
		public string DocumentType { get { return GetData("document_type"); } }
		/// <summary>
		/// 0: No status
		/// 2: Extended minority
		/// </summary>
		public string SpecialStatus { get { return GetData("special_status"); } }
		/// <summary>
		/// hash of the photo file
		/// </summary>
		public string PhotoHash { get { return GetData("photo_hash"); } }
		public string Duplicata { get { return GetData("duplicata"); } }
		/// <summary>
		/// 1: SHAPE
		/// 2: NATO
		/// </summary>
		public string SpecialOrganization { get { return GetData("special_organization"); } }
		/// <summary>
		/// (this is a boolean value)
		/// </summary>
		public string MemberOfFamily { get { return GetData("member_of_family"); } }
		public string DateAndCountryOfProtection { get { return GetData("date_and_country_of_protection"); } }
		public string WorkPermitMention { get { return GetData("work_permit_mention"); } }
		public string EmployerVat1 { get { return GetData("employer_vat_1"); } }
		public string EmployerVat2 { get { return GetData("employer_vat_2"); } }
		public string RegionalFileNumber { get { return GetData("regional_file_number"); } }
		/// <summary>
		/// SHA384 hash of the public basic key
		/// Only available on applet v1.8 cards
		/// </summary>
		public string BasicKeyHash { get { return GetData("basic_key_hash"); } }
		/// <summary>
		/// the streetname and number
		/// </summary>
		public string AddressStreetAndNumber { get { return GetData("address_street_and_number"); } }
		/// <summary>
		/// the zip-code of your town/city
		/// </summary>
		public string AddressZip { get { return GetData("address_zip"); } }
		/// <summary>
		/// your town/city
		/// </summary>
		public string AddressMunicipality { get { return GetData("address_municipality"); } }
		#endregion

		#region All GetData v1.8
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordCardNumber { get { return GetData("record_card_number"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordChipNumber { get { return GetData("record_chip_number"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordValidityBeginDate { get { return GetData("record_validity_begin_date"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordValidityEndDate { get { return GetData("record_validity_end_date"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordIssuingMunicipality { get { return GetData("record_issuing_municipality"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordNationalNumber { get { return GetData("record_national_number"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordSurname { get { return GetData("record_surname"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordFirstnames { get { return GetData("record_firstnames"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordFirstLetterOfThirdGivenName { get { return GetData("record_first_letter_of_third_given_name"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordNationality { get { return GetData("record_nationality"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordLocationOfBirth { get { return GetData("record_location_of_birth"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordDateOfBirth { get { return GetData("record_date_of_birth"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordGender { get { return GetData("record_gender"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordNobility { get { return GetData("record_nobility"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordDocumentType { get { return GetData("record_document_type"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordSpecialStatus { get { return GetData("record_special_status"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordPhotoHash { get { return GetData("record_photo_hash"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordDuplicata { get { return GetData("record_duplicata"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordSpecialOrganization { get { return GetData("record_special_organization"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordMemberOfFamily { get { return GetData("record_member_of_family"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordDateAndCountryOfProtection { get { return GetData("record_date_and_country_of_protection"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordWorkPermitMention { get { return GetData("record_work_permit_mention"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordEmployerVat1 { get { return GetData("record_employer_vat_1"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordEmployerVat2 { get { return GetData("record_employer_vat_2"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordRegionalFileNumber { get { return GetData("record_regional_file_number"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordBasicKeyHash { get { return GetData("record_basic_key_hash"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordAddressStreetAndNumber { get { return GetData("record_address_street_and_number"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordAddressZip { get { return GetData("record_address_zip"); } }
		/// <summary>
		/// Only available on applet v1.8 cards
		/// </summary>
		public string RecordAddressMunicipality { get { return GetData("record_address_municipality"); } }
		#endregion

		/// <summary>
		/// Return raw byte data from objects.
		/// </summary>
		/// <param name="filename">Label value of the object</param>
		/// <returns>byte array with file</returns>
		public byte[] GetFile(string filename)
		{
			byte[] value = null;
			SetModule();
			try
			{
				Slot[] slotlist = _module.GetSlotList(true);

				if (slotlist.Length <= 0)
				{
					throw new IndexOutOfRangeException("No smartcard has been found.");
				}

				Slot slot = slotlist.First();
				Session session = slot.Token.OpenSession(true);

				ByteArrayAttribute fileLabel = new(CKA.LABEL)
				{
					Value = Encoding.UTF8.GetBytes(filename)
				};
				ByteArrayAttribute fileData = new(CKA.CLASS)
				{
					Value = BitConverter.GetBytes((uint)CKO.DATA)
				};
				session.FindObjectsInit(new P11Attribute[] { fileLabel, fileData });
				P11Object[] foundObjects = session.FindObjects(1);
				if (foundObjects.Length != 0)
				{
					Data file = foundObjects[0] as Data;
					value = file.Value.Value;
				}
				session.FindObjectsFinal();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			finally
			{
				DisposeModule();
			}
			return value;
		}

		#region All GetFile
		public byte[] DataFile { get { return GetFile("DATA_FILE"); } }
		public byte[] AddressFile { get { return GetFile("ADDRESS_FILE"); } }
		public byte[] PhotoFile { get { return GetFile("PHOTO_FILE"); } }
		public byte[] SignDataFile { get { return GetFile("SIGN_DATA_FILE"); } }
		public byte[] SignAddressFile { get { return GetFile("SIGN_ADDRESS_FILE"); } }
		public byte[] BasicKeyFile { get { return GetFile("BASIC_KEY_FILE"); } }
		#endregion

		/// <summary>
		/// Return raw byte data from objects of object class Certificate.
		/// </summary>
		/// <param name="Certificatename">Label value of the certificate object</param>
		/// <returns>byte array with certificate file</returns>
		private byte[] GetCertificateFile(string Certificatename)
		{
			byte[] value = null;
			SetModule();
			try
			{
				Slot[] slotlist = _module.GetSlotList(true);

				if (slotlist.Length <= 0)
				{
					throw new IndexOutOfRangeException("No smartcard has been found.");
				}

				Slot slot = slotlist.First();
				Session session = slot.Token.OpenSession(true);
				ByteArrayAttribute fileLabel = new(CKA.LABEL);
				ObjectClassAttribute certificateAttribute = new(CKO.CERTIFICATE);
				fileLabel.Value = Encoding.UTF8.GetBytes(Certificatename);
				session.FindObjectsInit(new P11Attribute[] { certificateAttribute, fileLabel });
				P11Object[] foundObjects = session.FindObjects(1);
				if (foundObjects.Length != 0)
				{
					X509PublicKeyCertificate cert = foundObjects[0] as X509PublicKeyCertificate;
					value = cert.Value.Value;
				}
				session.FindObjectsFinal();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			finally
			{
				DisposeModule();
			}
			return value;
		}

		#region All certificates
		public byte[] AuthenticationCertificateFile { get { return GetCertificateFile("Authentication"); } }
		public byte[] SignatureCertificateFile { get { return GetCertificateFile("Signature"); } }
		public byte[] CACertificateFile { get { return GetCertificateFile("CA"); } }
		public byte[] RootCertificateFile { get { return GetCertificateFile("Root"); } }
		/// <summary>
		/// RN Certificate
		/// </summary>
		public byte[] CertificateRNFile { get { return GetCertificateFile("CERT_RN_FILE"); } }

		#endregion

		/// <summary>
		/// Returns a list of PKCS11 labels of the certificate on the card.
		/// </summary>
		/// <returns>List of labels of certificate objects</returns>
		public List<string> GetCertificateLabels()
		{
			SetModule();
			List<string> labels = new();
			try
			{
				Slot[] slotlist = _module.GetSlotList(true);

				if (slotlist.Length <= 0)
				{
					throw new IndexOutOfRangeException("No smartcard has been found.");
				}

				Slot slot = slotlist.First();
				Session session = slot.Token.OpenSession(true);
				ObjectClassAttribute certificateAttribute = new(CKO.CERTIFICATE);
				session.FindObjectsInit(new P11Attribute[] { certificateAttribute });
				P11Object[] certificates = session.FindObjects(100);
				foreach (P11Object certificate in certificates)
				{
					labels.Add(new string(((X509PublicKeyCertificate)certificate).Label.Value));
				}
				session.FindObjectsFinal();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			finally
			{
				DisposeModule();
			}
			return labels;
		}

		/// <summary>
		/// Returns a list of CKA_LABEL with it's corresponding information.
		/// </summary>
		public Dictionary<string, string> CardInfo { get { return _cardInfo; } }
		/// <summary>
		/// Starting from eID MW 4.0.2, the CKA_OBJECT_ID attribute has been added to the CKO_DATA object's attribute lists.
		/// This allows for searching for a group of objects with the same CKA_OBJECT_ID.
		/// (e.g.If you are interested in finding all objects of the identity data file (and the unparsed identity file), search for object with CKA_CLASS set to CKO_DATA and with CKA_OBJECT_ID set to id.
		/// </summary>
		public Dictionary<string, string> GroupedCardData { get { return _groupedCardData; } }
	}
}
