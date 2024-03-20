
using System;
using Be.Belgium.Net.Internal.Objects;
using Be.Belgium.Net.Internal.Wrapper;

namespace Be.Belgium.Net.Internal
{
	/// <summary>
	/// Description of P11Util.
	/// </summary>
	internal static class P11Util
	{
		public static string ConvertToUtf8String(byte[] val)
		{
			return System.Text.Encoding.UTF8.GetString(val);
		}

		public static string ConvertToASCIIString(byte[] val)
		{
			return System.Text.Encoding.ASCII.GetString(val);
		}

		public static CK_DATE ConvertToCK_DATE(DateTime dateTime)
		{

			CK_DATE d = new CK_DATE();

			d.year = System.Text.Encoding.ASCII.GetBytes(ConvertIntToString(dateTime.Year, 4));
			d.month = System.Text.Encoding.ASCII.GetBytes(ConvertIntToString(dateTime.Month, 2));
			d.day = System.Text.Encoding.ASCII.GetBytes(ConvertIntToString(dateTime.Day, 2));

			return d;
		}

		public static DateTime ConvertToDateTime(CK_DATE ckDate)
		{

			int _year = int.Parse(System.Text.Encoding.ASCII.GetString(ckDate.year));
			int _month = int.Parse(System.Text.Encoding.ASCII.GetString(ckDate.month));
			int _day = int.Parse(System.Text.Encoding.ASCII.GetString(ckDate.day));
			return new DateTime(_year, _month, _day);
		}

		public static string ConvertIntToString(int val, int strSize)
		{
			string str = new string('0', strSize) + val.ToString();
			return str.Substring(str.Length - strSize, strSize);
		}

		public static DateTime ConvertToDateTimeYYYYMMDDhhmmssxx(string time)
		{
			try
			{
				return new DateTime(
					int.Parse(time.Substring(0, 4)),
					int.Parse(time.Substring(4, 2)),
					int.Parse(time.Substring(6, 2)),
					int.Parse(time.Substring(8, 2)),
					int.Parse(time.Substring(10, 2)),
					int.Parse(time.Substring(12, 2)),
					int.Parse(time.Substring(14, 2)),
					DateTimeKind.Utc);
			}
			catch
			{
				return new DateTime();
			}

		}


		public static CK_ATTRIBUTE[] ConvertToCK_ATTRIBUTEs(P11Attribute[] attrs)
		{

			if (attrs == null || attrs.Length == 0) return null;

			CK_ATTRIBUTE[] ckAttrs = new CK_ATTRIBUTE[attrs.Length];

			for (int i = 0; i < attrs.Length; i++)
				ckAttrs[i] = attrs[i].CK_ATTRIBUTE;

			return ckAttrs;
		}
	}
}
